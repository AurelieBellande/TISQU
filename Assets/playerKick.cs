using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerKick : MonoBehaviour
{
    public float kickForce = 10f; 
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformKick()
    {
        animator.SetTrigger("Kick"); 
    }

    public void ApplyKickForce()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f); // Adjust the radius as needed

        foreach (Collider collider in hitColliders)
        {
            
            if (collider.CompareTag("enemy"))
            {
                Rigidbody enemyRigidbody = collider.GetComponent<Rigidbody>();

                if (enemyRigidbody != null)
                {
                    
                    Vector3 kickDirection = (collider.transform.position - transform.position).normalized;

                    enemyRigidbody.AddForce(kickDirection * kickForce, ForceMode.Impulse);
                }
            }
            else if (collider.CompareTag("Boss"))
            {
                Rigidbody enemyRigidbody = collider.GetComponent<Rigidbody>();

                if (enemyRigidbody != null)
                {

                    Vector3 kickDirection = (collider.transform.position - transform.position).normalized;

                    enemyRigidbody.AddForce(kickDirection * kickForce, ForceMode.Impulse);
                }
            }
        }
    }
}
