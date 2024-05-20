using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public float speed;
    public float minX;
    public float maxX;

    public float maxY;
    private Vector3 targetPosition;

    void Start()
    {
        // Set a random initial position within the bounds
        transform.position = new Vector3(Random.Range(minX, maxX),transform.position.y, transform.position.z);

    }

    void Update()
    {
        Debug.Log("affiche le résultat");
        // Move towards the target position
         transform.position = new Vector3(transform.position.x,transform.position.y -1 * Time.deltaTime * speed, transform.position.z);

        // If the object has reached the target position, set a new random target position
        if (transform.position.y < maxY)
        {

             transform.position = new Vector3(Random.Range(minX, maxX), - maxY, transform.position.z);
            //targetPosition = new Vector3(transform.position.x, Random.Range(minY, maxY), transform.position.z);
        }
    }
}