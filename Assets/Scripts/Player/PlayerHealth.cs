using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Canvas hitScreen;
    [SerializeField] private float waitTime = 0.3f;

    private void Start()
    {
        hitScreen.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(EnableHitScreen());

        if (health <= 0)
        {
            GameManager.Instance.HandleDeath();
            Debug.Log("Player has died!");
        }
    }

    private IEnumerator EnableHitScreen()
    {
        hitScreen.enabled = true;
        yield return new WaitForSeconds(waitTime);
        hitScreen.enabled = false;
    }
}