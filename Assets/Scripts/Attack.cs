using System.Collections;
using UnityEngine;
using TMPro;

public class Attack : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public TMP_Text ammoText;
    public TMP_Text pointsText;

    private int maxAmmo = 10;
    private int currentAmmo;
    private int currentPoints;

    private float bulletSpeed = 20.0f;
    private float fireCooldown = 0.5f;
    private float lastFireTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        UpdatePointsUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= lastFireTime + fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
        }
    }

    public void Fire()
    {
        if (currentAmmo <= 0)
            return;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on bullet prefab.");
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.ChildMethod();
        }
        else
        {
            Debug.LogWarning("Bullet component not found on the bullet prefab!");
        }

        currentAmmo--;
        UpdateAmmoUI();
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        UpdatePointsUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString("00");
        }
        else
        {
            Debug.LogWarning("Ammo Text UI element not assigned.");
        }
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = currentPoints.ToString("000");
        }
        else
        {
            Debug.LogWarning("Points Text UI element not assigned.");
        }
    }
}
