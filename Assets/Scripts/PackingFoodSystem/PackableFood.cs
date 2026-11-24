
using UnityEngine;

public class PackableFood : MonoBehaviour {

    [Header("Packing Footprint")]
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;

    [Header("Packing Rotation")]
    [SerializeField] private float packingRotationZ = 0f;

    [Header("Current Grid Placement")]
    public bool IsPacked { get; private set; }
    public int StartX { get; private set; }
    public int StartY { get; private set; }
    public int PackedWidth { get; private set; }
    public int PackedHeight { get; private set; }

    public int Width => width;
    public int Height => height;
    public float PackingRotationZ => packingRotationZ;

    public void SetPlacement(int startX, int startY, int packedWidth, int packedHeight) {
        StartX = startX;
        StartY = startY;
        PackedWidth = packedWidth;
        PackedHeight = packedHeight;
        IsPacked = true;
    }

    public void ClearPlacement() {
        IsPacked = false;
    }



    private void OnDestroy() {
        if (IsPacked) {
            PackingGrid grid = FindFirstObjectByType<PackingGrid>();

            if (grid != null) {
                grid.EmptyTheCell(
                    StartX,
                    StartY,
                    PackedWidth,
                    PackedHeight
                );
            }
        }
    }

}
