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


    
}


