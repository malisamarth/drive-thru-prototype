
using UnityEngine;

public class CookedItemDrag : MonoBehaviour {

    [SerializeField] private FoodType foodType;

    private CookedFood cookedFood;
    private PackableFood packableFood;

    private Vector3 offset;
    private Vector3 originalPosition; 

    private bool isDragging;
    private float currentRotation;
    private float baseRotation;

    private PackingGrid packingGrid;

    

    private void Awake() {
        cookedFood = GetComponent<CookedFood>();
        packableFood = GetComponent<PackableFood>();
        isDragging = true;
    }

    private float originalRotation;

    private void Start() {
        packingGrid = FindFirstObjectByType<PackingGrid>();
    }


    private void Update() {
        if (isDragging && Input.GetMouseButton(0)) {
            transform.position = GetMouseWorldPosition() + offset;

            float scroll = Input.mouseScrollDelta.y;

            if (scroll != 0f) {
                currentRotation += scroll > 0f ? 90f : -90f;

                transform.rotation = Quaternion.Euler(
                    0f, 0f, currentRotation
                );
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0)) {
            isDragging = false;
            TryPackFood();
        }
    }

    public void StartDragging() {

        originalRotation = transform.eulerAngles.z;

        originalPosition = transform.position;

        baseRotation = packableFood.PackingRotationZ;
        currentRotation = transform.eulerAngles.z;

        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseDown() {
        StartDragging();
    }


    private void TryPackFood() {
        if (packingGrid == null || packableFood == null) {
            return;
        }

        bool wasPacked = packableFood.IsPacked;

        // Free the item's previous grid cells, if it was packed.
        if (packableFood.IsPacked) {
            packingGrid.EmptyTheCell(
                packableFood.StartX,
                packableFood.StartY,
                packableFood.PackedWidth,
                packableFood.PackedHeight
            );

            packableFood.ClearPlacement();
        }

        int width = packableFood.Width;
        int height = packableFood.Height;

        float rotationDifference = Mathf.Abs(
            Mathf.DeltaAngle(currentRotation, baseRotation)
        );

        bool rotated = rotationDifference > 45f &&
                       rotationDifference < 135f;

        if (rotated) {
            (width, height) = (height, width);
        }

        bool valid = packingGrid.TryGetPlacement(
            foodType,
            transform.position,
            width,
            height,
            out Vector3 placementPosition,
            out int startX,
            out int startY
        );

        if (valid && packingGrid.PlaceItem(startX, startY, width, height)) {
            transform.position = placementPosition;
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            packableFood.SetPlacement(startX, startY, width, height);

            // Count only if this food wasn't already packed.
            if (!wasPacked) {
                packingGrid.UpdateFoodCount(foodType, 1);
            }

            Debug.Log($"Food packed successfully! Footprint: {width}x{height}");
        }
        else {
            // If it was previously packed, remove it from the grid count.
            if (wasPacked) {
                packingGrid.UpdateFoodCount(foodType, -1);
            }

            // Free dragging: leave the food wherever it was dropped.
            packableFood.ClearPlacement();

            Debug.Log("Food dropped outside a valid packing position.");
        }
    }

    private void OnDestroy() {
        
        if (packableFood.IsPacked) {
            packingGrid.UpdateFoodCount(foodType, -1);
        }

    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        cookedFood.TriggeredWith(collision);
    }
}
