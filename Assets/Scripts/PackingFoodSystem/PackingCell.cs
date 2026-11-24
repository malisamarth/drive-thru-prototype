using UnityEngine;



public class PackingCell : MonoBehaviour {

    public Vector2Int GridPosition { get; private set; }

    public bool IsOccupied { get; private set; }

    public void Initialize(int column, int row) {
        GridPosition = new Vector2Int(column, row);
        IsOccupied = false;
    }

    public void SetOccupied(bool occupied) {
        IsOccupied = occupied;
    }

}
