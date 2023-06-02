using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOSS : MonoBehaviour
{
    public Transform player;
    /*    public bool isFlipped = false;*/
    public string creditsSceneName = "credits";
    float speed = 8f;
    Vector3 targetPos;
   /* int pointIndex;
    int pointCount;
    int direction = 1;*/
    float step;
    [SerializeField] Transform Playertarget;
    float minimumDistance;
    public HealthManager healthBar3;
    float healthAmount = 100;
    Animator anim;
    private bool isFlipped = true;
    private bool isBossDefeated = false;

    // Start is called before the first frame update
    void Start()
    {

        minimumDistance = 1f;
        healthBar3 = GetComponent<HealthManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
            anim.SetTrigger("Death");
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > Playertarget.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < Playertarget.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }
    private void FixedUpdate()
    {
        /*if (isBossDefeated)
            return;*/

        step = speed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, Playertarget.position) < minimumDistance)
        {
           
            transform.position = Vector2.MoveTowards(transform.position, Playertarget.position, step);
            LookAtPlayer();
        }
       
    }

    /*public void BossDefeated()
    {
        if (!isBossDefeated)
        {
            isBossDefeated = true;
            LoadCreditsScene();
        }
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }*/
}
