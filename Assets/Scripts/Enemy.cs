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
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] AudioClip projectileAudio;
    [SerializeField] [Range(0, 1)] float projectileVolume = 1f;

    [Header("OnDeath")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] [Range(0,1)] float explosionVolume = 1f;

    void Start()
    {
        shotDelay = Random.Range(minShotDelay, maxShotDelay);
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

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(projectileAudio, Camera.main.transform.position, projectileVolume);

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
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
}
