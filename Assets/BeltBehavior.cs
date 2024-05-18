using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBehavior : MonoBehaviour
{

    // Prefab des sushis
    [SerializeField] private GameObject sushiPrefab;

    // Interval de temps entre chaque accélération de la belt
    [SerializeField] private float accelerationInterval = 10.0f;
    // Interval de temps entre spawn de sushi
    [SerializeField] private float spawnInterval = 1.0f;
    
    // Temps écoulé depuis la dernière accélération
    private float timeSinceLastAcceleration = 0.0f;
    // Temps écoulé depuis le dernier spawn de sushi
    private float timeSinceLastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
