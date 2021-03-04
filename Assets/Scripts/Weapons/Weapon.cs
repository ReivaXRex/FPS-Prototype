using UnityEngine;
using TMPro;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Camera cam;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private TextMeshProUGUI ammoCountText;

    [SerializeField] AmmoType ammoType;

    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;

    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float fireRate = 15f;

    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private void OnEnable()
    {
        isReloading = false;
    }

    private void Update()
    {
        DisplayAmmo();

        if (isReloading)
        {
            return;
        }

        if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {      
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
      
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoSlot.ReloadAmmo(ammoType, maxAmmo);
        isReloading = false;
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        // Shoot out a Ray forwards from the Camera's position with a max range and store data of what was hit within the hit variable.
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;

            target.EnemyTakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    /// <summary>
    /// Instantiate the HitVFX where the raycast collidies.
    /// </summary>
    /// <param name="hit">
    /// </param>
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoCountText.text = "Ammo Count " + currentAmmo.ToString();
    }
}