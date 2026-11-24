using UnityEngine;

public class TakeOutFoodDrag : MonoBehaviour {

    private Vector3 offset;
    private bool isDragging;


    private void Update() {

        if (isDragging && Input.GetMouseButton(0)) {
            transform.position = GetMouseWorldPosition() + offset;
        }

        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
        }

    }


    public void StartDragging() {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }


    private void OnMouseDown() {
        StartDragging();
    }

    Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Debug.Log("TakeOut Food Triggered by : - " + collision.gameObject.name);
       
    }


}