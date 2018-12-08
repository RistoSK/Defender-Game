using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = FindObjectOfType<Player>();
        player.SetShootingPowerUp();  
        Destroy(gameObject);
    }
}
