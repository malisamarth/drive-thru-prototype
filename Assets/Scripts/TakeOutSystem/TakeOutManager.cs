using Unity.VisualScripting;
using UnityEngine;

public class TakeOutManager : MonoBehaviour {

    public static TakeOutManager Instance { get; private set; }

    private int burgerCount = 0;
    private int friesCount = 0;
    private int colaCount = 0;

    private Car currentCar = null;

    private void Awake() {
        
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

    }


    public void GetOrderRequestCar(Car car, OrderRequest orderRequest) {

        Debug.Log("Takout request :- " + car.GetCustomerName() + " " + orderRequest);

        burgerCount = orderRequest.BurgerCount;
        friesCount = orderRequest.FriesCount;
        colaCount = orderRequest.ColaCount;

        currentCar = car;

    }


    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.TryGetComponent(out TakeOutFoodVisual takeOutFoodVisual)) {

            if (currentCar == null) {
                Debug.Log("No car at Take-Out");
                return;
            }


            OrderRequest RecievedOrder = takeOutFoodVisual.RecieveOrder();

            Debug.Log("FROM GRID :- " + "Burger = " + RecievedOrder.BurgerCount + "Fries = " + RecievedOrder.FriesCount + "Cola = " + RecievedOrder.ColaCount);

            burgerCount -= RecievedOrder.BurgerCount;
            friesCount -= RecievedOrder.FriesCount;
            colaCount -= RecievedOrder.ColaCount;

            if (burgerCount <= 0 && friesCount <= 0 && colaCount <= 0) {


                currentCar.OrderSatisfied();

                ClearCarRequest();
                
                Debug.Log("Order Satisfied!!!");

                return;

            }


            if (!(burgerCount <= 0)) {
                Debug.Log("Need burger");
            }

            if (!(friesCount <= 0)) {
                Debug.Log("Need fries");
            }

            if (!(colaCount <= 0)) {
                Debug.Log("Need cola");
            }

        }

    }


    private void ClearCarRequest() {

        currentCar = null;
        burgerCount = 0;
        friesCount = 0;
        colaCount = 0;

    }


}