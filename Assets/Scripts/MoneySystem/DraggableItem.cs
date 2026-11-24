
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] private CrediCard crediCard;

    private RectTransform cardRect;
    private Canvas canvas;

    private Vector3 originalPosition;
    private bool isDragging;
    private bool paymentSuccessful;

    private RectTransform cashCounter;
    private RectTransform carDropZone;
    
    private void Awake() {
        cardRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = cardRect.position;
    }

    private void Start() {

        cashCounter = MoneySystem.Instance.GetCashCounter().GetComponent<RectTransform>();

        if (cashCounter == null) {
            Debug.LogError("CashCounter RectTransform not found. Please assign it in the inspector.");
        }


    }




    public void OnBeginDrag(PointerEventData eventData) {
        isDragging = true;
        paymentSuccessful = false;
        originalPosition = cardRect.position;

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        if (!isDragging) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        cardRect.position = canvas.transform.TransformPoint(localPoint);

        // Check continuously while dragging.
        if (cashCounter != null &&
            RectTransformUtility.RectangleContainsScreenPoint(
                cashCounter,
                eventData.position,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay
                    ? null : canvas.worldCamera)) 
                    
        {

            if (crediCard.GetTransactionStatus() == TransactionStatus.Pending) {

                crediCard.SetTransactionStatus(TransactionStatus.Success);
                MoneySystem.Instance.ProcessTransactionRequest(crediCard.GetCar(), crediCard.GetTotalOrderCost(), TransactionStatus.Success);
                paymentSuccessful = true;

                Debug.Log("Payment successful!");



                //Post payment, get the car drop zone for card to be dragged

                carDropZone = MoneySystem.Instance.GetCar().GetCarDropZone();


            } else if (crediCard.GetTransactionStatus() == TransactionStatus.Success) {

                Debug.Log("Payment already completed.");

            }


        }
        else {
            paymentSuccessful = false;
        }



        if (carDropZone != null &&
        RectTransformUtility.RectangleContainsScreenPoint(
        carDropZone,
        eventData.position,
        canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null : canvas.worldCamera)) {

            if (crediCard.GetTransactionStatus() == TransactionStatus.Success) {


                crediCard.RemoveCard();

                Debug.Log("Credit Card has been returned");

            }
            else if (crediCard.GetTransactionStatus() == TransactionStatus.Pending) {

                Debug.Log("Credit Card is not used");

            }


        }
        else {
            paymentSuccessful = false;
        }


























    }

    public void OnEndDrag(PointerEventData eventData) {
        isDragging = false;

        if (paymentSuccessful) {
            Debug.Log("Payment completed!");
            // TODO: Complete the order here.
        }
        else {
            // Return the card to where it started.
            //cardRect.position = originalPosition;
            Debug.Log("Card returned to original position.");
        }
    }




}
