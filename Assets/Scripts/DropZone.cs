using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DropZone : MonoBehaviour
{
    [SerializeField] private int id;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sushi"))
        {
            GameManager.Instance.IsSushiInRange = true;
            List<string> conditions = collision.GetComponentInParent<SushiBehavior>().Appearance;
            GameManager.Instance.IsSushiValid = GameManager.Instance.CheckSushiAgainstRule(conditions, id);
        }
    }
}