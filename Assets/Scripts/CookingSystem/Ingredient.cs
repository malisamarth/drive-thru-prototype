using TMPro;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    [SerializeField] private Ingredients ingredient;

    [SerializeField] private TextMeshProUGUI ingredientCountText;

    private int ingredientCount;



    public void TriggeredWith(Collider2D collision) {

        if ("Bin" == collision.gameObject.name) {

            Destroy(gameObject);

        }


        //Debug.Log(ingredient + " triggered with " + collision.gameObject.name);

    }


    public void SetIngredientCount(int count) {

        ingredientCount = count;

        SetIngredientCountVisual(ingredientCount);

    }

    private void SetIngredientCountVisual(int count) {
        ingredientCountText.text = "X" + count.ToString();
    }

    public Ingredients GetIngredient() {
        return ingredient;
    }


    public void DestroyIngredient() {
        
        Destroy(gameObject);

    } 

}

public enum Ingredients {
    Bun,
    Patty,
    Lettuce,
    Cheese,
    Potato,
    Oil,
    Cola
}

public enum FoodType {
    Burger,
    Fries,
    Coke
}