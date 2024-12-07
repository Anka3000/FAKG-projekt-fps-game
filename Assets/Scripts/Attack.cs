using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public TMP_Text ammoText;
    public TMP_Text pointsText;
    public TMP_Text timeText;
    public TMP_Text gameOverText;
    public GameObject restartButton;

    private int maxAmmo = 10;
    private int currentAmmo;
    private int currentPoints;

    private float bulletSpeed = 20.0f;
    private float fireCooldown = 0.5f;
    private float lastFireTime = 0f;
    private float gameTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        UpdatePointsUI();
        Cursor.visible = false;

        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        UpdateTimeUI();

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= lastFireTime + fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
        }

        CheckGameOverConditions();
    }

    public void Fire()
    {
        if (currentAmmo <= 0)
            return;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetVelocity(bulletSpawnPoint.forward * bulletSpeed);
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
    private void UpdateTimeUI()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.FloorToInt(gameTime).ToString();
        }
        else
        {
            Debug.LogWarning("Time Text UI element not assigned.");
        }
    }
    private void CheckGameOverConditions()
    {
        if (currentAmmo == 0 || currentPoints == 150)
        {
            Time.timeScale = 0;
            gameOverText.gameObject.SetActive(true);
            restartButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        // Zresetuj wszystkie statystyki i elementy UI
        currentAmmo = maxAmmo;
        currentPoints = 0;
        gameTime = 0f;
        UpdateAmmoUI();
        UpdatePointsUI();
        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        // Upewnij siê, ¿e kursor jest widoczny i odblokowany
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
