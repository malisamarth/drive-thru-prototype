using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MoneySystem : MonoBehaviour {

    public static MoneySystem Instance { get; private set; }

    [SerializeField] private GameObject creditCardParent;
    [SerializeField] private GameObject CashCounter;

    [SerializeField] private PaymentMahcineVisual paymentMahcineVisual;

    [SerializeField] private PaymentMachine paymentMachine;

    private Car car;
    private float totalOrderCost;   
    private TransactionStatus transactionStatus;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    private void Reset() {
        car = null;
        totalOrderCost = 0f;
        transactionStatus = TransactionStatus.Pending;
        SetPaymentMachineVisual("--------", 00);
    }

    public void PayButton() {

        if (car == null) {
            Debug.Log("No car is currently being processed for payment.");
            return;
        }

        if (!(car.GetIsPaymentDoneByCard())) {
            return;
        }

        car.OrderTransactionResult(transactionStatus);
        Reset();
    }


    public void ProcessTransactionRequest(Car car, float totalOrderCost, TransactionStatus status) {

        this.car = car;
        this.transactionStatus = status;

        Debug.Log("Processing transaction request for " + car.GetCustomerName() + " with total order cost: " + totalOrderCost);


        SetPaymentMachineVisual(car.GetCustomerName(), totalOrderCost);

        paymentMahcineVisual.UpdateMoneyEarned(totalOrderCost);

        paymentMachine.ShowIndicator();

    }

    public GameObject GetCreditCardParent() {
        return creditCardParent;
    }

    public GameObject GetCashCounter() {
        return CashCounter;
    }

    private void SetPaymentMachineVisual(string name, float totalCost) {
        paymentMahcineVisual.UpdateCustomerName(name);
        paymentMahcineVisual.UpdateTotalOrderCost(totalCost);
    }

    public Car GetCar() {
        return car;
    }

}