using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] float healthToRestore;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = FindObjectOfType<Player>();
        player.SetHealthPowerUp(healthToRestore);
        Destroy(gameObject);
    }
}