using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameObject des la belt.
    // belt doit exposer:
    // - une fonction pour ajouter un objet sur la belt
    // - une fonction accelerer la vitesse de défilement de la belt
    [SerializeField] private GameObject belt;

    // GameObject des tables.
    // tables doit exposer:
    // - une fonction pour ajouter une règle
    [SerializeField] private GameObject tables;

    // Paramètres du jeu
    // Nombre de points de vie initial
    [SerializeField] private int initialHealth = 3;
    
    // Variables du jeu
    // Nombre de points de vie actuel
    private int health;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
