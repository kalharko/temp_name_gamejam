using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBehavior : MonoBehaviour
{

    // tables
    [SerializeField] private List<GameObject> tables;

    // Popup reference
    [SerializeField] private PopUpBehavior popup;

    // New rule interval
    [SerializeField] private float newRuleInterval = 25.0f;
    private float timeSinceLastRule = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastRule = newRuleInterval - 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Ajouter le temps écoulé depuis la dernière règle
        timeSinceLastRule += Time.deltaTime;
        // Si le temps depuis la dernière règle est supérieur à l'intervalle de temps
        if (timeSinceLastRule > newRuleInterval)
        {
            // Ajouter une règle
            AddRule();
            // Reset le temps depuis la dernière règle
            timeSinceLastRule = 0.0f;
        }
    }

    // Fonction pour ajouter une règle
    public void AddRule()
    {
        Debug.Log("Adding rule");
        GameManager.Instance.AddRule();
        int table_index = GameManager.Instance.rules.Count - 2;
        tables[table_index].SetActive(true);
        popup.OpenPopUp(table_index);
    }

}
