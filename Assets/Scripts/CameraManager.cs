using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Slider slider;
    [SerializeField] GameObject playerRef;
    Vector3 refVelocity = Vector3.zero;
    float smoothTime = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y, -10);
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref refVelocity, smoothTime);
    }
    public void SetMaxHealth(int health)
    {
        // met la vie du joueur a 100 pourcent, quand le jeu demarre le joueur a 100 pourcent de ses points de vie
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        // indique le nombre de points de vie a afficher
        slider.value = health;
    }
}
