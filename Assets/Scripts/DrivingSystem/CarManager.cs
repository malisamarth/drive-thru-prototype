
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    public static CarManager Instance { get; private set; }

    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private Transform orderPoint;
    [SerializeField] private Transform moneyPoint;
    [SerializeField] private Transform takeOutPoint;
    [SerializeField] private Transform carDeletePoint;
    [SerializeField] private GameObject carPrefab;

    private readonly Queue<Car> carQueue = new Queue<Car>();

    private void Awake() {

        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        PlaugeOfCars();

    }

    private void PlaugeOfCars() {


        for (int i = 0; i < 60; i++) {

            SpawnCar();

        }

    }



    public void SpawnCar() {

        if (carPrefab == null) {
            Debug.LogError("Car prefab reference is missing in CarManager.");
            return;
        }

        Car newCar = Instantiate(
            carPrefab,
            carSpawnPoint.position,
            Quaternion.identity
        ).GetComponent<Car>();

        if (newCar == null) {
            Debug.LogError("The car prefab is missing its Car component.");
            return;
        }

        // The previous last car is directly ahead of this new car.
        Car carAhead = null;

        foreach (Car car in carQueue) {
            if (car != null)
                carAhead = car;
        }

        newCar.carAhead = carAhead;
        carQueue.Enqueue(newCar);
    }

    public void ProcessNextCar() {
        if (carQueue.Count == 0)
            return;

        Car exitingCar = carQueue.Dequeue();

        if (exitingCar != null)
            Destroy(exitingCar.gameObject);

        // Rebuild the car-ahead relationships after the removal.
        Car previousCar = null;

        foreach (Car car in carQueue) {
            if (car == null)
                continue;

            car.carAhead = previousCar;
            previousCar = car;
        }
    }

    public Transform GetOrderPoint() => orderPoint;
    public Transform GetMoneyPoint() => moneyPoint;
    public Transform GetTakeOutPoint() => takeOutPoint;
    public Transform GetCarSpawnPoint() => carSpawnPoint;
    public Transform GetCarDeletePoint() => carDeletePoint;

}

public enum CarState {
    DrivingToOrder,
    WaitingForOrder,
    DrivingToMoney,
    WaitingToPay,
    DrivingToTakeOut,
    WaitingForTakeOut,
    Exiting
}
