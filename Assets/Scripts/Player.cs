using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public float jetpackForce = 30.0f;
    bool jetpackActive;
    bool firstinput = true;
    public float hovertime = 3;
    bool canjetpack;

    /*bool fall = false;
bool jumpd = false;*/
    bool grounded = false;
    int CountJump = 1;

    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    /*private bool m_grounded = false;
*/
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animController;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;

    [SerializeField] float jumpForce = 10f;
    [SerializeField] public Transform teleport;
    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;

    public static Vector2 lastCheckPointpos = new Vector2(-185, -150);
    public static bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
        //Debug.Log(Mathf.Lerp(current, target, 0));
        /*grounded = true;*/    
    }

    private void Awake()
    {
        isGameOver = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointpos;
    }

    // Update is called once per frame
    void Update()
    {

        horizontal_value = Input.GetAxis("Horizontal");

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        /*animController.SetFloat("Speed", Mathf.Abs(horizontal_value));*/

        if (Input.GetButtonDown("Fire2"))
        {
            
        }
        
        //jetpeck
        jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            Debug.Log("FireJetPAck");
            hovertime -= Time.deltaTime;
            hovertime = Mathf.Clamp(hovertime, 0, 6);
        }
        else
        {
            hovertime += Time.deltaTime;
            hovertime = Mathf.Clamp(hovertime, 0, 5);
        }

        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
            /*animController.SetBool("Jumping", true);*/
        }

        if (Input.GetKeyDown(KeyCode.Space) && CountJump > 0 && jetpackActive == false)
        {
            Jump();
            
        }

        m_timeSinceAttack += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            // Loop back to one after second attack
            if (m_currentAttack > 2)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of two attack animations "Attack1", "Attack2"
            animController.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        float inputX = Input.GetAxis("Horizontal");

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded && jetpackActive == false) 
        {
            animController.SetTrigger("Jump");
            grounded = false;
            /*animController.SetBool("Grounded", grounded);*/
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            /*m_groundSensor.Disable(0.2f);*/
            Debug.Log("jumpr");
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            animController.SetInteger("AnimState", 1);
            Debug.Log("runr");
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                animController.SetInteger("AnimState", 0);
            Debug.Log("idl");
        }

    }
    void FixedUpdate()
    {

        if (is_jumping && can_jump)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = false;
        }

        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);

        if (jetpackActive && hovertime > 0)
        {
            rb.gravityScale = 0;
            if (firstinput)
            {
                rb.velocity = new Vector2(rb.velocity.x, 1f);
                firstinput = false;
                rb.AddForce(new Vector2(0, jetpackForce));
                /*Debug.Log("it zorks");*/
            }
        }
        else
        {
            firstinput = true;
            rb.gravityScale = 1;
            /*Debug.Log("it zorks2");*/
        }

        /*if (fall == true)
        {
            if (jumpd == true)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
            }
        }*/
    }

    //jp
    void Jump()
    {
        // Garantit que nous ne pouvons pas appeler Jump plusieurs fois à partir d'une seule pression
        CountJump -= 1;

        // On augmente la force appliquée si on tombe
        // Cela signifie que nous aurons toujours l'impression de sauter le même montant
        float force = jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        //for the fast falling
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            /*animController.SetBool("Jumping", false);*/
            grounded = true;
            CountJump = 2; //reset double saut quand on touche le sol          
        }
       /* else if (collision.tag != "Ground" )
        {
            animController.SetBool("Jumping", true);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bossdoor")
        {
          transform.position = teleport.position;
        }
    }

}