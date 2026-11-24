using UnityEngine;

public class CarOrderRequest : MonoBehaviour {

    [SerializeField] private CarOrderVisual carOrderVisual;
    [SerializeField] private GameObject bannerVisual;
    [SerializeField] private OrderNotificationVisual orderNotificationVisual;

    private const int MAX_ITEM_COUNT = 3;

    private int burgerCount;
    private int friesCount;
    private int colaCount;

    private void Start() {
        GenerateRandomOrder();
        bannerVisual.SetActive(false);
    }

    private void GenerateRandomOrder() {

        burgerCount = Random.Range(0, MAX_ITEM_COUNT + 1);
        friesCount = Random.Range(0, MAX_ITEM_COUNT + 1);
        colaCount = Random.Range(0, MAX_ITEM_COUNT + 1);

        if (burgerCount == 0 && friesCount == 0 && colaCount == 0) {

            colaCount++;


        }


        SetVisual();
    }

    private void SetVisual() {
        carOrderVisual.SetOrderVisual(GetOrderDataRequest());
        
    }


    public OrderRequest GetOrderDataRequest() {
        return new OrderRequest(burgerCount, friesCount, colaCount);
    }

    public void ShowCarRequestOrder() {
        bannerVisual.SetActive(true);
        SetNotification();
    }

    public void SendCustomerVisualData(string customerName, float totalCost) {
        carOrderVisual.SetCustomerInfo(customerName, totalCost);
        RemoveNotification();
    }

    private void SetNotification() {
        orderNotificationVisual.ShowNotification();
    }

    private void RemoveNotification() {
        orderNotificationVisual.HideNotification();
    }

    public void HideOrderCost() {
        carOrderVisual.HideOrderCostInfo();
    }

}


public class OrderRequest {
    
    public int BurgerCount { get; set; }
    public int FriesCount { get; set; } = 0; 
    public int ColaCount { get; set; }
    
    public OrderRequest(int burgerCount, int friesCount, int colaCount) {
        BurgerCount = burgerCount;
        FriesCount = friesCount;
        ColaCount = colaCount;
    }


}

public enum FoodItems { 

    Burger,
    Fries,
    Cola

}