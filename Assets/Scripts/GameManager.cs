using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Paramètres du jeu
    // Nombre de points de vie initial
    [SerializeField] private int initialHealth = 3;
    [SerializeField] private float slowdownDuration = 2f;
    [SerializeField] private GameObject newRulePopup;

    // Référence aux assets du sushi
    [SerializeField] public List<Sprite> sprite_plates;
    [SerializeField] public List<Sprite> sprite_fillings;
    [SerializeField] public List<Sprite> sprite_toppings;

    // Variables du jeu
    // Nombre de points de vie actuel
    private int health;
    public List<List<List<string>>> rules;

    private int score;

    public GameObject DraggedSushi { get; set; } = null;
    public bool IsSushiInRange { get; set; }
    public bool IsSushiValid { get; set; }
    public int Health { get; set; }

    //audio sources

    public AudioSource loseLifeAudioSource;
    public AudioClip loseLifeAudioClip;

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

    public static event Action<GameState> OnGameStateChanged;
    public GameState state;
    
    public static GameManager Instance { get; private set; }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        
        switch (newState)
        {
            case GameState.IsPlaying:
                break;
            case GameState.HasSucceeded:
                HandleSucceededState();
                break;
            case GameState.HasFailed:
                HandleFailedState();
                break;
            case GameState.IsGameOver:
                HandleGameOverState();
                break;
        }
        
        OnGameStateChanged?.Invoke(state);
    }

    private void HandleGameOverState()
    {
        newRulePopup.SetActive(false);
        StartCoroutine(SlowTimeUntilGameOver());
    }

    private IEnumerator SlowTimeUntilGameOver()
    {
        float startTime = Time.realtimeSinceStartup;
        float startScale = Time.timeScale;

        while (Time.timeScale > 0)
        {
            float elapsed = Time.realtimeSinceStartup - startTime;
            Time.timeScale = Mathf.Lerp(startScale, 0, elapsed / slowdownDuration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            yield return null;
        }

        Time.timeScale = 0;
    }

    private void HandleSucceededState()
    {
        // Hide plate content and increment score
        DraggedSushi.GetComponentInParent<SushiBehavior>().HidePlateContent();
        Score++;
        
        // Reset dragged sushi data
        IsSushiInRange = false;
        IsSushiValid = false;
    }

    private void HandleFailedState()
    {
        
    }

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
        AddFirstRule();

        // Init game data;
        Health = initialHealth;
        Score = 0;
        
        UpdateGameState(GameState.IsPlaying);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddFirstRule()
    {
        List<List<string>> new_rule = new List<List<string>> { new() {}, new() { }, new() { } };
        int random_index;
        // Add random plate
        new_rule[0].Add(sprite_plates[Random.Range(0, sprite_plates.Count)].name);
        // Add 2 random fillings
        random_index = Random.Range(0, sprite_fillings.Count);
        new_rule[1].Add(sprite_fillings[random_index].name);
        while (sprite_fillings[random_index].name == new_rule[1][0]) {
            random_index = Random.Range(0, sprite_fillings.Count);
        }
        new_rule[1].Add(sprite_fillings[random_index].name);
        // Add 1 random topping
        new_rule[2].Add(sprite_toppings[Random.Range(0, sprite_toppings.Count)].name);
        
        rules.Add(new_rule);
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

        // get sprites already used in rules
        List<string> used_plates = new List<string>();
        List<string> used_fillings = new List<string>();
        List<string> used_toppings = new List<string>();

        foreach (List<List<string>> rule in rules)
        {
            foreach (string plate in rule[0])
            {
                used_plates.Add(plate);
            }
            foreach (string filling in rule[1])
            {
                used_fillings.Add(filling);
            }
            foreach (string topping in rule[2])
            {
                used_toppings.Add(topping);
            }
        }

        // for i in range(index_of_new_rule):
        List<List<string>> new_rule = new List<List<string>> { new() { }, new() { }, new() { } };

        string randomDraw;
        
        for (int i = 0; i <= index_of_new_rule; i++)
        {
            // get a random number between 0 and 1
            float random_number = Random.Range(0f, 1f);
            if (random_number < 0.33)
            {
                // add a constraint to the first list
                randomDraw = sprite_plates[Random.Range(0, sprite_plates.Count)].name;
                new_rule[0].Add(randomDraw);
                
            }
            else if (random_number < 0.66)
            {
                // add a constraint to the second list
                randomDraw = sprite_fillings[Random.Range(0, sprite_fillings.Count)].name;
                new_rule[1].Add(randomDraw);
            }
            else
            {
                // add a constraint to the third list
                randomDraw = sprite_toppings[Random.Range(0, sprite_toppings.Count)].name;
                new_rule[2].Add(randomDraw);
            }
            
            Debug.Log(randomDraw);
        }
        
        rules.Add(new_rule);
    }

    public bool CheckSushiAgainstRule(List<string> sushi, int rule_index)
    {
        List<List<string>> rule = rules[rule_index];

        Debug.Log("Checking sushi" + sushi[0] + " " + sushi[1] + " " + sushi[2] + " against rule " + rule_index);

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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void IncrementScore(int value)
    {
        Score += value;
    }

    public void RemoveLifePoints(int value)
    {
    
        loseLifeAudioSource.PlayOneShot(loseLifeAudioClip);
        Health -= value;

        if (Health <= 0)
        {
            UpdateGameState(GameState.IsGameOver);
        }
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
public enum GameState
{
    IsPlaying,
    HasFailed,
    HasSucceeded,
    IsGameOver
}
