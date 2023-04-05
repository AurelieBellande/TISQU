using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]public Image healthBar;
    [SerializeField] public float healthAmount = 100f;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }*/

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }

    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
     

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
