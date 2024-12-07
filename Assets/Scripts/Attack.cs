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

    private int maxAmmo = 25;
    private int currentAmmo;
    private int currentPoints;

    private float bulletSpeed = 20.0f;
    private float fireCooldown = 0.5f;
    private float lastFireTime = 0f;
    private float gameTime = 0f;

    private bool isGameOver = false;
    private float bulletLifetime = 5.0f;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        UpdatePointsUI();
        UpdateTimeUI();

        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (isGameOver) return;

        gameTime += Time.deltaTime;
        UpdateTimeUI();

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= lastFireTime + fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            Debug.Log("GAME OVER");
        }

        CheckGameOverConditions();
    }

    private void Fire()
    {
        if (currentAmmo <= 0) return;

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

        Destroy(bullet, bulletLifetime);

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
            int seconds = Mathf.FloorToInt(gameTime % 60);
            timeText.text = $"{seconds:00}";
        }
        else
        {
            Debug.LogWarning("Time Text UI element not assigned.");
        }
    }

    private void CheckGameOverConditions()
    {
        if (currentAmmo == 0)
        {
            EndGame("GAME OVER");
        }
    }

    private void EndGame(string message)
    {
        isGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.text = message;
            gameOverText.gameObject.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }

        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;

        currentAmmo = maxAmmo;
        currentPoints = 0;
        gameTime = 0f;

        UpdateAmmoUI();
        UpdatePointsUI();
        UpdateTimeUI();

        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
