using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    /*float V = 0,5;*/

    [SerializeField] public Transform checkpoint1;
    [SerializeField] public Transform checkpoint2;

    [SerializeField] public int maxHealth = 100;
    public int currentHealth;
    public bool isInvincible = false;
    /*public SpriteRenderer graphics;*/
    public float invincibilityFlashDelay = 0.2f;

    public bool onspider = false;
    public bool onWater = false;
    public HealthManager healthBar;

    public GameObject SpawnPoint;
    [SerializeField] GameObject hitboxDMG;

    // Start is called before the first frame update
    void Start()
    {
        // le joueur commence avec toute sa vie
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        // test pour voir si ca fonctionne
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(20);
        }*/

        if (currentHealth <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
           
        }
        /* if (currentHealth <= 0)
         {
             transform.position = SpawnPoint.position;

         }*/


    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;  // si on prends des degats ont retire de la vie a la vie actuelle
            healthBar.SetHealth(currentHealth); // pour mettre a jour le visuel de la barre de vie
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }

        /*currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100.0f;*/

           /* healthBar.fillAmount = currentHealth / 100.0f;*/
    }

   /* public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        *//*healthBar.fillAmount = currentHealth / 100f;*//*
        healthBar.SetHealth(currentHealth);

    }*/

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            hitboxDMG.SetActive(false);
           /* graphics.color = new Color(1f, 1f, 1f, 0f);*/
            yield return new WaitForSeconds(invincibilityFlashDelay);
            /*graphics.color = new Color(1f, 1f, 1f, 1f);*/
            yield return new WaitForSeconds(invincibilityFlashDelay);

            hitboxDMG.SetActive(true);
        }
        /*Debug.Log("Coroutine1");*/
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
        /*Debug.Log("Coroutine2");*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "water")
        {
            onWater = true;
            StartCoroutine(WaterDamage());
        }

        if (collision.tag == "enemy")
        {
            onspider = true;
            StartCoroutine(Spooderdmg());
        }

       /* if (collision.tag == "checkpoint2")
        {
            if (currentHealth <= 0)
            {
                transform.position = checkpoint2.position;
            }
            
        }
        else if (collision.tag == "checkpoint1" )
        {
            if (currentHealth <= 0)
            {
                transform.position = checkpoint1.position;
            }
                
        }*/

        if (collision.tag == "HP")
        {
            currentHealth += 20;
        }

        if (collision.tag == "HP2")
        {
            currentHealth += 100;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag != "water")
        {
            onWater = false;

        }

        if (collision.tag != "enemy")
        {
            onspider = false;
        }


    }


    public IEnumerator WaterDamage()
    {
        TakeDamage(10);
        /*currentHealth -= 10;*/
        yield return new WaitForSeconds(1.5f);
        if (onWater)
        {
            StartCoroutine(WaterDamage());
        }
    }

    public IEnumerator Spooderdmg()
    {
        TakeDamage(20);
        /*currentHealth -= 15;*/
        yield return new WaitForSeconds(2f);
        if (onspider)
        {
            StartCoroutine(Spooderdmg());
        }
        else if (!onspider)
        {
            StopCoroutine(Spooderdmg());
        }
    }

   
}
