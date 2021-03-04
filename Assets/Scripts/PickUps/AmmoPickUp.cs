using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private int ammoIncrease = 10;
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, ammoIncrease);
            Destroy(gameObject, 0.5f);
        }
    }
}
