using TMPro;
using UnityEngine;

public class PaymentMahcineVisual : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI customerName;
    [SerializeField] private TextMeshProUGUI totalOrderCost;

    [SerializeField] private TextMeshProUGUI moneyEarnedText;

    private float moneyEarned = 0f;

    public void UpdateCustomerName(string name) {

        Debug.Log("Updating customer name in PaymentMahcineVisual: " + name);

        customerName.text = name;
    }

    public void UpdateTotalOrderCost(float cost) {

        Debug.Log("Updating total order cost in PaymentMahcineVisual: " + cost);

        totalOrderCost.text = "$ " + cost.ToString();

    }

    public void UpdateMoneyEarned(float incoming) {

        moneyEarned += incoming;

        Debug.Log("Updating total money earned in PaymentMahcineVisual: " + moneyEarned);

        moneyEarnedText.text = "$ " + moneyEarned.ToString();
    }

}