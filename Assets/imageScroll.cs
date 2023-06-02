using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageScroll : MonoBehaviour
{
    public float scrollSpeed = 10f;  

    private void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
