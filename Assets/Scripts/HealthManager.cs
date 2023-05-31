using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]public Image healthBar;
    [SerializeField] public float healthAmount = 100f;
    public float damageAmount = 15f;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      /* if (collision.tag == "enemy")
        {
            TakeDamage(20);
        }*/

    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Test");
        healthAmount -= damageAmount;
        /*healthBar.fillAmount = healthAmount / 100;*/

        healthBar.fillAmount = healthAmount/100.0f;
        //if (healthAmount <= 0)
        //{
        //    Destroy(gameObject);
        //}
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
