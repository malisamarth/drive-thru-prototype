using TMPro;
using UnityEngine;

public class POSManager : MonoBehaviour {

    public static POSManager Instance { get; private set; }

    [Header("Quantity Text")]
    [SerializeField] private TextMeshProUGUI burgerQuantiy;
    [SerializeField] private TextMeshProUGUI friesQuantiy;
    [SerializeField] private TextMeshProUGUI colaQuantiy;

    [Header("Item Cost Text")]
    [SerializeField] private TextMeshProUGUI burgerCostText;
    [SerializeField] private TextMeshProUGUI friesCostText;
    [SerializeField] private TextMeshProUGUI colaCostText;
    [SerializeField] private TextMeshProUGUI totalCostText;

    [SerializeField] private TextMeshProUGUI billNumberText;

    [SerializeField] private TextMeshProUGUI customerNameText;

    [SerializeField] private GameObject posPanel;

    [SerializeField] private GameObject Counters;

    private const double BURGER_RATE = 5.00;
    private const double FRIES_RATE = 2.50;
    private const double COLA_RATE = 1.50;

    private double burgerCost;
    private double friesCost;
    private double colaCost;
    private double totalCost;

    private int burgerCount;
    private int friesCount;
    private int colaCount;

    private Car currentCar;
    private string customerName;




    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        ResetBill();
    }


    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Tab)) {
            posPanel.SetActive(!posPanel.activeSelf);

            //Hold with Ducttape. Dumb Logic!!
            Counters.SetActive(!posPanel.activeSelf);
        }


    }


    private void UpdateBill() {
        // Update quantities
        burgerQuantiy.text = $"x{burgerCount} Burger";
        friesQuantiy.text = $"x{friesCount} Fries";
        colaQuantiy.text = $"x{colaCount} Cola";

        // Calculate individual costs
        burgerCost = burgerCount * BURGER_RATE;
        friesCost = friesCount * FRIES_RATE;
        colaCost = colaCount * COLA_RATE;

        // Update individual costs
        burgerCostText.text = $"${burgerCost:F2}";
        friesCostText.text = $"${friesCost:F2}";
        colaCostText.text = $"${colaCost:F2}";

        // Calculate and update total
        totalCost = burgerCost + friesCost + colaCost;
        totalCostText.text = $"Total: ${totalCost:F2}";
    }

    public void SetCustomerName(string name) {
        customerNameText.text = name;
    }

    private void ResetBill() {
        burgerCount = 0;
        friesCount = 0;
        colaCount = 0;
        SetCustomerName("--------");
        billNumberText.text = "--------";
        UpdateBill();
    }


    public void AddBurger() {
        burgerCount++;
        UpdateBill();
    }

    public void AddFries() {
        friesCount++;
        UpdateBill();
    }

    public void AddCola() {
        colaCount++;
        UpdateBill();
    }

    public void RemoveBurger() {
        if (burgerCount > 0)
            burgerCount--;

        UpdateBill();
    }

    public void RemoveFries() {
        if (friesCount > 0)
            friesCount--;

        UpdateBill();
    }

    public void RemoveCola() {
        if (colaCount > 0)
            colaCount--;

        UpdateBill();
    }

    public void ResetPOS() {
        burgerCount = 0;
        friesCount = 0;
        colaCount = 0;

        UpdateBill();
    }

    public void SetCurrentCar(Car car, string name) {
        currentCar = car;
        customerName = name;
        SetCustomerName(customerName);
        billNumberText.text = CustomerInfoGenerator.Instance.GetUniqueBillNumber().ToString();
    }

    public void SendBillToCar() {
        if (currentCar == null) {
            Debug.LogWarning("No car is currently at the POS!");
            return;
        }

        Debug.Log("Sending bill to car: " + currentCar.name);

        OrderSystem.Instance.ReicevedRequestData(currentCar, customerName, totalCost);

        ResetBill();
    }

}
