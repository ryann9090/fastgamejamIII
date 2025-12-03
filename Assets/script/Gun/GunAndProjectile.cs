using UnityEngine;

public class GunAndProjectile : MonoBehaviour
{
    public GameObject gun;
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 6f;
    public bool autoFire = true;

    public int maxAmmo = 100;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    private float nextFireTime;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        gun.transform.position = player.transform.position;
        gun.transform.rotation = player.transform.rotation;

        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.F))
        {
            autoFire = !autoFire; 
            Debug.Log("AutoFire agora está: " + autoFire);
        }

        
        if (autoFire) TryFire();

        
        if (!autoFire && Input.GetButton("Fire1")) TryFire();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void TryFire()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    public bool VampirismoAtivo = false;
    void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        var projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        currentAmmo--;
        if (VampirismoAtivo)
        {
            Vampirismo vamp = projectile.GetComponent<Vampirismo>();
            vamp.Ativar();        
        }


    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}