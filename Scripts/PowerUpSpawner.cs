using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    [Header("Health")]
    [SerializeField] GameObject healthPowerUp;
    [SerializeField] float healthPowerUpSpeed;
    [SerializeField] float healthPowerUpCoolDown = 20f;   

    [Header("Shooting")]
    [SerializeField] GameObject shootingPowerUp;
    [SerializeField] float shootingPowerUpSpeed;
    [SerializeField] float shootingPowerUpCoolDown = 50f; 

    [Header("Invinsible")]
    [SerializeField] GameObject invinsiblePowerUp;
    [SerializeField] float invinsiblePowerUpSpeed;
    [SerializeField] float invinsiblePowerUpCoolDown = 60f;

    float healthRemainingCooldownTime;
    float shootRemainingCooldownTime;
    float invisRemainingCooldownTime;

    float padding = 3f;
    float halfScreenSizeX;
    float TopScreenSizeY;

    // Use this for initialization
    void Start()
    {
        Vector2 SpawnerArea = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize * 2);

        halfScreenSizeX = SpawnerArea.x;
        TopScreenSizeY = SpawnerArea.y;

        healthRemainingCooldownTime = healthPowerUpCoolDown;
        shootRemainingCooldownTime = shootingPowerUpCoolDown;
        invisRemainingCooldownTime = invinsiblePowerUpCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRemainingCooldownTime <= 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(padding - halfScreenSizeX, halfScreenSizeX - padding), TopScreenSizeY);

            GameObject powerUp = Instantiate(healthPowerUp, spawnPosition, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -healthPowerUpSpeed);
            healthRemainingCooldownTime = healthPowerUpCoolDown;
        }
        else
        {
            healthRemainingCooldownTime -= Time.deltaTime;
        }

        if(shootRemainingCooldownTime <= 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(padding - halfScreenSizeX, halfScreenSizeX - padding), TopScreenSizeY);

            GameObject powerUp = Instantiate(shootingPowerUp, spawnPosition, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootingPowerUpSpeed);
            shootRemainingCooldownTime = shootingPowerUpCoolDown;
        }
        else
        {
            shootRemainingCooldownTime -= Time.deltaTime;
        }

        if (invisRemainingCooldownTime <= 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(padding - halfScreenSizeX, halfScreenSizeX - padding), TopScreenSizeY);

            GameObject powerUp = Instantiate(invinsiblePowerUp, spawnPosition, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootingPowerUpSpeed);
            invisRemainingCooldownTime = invinsiblePowerUpCoolDown;
        }
        else
        {
            invisRemainingCooldownTime -= Time.deltaTime;
        }
    }

    private void SpawnPowerUp(GameObject prefab, float speed, float cooldown, float remainingCooldown)
    {
        if (remainingCooldown <= 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(padding - halfScreenSizeX, halfScreenSizeX - padding), TopScreenSizeY);

            GameObject powerUp = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            remainingCooldown = cooldown;
            cooldown = remainingCooldown;
        }
        else
        {
            remainingCooldown -= Time.deltaTime;
            Debug.Log(healthRemainingCooldownTime);
            Debug.Log(shootRemainingCooldownTime);
            Debug.Log(invisRemainingCooldownTime);
        }
    }
}
