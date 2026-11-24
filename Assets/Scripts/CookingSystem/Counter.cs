
using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

    [SerializeField] private GameObject IngredientSingle;

    [SerializeField] private Transform parentAllIngredients;

    [SerializeField] private float clickWindow = 0.3f;

    private int clickCount = 1;
    private Coroutine clickCoroutine;

    void OnMouseDown() {

        GameObject spawnedngredient = SpawnIngredient(IngredientSingle, parentAllIngredients);

        spawnedngredient.GetComponent<Ingredient>().SetIngredientCount(clickCount);
        /*
        clickCount++;

        if (clickCoroutine == null) {
            clickCoroutine = StartCoroutine(ProcessClicks());
        }*/
    }

    private IEnumerator ProcessClicks() {
        
        yield return new WaitForSeconds(clickWindow);

        GameObject spawnedngredient = SpawnIngredient(IngredientSingle, parentAllIngredients);

        spawnedngredient.GetComponent<Ingredient>().SetIngredientCount(clickCount);
        

        clickCount = 0;
        clickCoroutine = null;
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
}
