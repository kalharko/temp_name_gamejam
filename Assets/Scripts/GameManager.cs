using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Paramètres du jeu
    // Nombre de points de vie initial
    [SerializeField] private int initialHealth = 3;

    // Référence aux assets du sushi
    [SerializeField] public List<Sprite> sprite_plates;
    [SerializeField] public List<Sprite> sprite_fillings;
    [SerializeField] public List<Sprite> sprite_toppings;

    // Variables du jeu
    // Nombre de points de vie actuel
    private int health;
    private List<List<List<string>>> rules;

    private int score;

    public GameObject DraggedSushi { get; set; } = null;
    public bool IsSushiInRange { get; set; }
    public bool IsSushiValid { get; set; }
    public int Health { get; set; }

    public int Score
    {
        get => score;
        set
        {
            if (value > 0)
            {
                score = value;
            }
        }
    }
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rules = new List<List<List<string>>>();
        // rules.Add(new List<List<string>>{new List<string>{"square"}, new List<string>{}, new List<string>{}});
        rules.Add(new List<List<string>> { new() { sprite_plates[0].name }, new() { }, new() { } });

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
                new_rule[0].Add(sprite_plates[Random.Range(0, sprite_plates.Count)].name);
            }
            else if (random_number < 0.66)
            {
                // add a constraint to the second list
                new_rule[1].Add(sprite_fillings[Random.Range(0, sprite_fillings.Count)].name);
            }
            else
            {
                // add a constraint to the third list
                new_rule[2].Add(sprite_toppings[Random.Range(0, sprite_toppings.Count)].name);
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
    
    public List<List<List<string>>> GetRules()
    {
        return rules;
    }

    // TODO : Fonction qui retourne les images associées à une règle
}
