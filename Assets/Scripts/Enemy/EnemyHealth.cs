using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    private Animator anim;
    private bool isDead = false;
    public bool IsDead { get { return isDead; } }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Reduce the enemy's health.
    /// </summary>
    /// <param name="damage">
    /// Value to reduce enemy health by.
    /// </param>
    public void EnemyTakeDamage(int damage)
    {
        BroadcastMessage("OnDamageTaken");

        health -= damage;
        if (health <= 0)
        {
            Die();
           // Destroy(gameObject);
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("Die");
    }
}