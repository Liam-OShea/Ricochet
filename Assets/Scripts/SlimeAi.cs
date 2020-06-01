using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAi : MonoBehaviour
{
    public Transform rayStart, player;
    private bool playerSpotted = false;
    private bool obstruction = false;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyHealthManager enemyHealthManager;
    private DoDamage doDamage;
    public float speed = 1;
    public float range = 10;
    private float leftX, rightX, patrolTime = 10;
    private bool left = true;
    private bool flipped = false;
    private int currentHealth;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        doDamage = GetComponent<DoDamage>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        currentHealth = enemyHealthManager.currentHP;
        leftX = transform.position.x - (range / 2);
        rightX = transform.position.x + (range / 2);
    }

    void Update()
    {
        //check if the player is visable
        Raycasting();
      
        if (playerSpotted && !obstruction)
        {
            //move toward the player
            float x = player.position.x - transform.position.x;
            rb.velocity = new Vector2(speed * x / Mathf.Abs(x), rb.velocity.y);

            leftX = transform.position.x - (range / 2);
            rightX = transform.position.x + (range / 2);
            patrolTime = 10;
        }
        else
        {
            //patrol around the area
            if (left)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            if(left && transform.position.x < leftX)
            {
                left = false;
                patrolTime = 10;
            }
            else if(!left && transform.position.x > rightX)
            {
                left = true;
                patrolTime = 10;
            }

            patrolTime -= Time.deltaTime;
            if(patrolTime <= 0)
            {
                left = !left;
                patrolTime = 10;
            }
        }

        //if the slime is close to the player, play the slimes attack animation
        if (doDamage.doingDamage == true)
        {
            doDamage.doingDamage = false;
            animator.Play("SlimeAttack");
        }
    
        //set the slimes speed variable to be equal to its velocity
        //speed > 0 means the slimes walk animation will play
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        //rotate the slime so it faces in the direction that its moving
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            flipped = false;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            flipped = true;
        }

        //play the slimes hurt animation when its health drops
        if(enemyHealthManager.currentHP < currentHealth)
        {
            currentHealth = enemyHealthManager.currentHP;
            animator.Play("SlimeHurt");
        }

        //play the slimes death animation when its health is below 0
        if (currentHealth <= 0)
        {
            animator.Play("SlimeDie");
        }
    }

    void Raycasting()
    {
        //Debug.DrawLine(rayStart.position, player.position, Color.green);
        playerSpotted = Physics2D.Linecast(rayStart.position, player.position, 1 << LayerMask.NameToLayer("Player"));
        // Can create layer with all possible obstructions
        obstruction = Physics2D.Linecast(rayStart.position, player.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
