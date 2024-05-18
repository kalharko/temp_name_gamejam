using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Paramètres du jeu
    // Nombre de points de vie initial
    [SerializeField] private int initialHealth = 3;

    // Noms des attributs
    [SerializeField] private List<string> plates;
    [SerializeField] private List<string> filling;
    [SerializeField] private List<string> decorations;

    // Nombre d'assiettes
    [SerializeField] private int nb_Plates = 3;
    // Nombre de sushi
    [SerializeField] private int nb_Sushi = 3;
    // Nombre de décorations
    [SerializeField] private int nb_Decorations = 3;

    // Variables du jeu
    // Nombre de points de vie actuel
    private int health;
    private List<List<List<string>>> rules;


    // Start is called before the first frame update
    void Start()
    {
        rules = new List<List<List<string>>>();
        // rules.Add(new List<List<string>>{new List<string>{"square"}, new List<string>{}, new List<string>{}});
        rules.Add(new List<List<string>> { new() { "square" }, new() { }, new() { } });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddRule()
    {
        int index_of_new_rule = rules.Count;

        // sanity check, we can't have more than 7 rulse
        if (index_of_new_rule >= 7)
        {
            Debug.Log("Can't add more rules");
            return;
        }

        // for i in range(index_of_new_rule):
        List<List<string>> new_rule = new List<List<string>> { new() { }, new() { }, new() { } };

        for (int i = 0; i <= index_of_new_rule; i++)
        {
            // get a random number between 0 and 1
            float random_number = Random.Range(0f, 1f);
            if (random_number < 0.33)
            {
                // add a constraint to the first list
                new_rule[0].Add(plates[Random.Range(0, plates.Count)]);
            }
            else if (random_number < 0.66)
            {
                // add a constraint to the second list
                new_rule[1].Add(filling[Random.Range(0, filling.Count)]);
            }
            else
            {
                // add a constraint to the third list
                new_rule[2].Add(decorations[Random.Range(0, decorations.Count)]);
            }
        }
    }

    public bool CheckSushiAgainstRule(List<string> sushi, int rule_index)
    {
        List<List<string>> rule = rules[rule_index];

        // if the sushi is valid for newer rules, cant be valid for this rule
        if (rule_index + 1 < rules.Count)
        {
            if (CheckSushiAgainstRule(sushi, rule_index + 1))
            {
                return false;
            }
        }

        // Check if the sushi is valid for this rule
        for (int i = 0; i < 3; i++)
        {
            if (rule[i].Count > 0)
            {
                if (!rule[i].Contains(sushi[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public List<List<string>> GetRuleAtIndex(int index)
    {
        return rules[index];
    }

    public string GetRuleDisplayText()
    {
        // TODO : Implementer
        return "";
    }

    // TODO : Fonction qui retourne les images associées à une règle
}
