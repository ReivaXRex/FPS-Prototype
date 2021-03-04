using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private int damage = 4;

    public void AttackHitEvent()
    {
        if (target == null) return;
        PlayerHealth playerhealth = FindObjectOfType<PlayerHealth>();
        playerhealth.TakeDamage(damage);
        Debug.Log("Damage Player");
    }
}