using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform enemy;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private EnemyHealth enemyHealth;
    private Transform target;

    [Tooltip("Distance requirement to meet before chasing the Player.")]
    [SerializeField] private float chaseRange = 5f;

    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;

    private void Awake()
    {
        // Initialization
        target = FindObjectOfType<PlayerHealth>().transform;
        enemy = GetComponent<Transform>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyHealth.IsDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }

        // How far the Enemy is from it's target.
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        // If the Enemy has been alerted to the Player.//
        if (isProvoked)
        {
            EngageTarget();
        }
        // If the Player is within range of the Enemy's chase range.
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        FaceTarget(target);

        // If the target isn't within attacking range..
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        // If the target isn within attacking range..
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        // Set the destination of the Enemy towards it's target.
        navMeshAgent.SetDestination(target.position);

        anim.SetBool("Attack", false);
        anim.SetTrigger("Move");
    }

    private void AttackTarget()
    {
        Debug.Log(name + " has seeked and is destroying " + target.name);
        anim.SetBool("Attack", true);
    }

    /// <summary>
    /// Face the Enemy towards a target.
    /// </summary>
    /// <param name="target">
    /// Target to look at.
    /// </param>
    private void FaceTarget(Transform target)
    {
        enemy.LookAt(target);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Create a Red Wireframe Sphere to visualize the Enemy's chase range.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}