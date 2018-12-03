using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 7f;
    [SerializeField] float padding = 0.7f;
    [SerializeField] float maxHealth = 200;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    [SerializeField] AudioClip projectileAudio;
    [SerializeField] [Range(0, 1)] float projectileVolume = 0.1f;

    [Header("Damaged")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] [Range(0, 1)] float explosionVolume = 1f;
    [SerializeField] GameObject gotHitPrefab;
    [SerializeField] AudioClip gotHitAudio;
    [SerializeField] [Range(0, 1)] float gotHitVolume = 1f;


    Coroutine firingCoroutine;

    float currentHealth;
        
    float minX;
    float maxX;
    float minY;
    float maxY;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        CreateBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void CreateBoundaries()
    {
        Camera camera = Camera.main;

        minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float newPositionX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float newPositionY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY); 

        transform.position = new Vector2(newPositionX, newPositionY);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(ContinueFiring());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator ContinueFiring()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(projectileAudio, Camera.main.transform.position, projectileVolume);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Damage damage = collider.gameObject.GetComponent<Damage>();

        if(damage)
        {
            currentHealth -= damage.GetDamaged();

            PlayAnimation(gotHitPrefab, gotHitAudio, gotHitVolume, 0.2f);

            if (!damage.DamageIsFromProjectile())
            {
                PlayAnimation(explosionPrefab, explosionAudio, explosionVolume, 0.5f);
            }
            damage.DestroyDamageDealer();
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Health>().SetSize(0);
        Destroy(gameObject);
        PlayAnimation(explosionPrefab, explosionAudio, explosionVolume, 0.5f);
        FindObjectOfType<EnemySpawner>().ResetAllWaveSettings();
        FindObjectOfType<Level>().GameOver();
    }

    private void PlayAnimation(GameObject prefab, AudioClip audio, float Volume, float DestroyTime)
    {
        GameObject clip = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, Volume);
        Destroy(clip, DestroyTime);
    }
}