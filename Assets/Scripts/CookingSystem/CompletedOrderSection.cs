using System;
using UnityEngine;

public class CompletedOrderSection : MonoBehaviour {

    public static CompletedOrderSection Instance { get; private set; }

    public event EventHandler<FoodType> OnCookedFoodIcrease;

    private int totalCookedBurgerCount = 0;
    private int totalCookedFriesCount = 0;
    private int totalCookedColaCount = 0;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void IncreaseCookedFoodCounter(FoodType foodType) {

        if (foodType == FoodType.Burger) {
            totalCookedBurgerCount++;
            SendDataToCookedCounters(FoodType.Burger);
        }

        if (foodType == FoodType.Fries) {
            totalCookedFriesCount++;
            SendDataToCookedCounters(FoodType.Fries);
        }

        if (foodType == FoodType.Coke) {
            totalCookedColaCount++;
            SendDataToCookedCounters(FoodType.Coke);
        }

    }

    private void SendDataToCookedCounters(FoodType food) {

        OnCookedFoodIcrease?.Invoke(this, food);

    }

}