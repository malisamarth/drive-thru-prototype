
using UnityEngine;

public class Car : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform carRearPoint;
    [SerializeField] private float safeDistance = 2f;

    [SerializeField] private CarOrderRequest carOrderRequest;

    [SerializeField] private PaymentRequest paymentRequest;

    [SerializeField] private RectTransform carDropZone;

    private MoneySystem moneySystem;

    private Vector2 targetPosition;
    private CarManager carManager;

    [HideInInspector] public Car carAhead;

    private CarState currentState;

    private string customerName;

    private double totalOrderCost;

    private bool IsPaymentDoneByCard = false;

    private void Start() {
        carManager = CarManager.Instance;
        moneySystem = MoneySystem.Instance;
        customerName = CustomerInfoGenerator.Instance.GetUniqueCustomerName();
        SetCarState(CarState.DrivingToOrder);
    }

    private void Update() {
        CarMovement();
    }

    //From order to money
    public void GetOrderData(double totalCost) {

        totalOrderCost = totalCost;
        carOrderRequest.SendCustomerVisualData(customerName, (float)totalOrderCost);
        SetCarState(CarState.DrivingToMoney);

    }

    //From money to take out
    public void OrderTransactionResult(TransactionStatus status) {

        if (status == TransactionStatus.Success) {
            carOrderRequest.HideOrderCost();
            SetCarState(CarState.DrivingToTakeOut);
        } else if (status == TransactionStatus.Failure) {
            SetCarState(CarState.DrivingToMoney);
        }

    }

    //From take-out to exist
    public void OrderSatisfied() {

        SetCarState(CarState.Exiting);

    }

    //Test
    public void TestingNextTargetStop() {
       
        if (currentState == CarState.WaitingForOrder)
            SetCarState(CarState.DrivingToMoney);
        else if (currentState == CarState.WaitingToPay)
            SetCarState(CarState.DrivingToTakeOut);
        else if (currentState == CarState.WaitingForTakeOut)
            Destroy(gameObject);


    }


    private void GetTargetPosition() {

        switch (currentState) {
            case CarState.DrivingToOrder:
            case CarState.WaitingForOrder:
                targetPosition = carManager.GetOrderPoint().position;
                break;

            case CarState.DrivingToMoney:
            case CarState.WaitingToPay:
                targetPosition = carManager.GetMoneyPoint().position;
                break;

            case CarState.DrivingToTakeOut:
            case CarState.WaitingForTakeOut:
                targetPosition = carManager.GetTakeOutPoint().position;
                break;

            case CarState.Exiting:
                targetPosition = carManager.GetCarDeletePoint().position;
                break;
        }
    }

    private void SetCarState(CarState newState) {

        currentState = newState;

        GetTargetPosition();

        if (newState == CarState.WaitingForOrder) {

            //Car has reached the order point, so we can show the order request UI for this car.

            carOrderRequest.ShowCarRequestOrder();

            OrderSystem.Instance.ProcessOrder(this, customerName);
        }

        if (newState == CarState.WaitingToPay) {

            //Car has reached the pay point, so we can show the credit card request UI for this car.

            GameObject creditCard = paymentRequest.SpawnCreditCardVisual();

            creditCard.GetComponent<CrediCard>().GetOrderData(this, carOrderRequest.GetOrderDataRequest(), (float)totalOrderCost);

        }

        if (newState == CarState.WaitingForTakeOut) {

            TakeOutManager.Instance.GetOrderRequestCar(this, carOrderRequest.GetOrderDataRequest());


        }


    }

    private void CarMovement() {
        // If another car is ahead, maintain a safe distance.
        if (carAhead != null) {
            float distance = Vector2.Distance(
                transform.position,
                carAhead.transform.position
            );

            if (distance <= safeDistance)
                return;
        }

        // Move toward this car's current station.
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Stop when the station destination is reached.
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
            if (currentState == CarState.DrivingToOrder)
                SetCarState(CarState.WaitingForOrder);
            else if (currentState == CarState.DrivingToMoney)
                SetCarState(CarState.WaitingToPay);
            else if (currentState == CarState.DrivingToTakeOut)
                SetCarState(CarState.WaitingForTakeOut);
            else if (currentState == CarState.Exiting)
                //Total Life cycle is complete!!
                Destroy(gameObject);
        }
    }

    public Vector2 GetClosestCarAheadPosition() {
        return carRearPoint.position;
    }

    public string GetCustomerName() {
        return customerName;
    }

    public RectTransform GetCarDropZone() {
        return carDropZone;
    }

    public void SetIsPaymentDoneByCard(bool used) {
        IsPaymentDoneByCard = used;
    }

    public bool GetIsPaymentDoneByCard() {
        return IsPaymentDoneByCard;
    }

}
