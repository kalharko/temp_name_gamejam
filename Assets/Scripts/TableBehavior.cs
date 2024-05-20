using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBehavior : MonoBehaviour
{

    // tables
    [SerializeField] private List<GameObject> tables;
    [SerializeField] private List<GameObject> dropZones;

    // Popup reference
    [SerializeField] private PopUpBehavior popup;

    // New rule interval
    [SerializeField] private float newRuleInterval = 25.0f;
    private float timeSinceLastRule = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastRule = newRuleInterval - 1.0f;

        // Hide all tables
        foreach (GameObject table in tables)
        {
            table.SetActive(false);
        }

        // Hide all drop zones
        foreach (GameObject dropZone in dropZones)
        {
            dropZone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ajouter le temps écoulé depuis la dernière règle
        timeSinceLastRule += Time.deltaTime;
        // Si le temps depuis la dernière règle est supérieur à l'intervalle de temps
        if (timeSinceLastRule > newRuleInterval)
        {
            // Reset le temps depuis la dernière règle
            timeSinceLastRule = 0.0f;
            // Ajouter une règle
            AddRule();
        }
    }

    // Fonction pour ajouter une règle
    public void AddRule()
    {
        GameManager.Instance.AddRule();
        int table_index = GameManager.Instance.rules.Count - 2;
        tables[table_index].SetActive(true);
        dropZones[table_index].SetActive(true);
        popup.OpenPopUp(table_index);
    }

}
