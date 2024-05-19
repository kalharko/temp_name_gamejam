using UnityEngine;
using Random = System.Random;

public class DropZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sushi"))
        {
            GameManager.Instance.IsSushiInRange = true;

            var random = new Random();
            int number = random.Next(1, 100);
            
            /* TODO
             *
             * Le joueur réussit 70% du temps
             * A changer lors de l'ajout de la règle des validations
             */
            GameManager.Instance.IsSushiValid = number >= 30;
        }
    }
}