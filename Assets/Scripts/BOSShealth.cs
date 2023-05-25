using UnityEngine;
using System.Collections;

public class BOSShealth : MonoBehaviour
{
    
    [SerializeField] public int maxHealth = 1000;
    public int currentHealth;
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float invincibilityFlashDelay = 0.2f;


    public HealthManager healthBar3;

    [SerializeField] GameObject hitboxDMG;

    // Start is called before the first frame update
    void Start()
    {
        // le joueur commence avec toute sa vie
        currentHealth = maxHealth;
        healthBar3.SetMaxHealth(maxHealth);


    }

    // Update is called once per frame
    void Update()
    {

        

      
    }

    public void TakeDamagee(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;  // si on prends des degats ont retire de la vie a la vie actuelle
            healthBar3.SetHealth(currentHealth); // pour mettre a jour le visuel de la barre de vie
            isInvincible = true;
            StartCoroutine(InvincibilityFlash2());
            StartCoroutine(HandleInvincibilityDelay2());
        }

        /*currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100;*/

    }

    
    public IEnumerator InvincibilityFlash2()
    {
        while (isInvincible)
        {
            hitboxDMG.SetActive(false);
            /*graphics.color = new Color(1f, 1f, 1f, 0f);*/
            yield return new WaitForSeconds(invincibilityFlashDelay);
            /*graphics.color = new Color(1f, 1f, 1f, 1f);*/
            yield return new WaitForSeconds(invincibilityFlashDelay);

            hitboxDMG.SetActive(true);
        }
        Debug.Log("Coroutine3");
    }

    public IEnumerator HandleInvincibilityDelay2()
    {
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
        Debug.Log("Coroutine4");
    }

   

}