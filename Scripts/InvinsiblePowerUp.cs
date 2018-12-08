using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinsiblePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = FindObjectOfType<Player>();
        player.SetInvinsiblePowerUp();     
        Destroy(gameObject);
    }
}
