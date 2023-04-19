using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2ai : MonoBehaviour
{

    //Cette ia fait des aller retours sur la plateforme ou elle
    // est placer avec le ground dectetion
    // normalement des qu'elle voit un joueur elle lui fonce dessus
    //ce script est utilisable si on veut faire des enemies qui font des allers retours basiques
    // ex des goumbas ou des robots

    [SerializeField] float speed = 10f;
    [SerializeField] float distance = 2f;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform target;

  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

       /* if(Input.GetButtonDown("R"))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }*/

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

}