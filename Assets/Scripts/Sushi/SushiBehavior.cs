using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Splines;

public class SushiBehavior : MonoBehaviour
{
    // Référence aux assets d'assiettes
    public 

    // Référence à la spline
    private SplineContainer spline;

    // Vitesse du sushi
    public float speed = 1.0f;

    // Position du sushi sur la spline
    private float positionOnSpline = 0.0f;
    private bool following_spline = true;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère la référence vers la spline
        spline = GetComponentInParent<SplineContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        // On fait avancer le sushi
        if (following_spline)
        {
            positionOnSpline += speed * Time.deltaTime;
            transform.position = spline.EvaluatePosition(positionOnSpline);
        }
    }

    void StopFollowingSpline()
    {
        following_spline = false;
    }

    void SelectApearance() {
        // TODO: Récuperer la liste de règles du game manager
        // List<List<string>> rules = GameManager.GetRules();
    }
}
