using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField] private int id;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Sushi"))
        {
            return;
        }

        List<string> conditions = collision.GetComponentInParent<SushiBehavior>().Appearance;
            
        if (GameManager.Instance.CheckSushiAgainstRule(conditions, id))
        {
            
        }
    }
}
