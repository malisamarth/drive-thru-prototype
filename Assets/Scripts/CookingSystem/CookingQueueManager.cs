
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingQueueManager : MonoBehaviour {

    [SerializeField] private GameObject idleCheffVisual;
    [SerializeField] private GameObject cookingCheffVisual;


    [Header("Cooking Times")]
    [SerializeField] private float burgerTimer = 3f;
    [SerializeField] private float friesTimer = 2f;
    [SerializeField] private float colaTimer = 1f;

    private Queue<FoodType> cookingQueue = new Queue<FoodType>();

    private Cheff cheff;
    private bool isCooking;

    private Dictionary<FoodType, int> previousCounts;

    [SerializeField] CookingVisualTimer cookingVisualTimer;

    private void Awake() {
        cheff = GetComponent<Cheff>();

        if (cheff == null) {
            Debug.LogError("CookingQueueManager needs Cheff on the same GameObject!");
            enabled = false;
            return;
        }

        previousCounts = cheff.GetAllFoodCounts();
    }

    private void OnEnable() {
        if (cheff != null) {
            cheff.OnFoodCountIncreased += Cheff_OnFoodCountIncreased;
        }
    }

    private void OnDisable() {
        if (cheff != null) {
            cheff.OnFoodCountIncreased -= Cheff_OnFoodCountIncreased;
        }
    }

    private void Cheff_OnFoodCountIncreased(
        Dictionary<FoodType, int> foodCounts) {
        foreach (FoodType foodType in foodCounts.Keys) {
            int previousCount = previousCounts[foodType];
            int currentCount = foodCounts[foodType];

            // An increase means newly prepared food.
            if (currentCount > previousCount) {
                int newItems = currentCount - previousCount;

                for (int i = 0; i < newItems; i++) {
                    cookingQueue.Enqueue(foodType);

                    Debug.Log("Added to cooking queue: " + foodType);
                }
            }

            // Update the snapshot for increases AND decreases.
            previousCounts[foodType] = currentCount;
        }

        // Start cooking if the station is idle.
        if (!isCooking && cookingQueue.Count > 0) {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue() {
        isCooking = true;

        while (cookingQueue.Count > 0) {
            FoodType currentFood = cookingQueue.Dequeue();

            float cookingTime = GetCookingTime(currentFood);

            cookingVisualTimer.StartCookingTimer(cookingTime);


            Debug.Log("Cooking started: " + currentFood);

            idleCheffVisual.SetActive(false);
            cookingCheffVisual.SetActive(true);

            yield return new WaitForSeconds(cookingTime);

            Debug.Log("Cooking finished: " + currentFood);

            idleCheffVisual.SetActive(true);
            cookingCheffVisual.SetActive(false);

            // Tell the Chef that this food is finished.
            cheff.OrderCompleted(currentFood);

            //Tell CompletedOrderSection to increase count.
            CompletedOrderSection.Instance.IncreaseCookedFoodCounter(currentFood);



            // The next item starts only after this one finishes.
        }

        isCooking = false;
    }

    private float GetCookingTime(FoodType foodType) {
        switch (foodType) {
            case FoodType.Burger:
                return burgerTimer;

            case FoodType.Fries:
                return friesTimer;

            case FoodType.Coke:
                return colaTimer;

            default:
                return 0f;
        }
    }
}
