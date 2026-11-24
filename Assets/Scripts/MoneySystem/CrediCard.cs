using UnityEngine;

public class CrediCard : MonoBehaviour {

    [SerializeField] private DraggableItem draggableItem;

    private float totalOrderCost;

    private Car car;

    private TransactionStatus transactionStatus;

    private void Start() {
        transactionStatus = TransactionStatus.Pending;
    }

    public void GetOrderData(Car car, OrderRequest orderRequest, float totalOrderCost) {
        
        this.totalOrderCost = totalOrderCost;
        this.car = car;

    }

    public float GetTotalOrderCost() {
        return totalOrderCost;
    }

    public TransactionStatus GetTransactionStatus() {
        return transactionStatus;
    }

    public void SetTransactionStatus(TransactionStatus status) {
        transactionStatus = status;
    }

    public Car GetCar() {
        return car;
    }

    public void RemoveCard() {

        car.SetIsPaymentDoneByCard(true);

        Destroy(gameObject);
    }

}
public enum TransactionStatus {
    Pending,
    Success,
    Failure
}