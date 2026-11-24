using UnityEngine;

public class OrderNotificationVisual : MonoBehaviour {

    [SerializeField] private GameObject orderNotificationVisual;

    [Header("Notification Animation")]
    [SerializeField] private float rotationAngle = 15f;
    [SerializeField] private float rotationSpeed = 5f;

    private Quaternion initialRotation;

    private void Start() {

        if (orderNotificationVisual != null) {
            initialRotation = orderNotificationVisual.transform.localRotation;
        }

        HideNotification();
    }

    private void Update() {
        if (orderNotificationVisual == null) return;

        float angle = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;

        orderNotificationVisual.transform.localRotation =
            initialRotation * Quaternion.Euler(0f, 0f, angle);
    }


    public void ShowNotification() {
        if (orderNotificationVisual != null) {
            orderNotificationVisual.SetActive(true);
        }
    }

    public void HideNotification() {
        if (orderNotificationVisual != null) {
            orderNotificationVisual.SetActive(false);
        }
    }

}
