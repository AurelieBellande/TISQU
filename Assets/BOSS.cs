using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    float speed = 8f;
    Vector3 targetPos;
    
    public HealthManager healthBar2;

    int pointIndex;
    int pointCount;
    int direction = 1;
    float step;

    [SerializeField] Transform Playertarget;
    float minimumDistance;


   
    // Start is called before the first frame update
    void Start()
    {
       /* pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;*/

        minimumDistance = 20f;



    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        step = speed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, Playertarget.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Playertarget.position, step);
        }

       /* else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            if (transform.position == targetPos)
            {
                NextPoint();
            }
        }*/



    }

   /* void NextPoint()
    {


        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {

            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
    }*/
}
