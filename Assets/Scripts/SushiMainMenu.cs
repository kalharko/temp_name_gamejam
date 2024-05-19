using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{

    public float speed;
    public float stop;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(transform.position.x, transform.position.y + 1 *speed * Time.deltaTime, 0);
        if (transform.position.y > stop)
        {
            transform.position = new Vector3(transform.position.x, -stop, transform.position.z);
        }
    }

}

