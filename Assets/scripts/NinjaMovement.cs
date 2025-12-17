using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class NinjaMovement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float EnemySpeed=4f;
    public Animator anim;
    public float enemyRange=10f;

    public float attackRange=1f;
    public int attackDamage=10;
    public LayerMask playerLayer;
    public float attackCooldown=1f;
    private float attackTimer=0;

    public int MaxHealth = 100;
    private int currentHealth;

    private bool isDead=false;

    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        anim=this.GetComponent<Animator>();
        currentHealth=MaxHealth;
        player=GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            Vector3 direction=player.transform.position-transform.position;

        if (direction.magnitude < enemyRange)
        {
            transform.position=Vector2.MoveTowards(this.transform.position, player.transform.position, EnemySpeed*Time.deltaTime);

            anim.SetBool("IsMoving", true);

            if(direction.magnitude<=attackRange)
            {
                if (attackTimer <= 0)
                {
                  attackTimer=attackCooldown;
                  attack();  
                }
                else attackTimer-=Time.fixedDeltaTime;
            }
            else anim.SetBool("IsAttacking", false);
        }
        else anim.SetBool("IsMoving", false);
        }    
    }

    void attack()
    {
        anim.SetBool("IsAttacking", true);
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if(hitPlayer!=null)
        {
            hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
        
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
        isDead=true;
        anim.SetTrigger("isDead");
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
