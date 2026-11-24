using TMPro;
using UnityEngine;

public class CookedFoodItems : MonoBehaviour {

    [SerializeField] private FoodType cookedFoodType;

    [SerializeField] private GameObject IngredientSingle;

    [SerializeField] private Transform parentAllIngredients;

    [SerializeField] private TextMeshProUGUI cookedFoodCountText;

    private int cookedFoodCount = 0;


    private void Start() {

        cookedFoodCount = 0;

        SetCookedFoodCountText(cookedFoodCount);

        CompletedOrderSection.Instance.OnCookedFoodIcrease += Instance_OnCookedFoodIcrease;

    }

    private void Instance_OnCookedFoodIcrease(object sender, FoodType incomingCookedFood) {
        
        if (cookedFoodType == incomingCookedFood) {
            cookedFoodCount++;
            SetCookedFoodCountText(cookedFoodCount);
        }


    }

    void OnMouseDown() {

        if (cookedFoodCount <= 0) {

            Debug.Log(cookedFoodType + " :- No cooked food" );

            return;
        }

        SpawnIngredient(IngredientSingle, parentAllIngredients);
        cookedFoodCount--;
        SetCookedFoodCountText(cookedFoodCount);
    }


    private GameObject SpawnIngredient(GameObject prefab, Transform parent) {
        Vector3 spawnPosition = GetMouseWorldPosition();
        spawnPosition.z = 0f;

        GameObject ingredient = Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity,
            parent
        );

        Drag ingredientDrag = ingredient.GetComponent<Drag>();

        if (ingredientDrag != null) {
            ingredientDrag.StartDragging();
        }

        return ingredient;
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void SetCookedFoodCountText(int count) {

        cookedFoodCountText.text = "X" + count;

    }


}