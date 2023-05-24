using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Transform teleport;
    [SerializeField] public Transform checkpoint;



    //jetpack
    /*float distance = 10f;*/
    public float jetpackForce = 30.0f;
    bool jetpackActive;
    bool firstinput = true;
    public float hovertime = 3;
    bool canjetpack;

    /*bool IsGrounded = false;*/
    int CountJump = 2;
    /*private int LastPressedJumpTime = 0;*/
    /*private int LastOnGroundTime = 0;*/

    /*dubl jump*/

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animController;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;


    [SerializeField] float jumpForce = 10f;

    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;
  /* [Range(0, 1)] [SerializeField] float smooth_time = 0.5f;*/


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
        //Debug.Log(Mathf.Lerp(current, target, 0));
      
    }

    // Update is called once per frame
    void Update()
    {
      

        horizontal_value = Input.GetAxis("Horizontal");

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        animController.SetFloat("Speed", Mathf.Abs(horizontal_value));

       /* if (Input.GetButtonDown("Jump") && can_jump)
        {

           *//* is_jumping = true;*//*
            animController.SetBool("Jumping", true);
        }*/

        //jetpeck

        jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            hovertime -= Time.deltaTime;
            hovertime = Mathf.Clamp(hovertime, 0, 5);
        }
        else
        {
            hovertime += Time.deltaTime;
            hovertime = Mathf.Clamp(hovertime, 0, 5);
        }
        if (Input.GetKeyDown(KeyCode.Space) && CountJump > 0 && jetpackActive == false )
        {
         
            Jump();
           
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
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                firstinput = false;
                rb.AddForce(new Vector2(0, jetpackForce));
            }
        }
        else
        {
            firstinput = true;
            rb.gravityScale = 1;
        }


    }

    //jp
    void Jump()
    {
        // Garantit que nous ne pouvons pas appeler Jump plusieurs fois à partir d'une seule pression
        /*LastPressedJumpTime = 0;*/
        /*LastOnGroundTime = 0;*/
        CountJump -= 1;


        // On augmente la force appliquée si on tombe
        // Cela signifie que nous aurons toujours l'impression de sauter le même montant
        float force = jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;


        
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            animController.SetBool("Jumping", false);
           /* IsGrounded = true;*/
            CountJump = 2; //reset double saut quand on touche le sol
            
        }
       else if (collision.tag != "Ground")
        {
            animController.SetBool("Jumping", true);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.tag == "bossdoor")
        {
          transform.position = teleport.position;
        }

        /*if (collision.tag == "checkpoint" && currentHealth <= 0)
        {
            transform.position = checkpoint.position;
        }*/
    }

   

}