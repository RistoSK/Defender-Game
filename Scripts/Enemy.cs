using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 100;
    [SerializeField] int points = 100;
    [SerializeField] GameObject pointsPrefab;
    [SerializeField] AudioClip pointsAudio;
    [SerializeField] [Range(0, 1)] float pointsVolume = 1f;

    [Header("Shoot")]
    [SerializeField] float shotDelay = 0;
    [SerializeField] float minShotDelay = 0.2f;
    [SerializeField] float maxShotDelay = 3f;

    [Header("Projectile")]
    [SerializeField] Transform projectileSpawnPointLeft;
    [SerializeField] Transform projectileSpawnPointCenter;
    [SerializeField] Transform projectileSpawnPointRight;
    [SerializeField] Transform projectileSpawnPointLeftWing;
    [SerializeField] Transform projectileSpawnPointRightWing;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] AudioClip projectileAudio;
    [SerializeField] [Range(0, 1)] float projectileVolume = 1f;
    Quaternion projectileRightRotation;
    Quaternion projectileLeftRotation;

    [Header("OnDeath")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] [Range(0,1)] float explosionVolume = 1f;

    void Start()
    {
        shotDelay = Random.Range(minShotDelay, maxShotDelay);
        
        projectileRightRotation = Quaternion.Euler(new Vector3(0, 0, 20));
        projectileLeftRotation = Quaternion.Euler(new Vector3(0, 0, -20));
    }

    void Update()
    {
        ReadyToShoot();
    }

    private void ReadyToShoot()
    {
        shotDelay -= Time.deltaTime;
        if(shotDelay <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        shotDelay = Random.Range(minShotDelay, maxShotDelay);

        if (projectileSpawnPointLeft != null)
        {
            FireProjectile(projectilePrefab, projectileAudio, projectileVolume, projectileSpawnPointLeft.position, projectileLeftRotation, projectileSpeed);
        }
        if (projectileSpawnPointCenter != null)
        {
            FireProjectile(projectilePrefab, projectileAudio, projectileVolume, projectileSpawnPointCenter.position, Quaternion.identity, projectileSpeed);
        }
        if (projectileSpawnPointRight != null)
        {
            FireProjectile(projectilePrefab, projectileAudio, projectileVolume, projectileSpawnPointRight.position, projectileRightRotation, projectileSpeed);
        }
        if(projectileSpawnPointLeftWing != null && projectileSpawnPointRightWing != null)
        {
            StartCoroutine(FireSecondary());
        }
    }

    IEnumerator FireSecondary()
    {
        yield return new WaitForSeconds(1f);
        FireProjectile(projectilePrefab, projectileAudio, projectileVolume, projectileSpawnPointLeftWing.position, Quaternion.identity, projectileSpeed);
        FireProjectile(projectilePrefab, projectileAudio, projectileVolume, projectileSpawnPointRightWing.position, Quaternion.identity, projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Damage damage = collider.gameObject.GetComponent<Damage>();

        if (damage)
        {
            health -= damage.GetDamaged();
            damage.DestroyDamageDealer();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayAnimation(pointsPrefab, pointsAudio, pointsVolume, 0.7f);
        FindObjectOfType<GameSession>().AddScore(points);
        Destroy(gameObject);
        PlayAnimation(explosionPrefab, explosionAudio, explosionVolume, 0.5f);
    }

    private void PlayAnimation(GameObject prefab, AudioClip audio, float Volume, float DestroyTime)
    {
        GameObject clip = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, Volume);
        Destroy(clip, DestroyTime);
    }

    private void FireProjectile(GameObject prefab, AudioClip audio, float Volume, Vector3 spawnPoint, Quaternion projectileRotation, float speed)
    {
        GameObject projectile = Instantiate(prefab, spawnPoint, projectileRotation) as GameObject;
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, Volume);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileRotation.z * 5, -speed);
    }
}
