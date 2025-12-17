using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements.Experimental;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public float attackRange=1.2f;
    public LayerMask enemyLayer;
    public int attackDamage = 20;
    public int maxHealth = 100;
    public int currentHealth;
    public int SpecialAttackDamage=60;
    public float attackCooldown=10f;
    private float attackTimer;
    private bool SpecialAttack=false;

    void Start()
    {
        anim=this.GetComponent<Animator>();
        currentHealth=100;
        attackTimer=0f;
    }

    public void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            anim.SetBool("isAttacking", true);
            Attack(attackDamage);
        }
    }

    void Attack(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            var zombie=enemy.GetComponent<zombieMovement>();
            var ninja=enemy.GetComponent<NinjaMovement>();
            if(zombie!=null) zombie.TakeDamage(damage);
            if(ninja!=null) ninja.TakeDamage(damage);
        }
    }

    void FixedUpdate()
    {
        if(attackTimer<=0 && SpecialAttack)
        {
            attackTimer=attackCooldown;
            anim.SetBool("SpecialAttack", true);
            Attack(SpecialAttackDamage);
            SpecialAttack=false;
        }
        else attackTimer-=Time.fixedDeltaTime;
    }
    public void OnSpecialAttack(InputValue value)
    {
        if(value.isPressed && attackTimer<=0)
        {
            SpecialAttack=true;
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
        Destroy(gameObject);
    }
}
