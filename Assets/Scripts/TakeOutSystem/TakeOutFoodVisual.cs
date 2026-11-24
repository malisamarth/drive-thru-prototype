using UnityEngine;

public class TakeOutFoodVisual : MonoBehaviour {

    private int burgerCount = 0;
    private int friesCount = 0;
    private int colaCount = 0;

    private bool hasRequestSent = false;


    public void SetFoodCount(int burgerCount, int friesCount, int colaCount) {

        this.burgerCount = burgerCount;
        this.friesCount = friesCount;
        this.colaCount = colaCount;


        Debug.Log("Packed Food : - " +  burgerCount + " " + friesCount + " " + colaCount);


    }

    public OrderRequest RecieveOrder() {

        hasRequestSent = true;

        return new OrderRequest(burgerCount, friesCount, colaCount);


    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.TryGetComponent(out TakeOutManager takeOutManager)) {

            if (hasRequestSent) {
                Destroy(gameObject);
            }



        }

    }


}