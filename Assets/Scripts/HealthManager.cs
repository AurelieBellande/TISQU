using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public BOSS boss;
    public GameObject animatorObject;
    [SerializeField]public Image healthBar;
    [SerializeField] public float healthAmount = 100f;
    public float damageAmount = 15f;
    public Slider slider;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<BOSS>();
        /*animatorObject = animatorObject.GetComponent<BOSS>().anim;*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (healthAmount <= 0)
        {
            *//*DefeatBoss();*//*
        }*/

        /*if (healthAmount <= 0)
        {
            // Disable the current GameObject (enemy)
            transform.root.gameObject.SetActive(false);
        }*/


        if (anim != null && healthAmount <= 0)
        {
            anim.SetTrigger("Death");
        }
        /*GameObject[] boss = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject Boss in boss)
        {
            Destroy(Boss);

        }*/
        if (anim!= null && healthAmount == 50)
        {
            anim.SetBool("Attack2", true);
        }

    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Test");
        healthAmount -= damageAmount;
        /*healthBar.fillAmount = healthAmount / 100;*/

        healthBar.fillAmount = healthAmount/100.0f;

        /*if (healthAmount == 500)
        {
            healthBar.fillAmount = healthAmount / 500.0f;
            Debug.Log("GYAT");
        }*/
       
    }
   /* public void DefeatBoss()
    {
        transform.root.gameObject.SetActive(false);
    }*/


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
