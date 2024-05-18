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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
