
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cheff : MonoBehaviour {

    [SerializeField] private int FoodCookingCapacity = 5;

    private const int MAX_BURGER_COUNT = 5;
    private const int MAX_FRIES_COUNT = 5;
    private const int MAX_COLA_COUNT = 5;


    private List<List<Ingredients>> BurgerStations = new();
    private List<List<Ingredients>> FriesStations = new();
    private List<List<Ingredients>> ColaStations = new();

    private Ingredients[] BurgerRecipe =
    {
        Ingredients.Bun,
        Ingredients.Patty,
        Ingredients.Lettuce,
        Ingredients.Cheese
    };

    private Ingredients[] FriesRecipe =
    {
        Ingredients.Potato,
        Ingredients.Oil
    };

    private Ingredients[] ColaRecipe =
    {
        Ingredients.Cola
    };

    private int burgerCount;
    private int friesCount;
    private int colaCount;

    public event Action<Dictionary<FoodType, int>> OnFoodCountIncreased;

    private void Awake() {

        for (int i = 0; i < FoodCookingCapacity; i++) {
            BurgerStations.Add(new List<Ingredients>());
            FriesStations.Add(new List<Ingredients>());
            ColaStations.Add(new List<Ingredients>());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Ingredient component = collision.GetComponent<Ingredient>();

        if (component == null) {
            return;
        }

        Ingredients incoming = component.GetIngredient();

        if (BurgerRecipe.Contains(incoming)) {

            if (burgerCount >= MAX_BURGER_COUNT) {

                Debug.Log("Burger requests are full");

                return;
            }

            AddIngredient(BurgerStations, BurgerRecipe, incoming, FoodType.Burger, collision.gameObject);
        
        } else if (FriesRecipe.Contains(incoming)) {

            if (friesCount >= MAX_FRIES_COUNT) {

                Debug.Log("Fries requests are full");

                return;
            }

            AddIngredient(FriesStations, FriesRecipe, incoming, FoodType.Fries, collision.gameObject);
        
        } else if (ColaRecipe.Contains(incoming)) {

            if (colaCount >= MAX_COLA_COUNT) {

                Debug.Log("Cola requests are full");

                return;
            }

            AddIngredient(ColaStations, ColaRecipe, incoming, FoodType.Coke, collision.gameObject);
        
        } else {

            Debug.LogWarning("Unknown ingredient: " + incoming);
        
        }
    }

    private void AddIngredient(List<List<Ingredients>> stations, Ingredients[] recipe, Ingredients incoming, FoodType foodName, GameObject IngredientObject) {
       
        // Find stations that don't already contain this ingredient.
        List<Ingredients> bestStation = null;

        foreach (List<Ingredients> station in stations) {
            if (station.Contains(incoming))
                continue;

            if (station.Count >= recipe.Length)
                continue;

            if (bestStation == null || station.Count > bestStation.Count)
                bestStation = station;
        }

        if (bestStation == null) {
            
            Debug.LogWarning("Chef can't accept more " + incoming + "!");

            return;
        }

        // Add to the station closest to completion.
        bestStation.Add(incoming);

        //Here we DELETE the Ingredient!!
        IngredientObject.GetComponent<Ingredient>().DestroyIngredient();


        Debug.Log(foodName + " station: " +
                  string.Join(", ", bestStation));

        // Check whether the recipe is complete.
        if (recipe.All(item => bestStation.Contains(item))) {

            switch (foodName) {
                case FoodType.Burger:
                    burgerCount++;
                    break;

                case FoodType.Fries:
                    friesCount++;
                    break;

                case FoodType.Coke:
                    colaCount++;
                    break;
            }


            //SendingDataToTimer
            SendDataToCookingTime();

            Debug.Log(foodName + " completed! Total: " +
                      GetCount(foodName));

            // Reset this station for the next order.
            bestStation.Clear();
        }
    }

    public int GetCount(FoodType foodName) {
        switch (foodName) {
            case FoodType.Burger: return burgerCount;
            case FoodType.Fries: return friesCount;
            case FoodType.Coke: return colaCount;
            default: return 0;
        }
    }

    public Dictionary<FoodType, int> GetAllFoodCounts() {

        Dictionary<FoodType, int> foodCounts = new Dictionary<FoodType, int>
        {
            { FoodType.Burger, GetCount(FoodType.Burger)},
            { FoodType.Fries, GetCount(FoodType.Fries)},
            { FoodType.Coke, GetCount(FoodType.Coke)}
        };


        return foodCounts;
       
    }


    private void SendDataToCookingTime() {

        OnFoodCountIncreased?.Invoke(GetAllFoodCounts());

    }

    public void OrderCompleted(FoodType foodType) {

        switch (foodType) {

            case FoodType.Burger:
                burgerCount = Mathf.Max(0, burgerCount - 1);
                break;

            case FoodType.Fries:
                friesCount = Mathf.Max(0, friesCount - 1);
                break;

            case FoodType.Coke:
                colaCount = Mathf.Max(0, colaCount - 1);
                break;

            default:
                Debug.LogError("Unknown food type!");
                return;
        }

        Debug.Log(foodType + " :- Completed");

        SendDataToCookingTime();
    }


}

