using UnityEngine;

public class OrderSystem : MonoBehaviour {

    public static OrderSystem Instance { get; private set; }

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ProcessOrder(Car car, string customerName) {

        POSManager.Instance.SetCurrentCar(car, customerName);

        Debug.Log("Processing bill for: " + customerName);

    }

    public void ReicevedRequestData(Car car, string customerName, double totalCost) {

        car.GetOrderData(totalCost);

    }

}