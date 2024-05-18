using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Splines;

public class SushiBehavior : MonoBehaviour
{
    // référence aux assets du sushi
    [SerializeField] private List<Sprite> sprite_plates;
    [SerializeField] private List<Sprite> sprite_fillings;
    [SerializeField] private List<Sprite> sprite_toppings;

    // référence aux assets du sushi
    [SerializeField] private SpriteRenderer plate;
    [SerializeField] private SpriteRenderer filling;
    [SerializeField] private SpriteRenderer topping;

    // Référence à la spline
    private SplineContainer spline;

    // Vitesse du sushi
    public float speed = 1.0f;

    // Position du sushi sur la spline
    private float positionOnSpline = 0.0f;
    private bool following_spline = true;
    private List<string> appearance;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère la référence vers la spline
        spline = GetComponentInParent<SplineContainer>();

        // On sélectionne l'apparence du sushi
        SelectApearance();

        // On set les assets du sushi en fonction de l'apparence
        ApplyAppearance();
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
        List<List<List<string>>> rules = GameManager.Instance.GetRules();

        // gather the list of possible sushi appearances
        List<string> possible_plates = new List<string>();
        List<string> possible_fillings = new List<string>();
        List<string> possible_topping = new List<string>(); 

        foreach (List<List<string>> rule in rules)
        {
            possible_plates.AddRange(rule[0]);
            possible_fillings.AddRange(rule[1]);
            possible_topping.AddRange(rule[2]);
        }

        // select a random appearance
        string plate = possible_plates[Random.Range(0, possible_plates.Count)];
        string filling = possible_fillings[Random.Range(0, possible_fillings.Count)];
        string topping = possible_topping[Random.Range(0, possible_topping.Count)];

        // store the appearance
        appearance = new List<string> { plate, filling, topping };
    }

    void ApplyAppearance() {

    }
}
