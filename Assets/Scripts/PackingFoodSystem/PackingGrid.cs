using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class PackingGrid : MonoBehaviour {

    [SerializeField] private GameObject takeOutFood;
    [SerializeField] private Transform spwanTakeOutFood;

    [Header("Grid Settings")]
    [SerializeField] private int columns = 4;
    [SerializeField] private int rows = 2;
    [SerializeField] private float cellSpacing = 1f;

    [Header("References")]
    [SerializeField] private GameObject cellPrefab;

    private PackingCell[,] cells;


    private int burgerCount = 0;
    private int friesCount = 0;
    private int colaCount = 0;

    private void Start() {
        GenerateGrid();
    }

    private void GenerateGrid() {

        cells = new PackingCell[columns, rows];

        for (int y = 0; y < rows; y++) {

            for (int x = 0; x < columns; x++) {

                Vector3 position = transform.position + new Vector3(x * cellSpacing, -y * cellSpacing, 0f);

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);

                cell.name = $"Cell_{x}_{y}";

                PackingCell packingCell = cell.GetComponent<PackingCell>();

                if (packingCell != null) {
                    packingCell.Initialize(x, y);
                    cells[x, y] = packingCell; // Store the cell in the grid array
                }


            }
        }
    }


    public bool TryGetPlacement(FoodType foodType, Vector3 dropPosition, int width, int height, out Vector3 placementPosition, out int startX, out int startY) {
        
        placementPosition = Vector3.zero;
        startX = -1;
        startY = -1;

        // Find the nearest cell center to the dropped item's center.
        float bestDistance = float.MaxValue;
        int centerX = -1;
        int centerY = -1;

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                Vector3 cellPosition = cells[x, y].transform.position;
                float distance = Vector2.Distance(dropPosition, cellPosition);

                if (distance < bestDistance) {
                    bestDistance = distance;
                    centerX = x;
                    centerY = y;
                }
            }
        }

        // Convert the selected center cell to the footprint's start cell.
        startX = centerX - (width - 1) / 2;
        startY = centerY - (height - 1) / 2;

        if (!CanPlaceItem(startX, startY, width, height)) {
            return false;
        }

        // Calculate the center of the footprint using actual cell positions.
        Vector3 firstCellPosition = cells[startX, startY].transform.position;
        Vector3 lastCellPosition = cells[startX + width - 1, startY + height - 1].transform.position;

        placementPosition = (firstCellPosition + lastCellPosition) / 2f;
        placementPosition.z = dropPosition.z;


        return true;
    }


    public void EmptyTheCell(int startX, int startY, int width, int height) {
        for (int x = startX; x < startX + width; x++) {
            for (int y = startY; y < startY + height; y++) {
                cells[x, y].SetOccupied(false);
            }
        }
    }



    public bool CanPlaceItem(int startX, int startY, int width, int height) {
        // Check grid boundaries.
        if (startX < 0 || startY < 0 ||
            startX + width > columns ||
            startY + height > rows) {
            return false;
        }

        // Check every cell the item needs.
        for (int x = startX; x < startX + width; x++) {
            for (int y = startY; y < startY + height; y++) {
                if (cells[x, y] == null || cells[x, y].IsOccupied) {
                    return false;
                }
            }
        }

        return true;
    }

    public bool PlaceItem(int startX, int startY, int width, int height) {
        // First, make sure the entire footprint is available.
        if (!CanPlaceItem(startX, startY, width, height)) {
            return false;
        }

        // Reserve every cell occupied by this item.
        for (int x = startX; x < startX + width; x++) {
            for (int y = startY; y < startY + height; y++) {
                cells[x, y].SetOccupied(true);
            }
        }

        return true;
    }



    public void UpdateFoodCount(FoodType foodType, int amount) {
        
    

        switch (foodType) {

            case FoodType.Burger:

                burgerCount += amount;

                if (burgerCount < 0) {
                    burgerCount = 0;
                }

                break;

            case FoodType.Fries:

                friesCount += amount;

                if (friesCount < 0) {
                    friesCount = 0;
                }

                break;


            case FoodType.Coke:

                colaCount += amount;

                if (colaCount < 0) {
                    colaCount = 0;
                }

                break;
        }

        Debug.Log($"Grid Food Counts → Burger: {burgerCount}, Fries: {friesCount}, Coke: {colaCount}");


    }

    public void ClearGrid() {

        // Destroy all food items in the scene that are packed.
        PackableFood[] packedFoods = FindObjectsByType<PackableFood>(FindObjectsSortMode.None);

        foreach (PackableFood food in packedFoods) {
            if (food.IsPacked) {
                Destroy(food.gameObject);
            }
        }

        // Make every cell empty.
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                if (cells[x, y] != null) {
                    cells[x, y].SetOccupied(false);
                }
            }
        }

        GenerateTakeOut();

        // Reset all food counters.
        burgerCount = 0;
        friesCount = 0;
        colaCount = 0;

        Debug.Log("Grid cleared! All food removed and counters reset.");
    }

    private void GenerateTakeOut() {



        GameObject newTakeOutFood = Instantiate(takeOutFood, spwanTakeOutFood.position, Quaternion.identity);

        newTakeOutFood.GetComponent<TakeOutFoodVisual>().SetFoodCount(burgerCount, friesCount, colaCount);


    }
    



}
