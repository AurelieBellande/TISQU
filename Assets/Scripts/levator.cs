using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levator : MonoBehaviour
{

    [SerializeField] public Transform teleport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleport.position;
        }
    }


    /* public enum TriggerType { Enter, Exit };

     [Tooltip("The Transform to teleport to")]
     [SerializeField] Transform teleportTo;

     [Tooltip("The filter Tag")]
     [SerializeField] string tagy = "Player";

     [Tooltip("Trigger Event to Teleport")]
     [SerializeField] TriggerType type;

     void OnTriggerEnter2D(Collider2D other)
     {
         if (type != TriggerType.Enter)
             return;

         if (tagy == string.Empty || other.CompareTag(tagy))
             other.transform.position = teleportTo.position;
     }

     void OnTriggerExit2D(Collider2D other)
     {
         if (type != TriggerType.Exit)
             return;

         if (tagy == string.Empty || other.CompareTag(tagy))
             other.transform.position = teleportTo.position;
     }*/
}


