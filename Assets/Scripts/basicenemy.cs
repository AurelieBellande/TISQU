using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemy : MonoBehaviour
{


    [SerializeField] int maxHealth;
    private int currentHealth;
    public HealthManager healthBar;
    private SpriteRenderer graphics;
    [SerializeField] GameObject HEALTHBAR;
    [SerializeField] GameObject Player;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animController;
    float horizontal_value = 1;
    Vector2 ref_velocity = Vector2.zero;

    float jumpForce = 12f;

    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [Range(0, 2)] [SerializeField] float smooth_time = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        HEALTHBAR.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;
    }
    void FixedUpdate()
    {
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

  
    private IEnumerator ShowBar()
    {
        HEALTHBAR.SetActive(true);
        yield return new WaitForSeconds(5f);
        HEALTHBAR.SetActive(false);
    }
}
