using UnityEngine;
using System.Collections;


public class EnemyHealth : MonoBehaviour
{

    bool IsGrounded = false;
    /*int CountJump = 1;*/
    [SerializeField] int maxHealth;
    private int currentHealth;
    public HealthManager healthBar;
    private SpriteRenderer graphics;
    [SerializeField] GameObject HEALTHBAR;

    float horizontal_value = 1;
    Vector2 ref_velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        HEALTHBAR.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "Player")
        {
            horizontal_value *= -1;
        }
       /* if (collision.tag == "Player")
        {
            Player scriptref = collision.gameObject.GetComponent<Player>();
            scriptref.TakeDamage(25);
            scriptref.StartCoroutine(ShowBar());

        }*/

        //ground detect
        if (collision.tag == "Ground")
        {
            IsGrounded = true;

        }

        if (collision.tag == "Player")
        {
            TakeDamage(25);
            StartCoroutine(ShowBar());

        }


       /* if (Player.transform.name == "Player")
        {
            TakeDamage(25);
            StartCoroutine(ShowBar());

        }*/
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    /*private void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.transform.name == "Player")
        {
            TakeDamage(25);
            StartCoroutine(ShowBar());

        }
    }*/
    private IEnumerator ShowBar()
    {
        HEALTHBAR.SetActive(true);
        yield return new WaitForSeconds(5f);
        HEALTHBAR.SetActive(false);
    }

}
