using System.Collections.Generic;
using UnityEngine;

public class CustomerInfoGenerator : MonoBehaviour {
    
    public static CustomerInfoGenerator Instance { get; private set; }

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        availableNames = new List<string>(customerNames);
    }

    [SerializeField] private string[] customerNames = 
    {
        "Samarth Mali",
        "Jon Snow",
        "Daenerys Targaryen",
        "Tyrion Lannister",
        "Arya Stark",
        "Sansa Stark",
        "Bran Stark",
        "Robb Stark",
        "Ned Stark",
        "Catelyn Stark",
        "Cersei Lannister",
        "Jaime Lannister",
        "Tywin Lannister",
        "Joffrey Baratheon",
        "Theon Greyjoy",
        "Yara Greyjoy",
        "Samwell Tarly",
        "Brienne of Tarth",
        "Sandor Clegane",
        "Gregor Clegane",
        "Petyr Baelish",
        "Varys",
        "Davos Seaworth",
        "Melisandre",
        "Jorah Mormont",
        "Khal Drogo",
        "Missandei",
        "Sakshi Meshram",
        "Margaery Tyrell",
        "Ramsay Bolton"
    };

    private List<string> availableNames;
    private HashSet<long> usedBillNumbers = new HashSet<long>();

    public string GetUniqueCustomerName() {

        if (availableNames.Count == 0) {
            availableNames = new List<string>(customerNames);
        }

        if (availableNames.Count == 0) {
            Debug.LogWarning("No customer names configured!");
            return "Customer";
        }

        int randomIndex = Random.Range(0, availableNames.Count);
        string selectedName = availableNames[randomIndex];

        availableNames.RemoveAt(randomIndex);

        return selectedName;
    }


    public string GetUniqueBillNumber() {
        // Keep your existing unique-number generation logic here.
        long billNumber;

        do {
            billNumber = (long)Random.Range(100000000, 1000000000)
                         * 10 + Random.Range(0, 10);
        }
        while (!usedBillNumbers.Add(billNumber));

        return "#" + billNumber;
    }

}
