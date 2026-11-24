using UnityEngine;

public class PaymentRequest : MonoBehaviour {

    [SerializeField] private GameObject creditCard;

    [SerializeField] private Transform spawnPoint;

    

    public GameObject SpawnCreditCardVisual() {
        return Instantiate(creditCard, spawnPoint.position, spawnPoint.rotation, MoneySystem.Instance.GetCreditCardParent().transform);
    }

}