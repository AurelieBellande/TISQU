using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    /*float V = 0,5;*/

    /*[SerializeField] public float healthAmount = 100f;*/
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
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
        healthBar.fillAmount = currentHealth / 100;*/

    }

    /*public void Heal(int healingAmount)
    {
        currentHealth  += healingAmount;
        currentHealth  = Mathf.Clamp(currentHealth , 0, 100);

        healthBar.fillAmount = currentHealth  / 100f;


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
        Debug.Log("Coroutine1");
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
        Debug.Log("Coroutine2");
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
            StartCoroutine(WaterDamage());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "water")
        {
            onWater = false;

        }

        if (collision.tag == "enemy")
        {
            onWater = false;
        }
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
            onspider = true;
        }
    }*/

   /* private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
            onWater = false;
        }
    }*/

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
    }

    /*    void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == "enemy")
            {
                this.transform.position = SpawnPoint.transform.position;
            }

            if (col.transform.tag == "HP")
            {
                currentHealth += 20;
            }

        }*/


}
