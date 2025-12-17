using System;
using Unity.VisualScripting;
using UnityEngine;

public class zombieMovement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float EnemySpeed=3f;
    public Animator anim;
    public float enemyRange=7f;

    public float attackRange=0.6f;
    public int attackDamage=5;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public float attackCooldown=1f;
    private float attackTimer=0;

    public int MaxHealth = 100;
    private int currentHealth;

    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        anim=this.GetComponent<Animator>();
        currentHealth=MaxHealth;
        player=GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 direction=player.transform.position-transform.position;

        if (direction.magnitude < enemyRange)
        {
            transform.position=Vector2.MoveTowards(this.transform.position, player.transform.position, EnemySpeed*Time.deltaTime);

            anim.SetFloat("X", direction.normalized.x);
            anim.SetFloat("Y", direction.normalized.y);

            if(direction.magnitude<attackRange)
            {

                if (attackTimer <= 0)
                {
                  attackTimer=attackCooldown;
                  attack();  
                }
                else attackTimer-=Time.fixedDeltaTime;
            }
            else anim.SetBool("isAttacking", false);
        }
        
        
    }

    void attack()
    {
        anim.SetBool("isAttacking", true);
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;


        if(currentHealth<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }


}
