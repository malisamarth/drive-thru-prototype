using System;
using TMPro;
using UnityEngine;

public class CarOrderVisual : MonoBehaviour {

    [SerializeField] private GameObject burgerVisual;
    [SerializeField] private GameObject friesVisual;
    [SerializeField] private GameObject colaVisual;

    [SerializeField] private GameObject customerNameVisual;
    [SerializeField] private GameObject totalCostVisual;

    private void Start() {
        //customerNameVisual.SetActive(false);
        //totalCostVisual.SetActive(false);
    }

    public void SetOrderVisual(OrderRequest orderRequest) {

        TextMeshProUGUI burgerText = burgerVisual.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI friesText = friesVisual.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI colaText = colaVisual.GetComponentInChildren<TextMeshProUGUI>();

        burgerText.text = 'x' + orderRequest.BurgerCount.ToString();
        friesText.text = 'x' + orderRequest.FriesCount.ToString();
        colaText.text = 'x' + orderRequest.ColaCount.ToString();

        burgerVisual.SetActive(orderRequest.BurgerCount > 0);
        friesVisual.SetActive(orderRequest.FriesCount > 0);
        colaVisual.SetActive(orderRequest.ColaCount > 0);
    }

    public void SetCustomerInfo(string customerName, float totalCost) {
        SetCustomerName(customerName);
        SetTotalCost(totalCost);
    }

    private void SetCustomerName(string customerName) {
        TextMeshProUGUI customerNameText = customerNameVisual.GetComponentInChildren<TextMeshProUGUI>();
        customerNameText.text = customerName;
        customerNameVisual.SetActive(true);
    }

    private void SetTotalCost(float totalCost) {
        TextMeshProUGUI totalCostText = totalCostVisual.GetComponentInChildren<TextMeshProUGUI>();
        totalCostText.text = $"${totalCost:F2}";
        totalCostVisual.SetActive(true);
    }

    public void HideOrderCostInfo() {
        totalCostVisual.SetActive(false);
    }

}