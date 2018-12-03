using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool damageIsFromProjectile = true;

    public int GetDamaged()
    {
        return damage;
    }

    public bool DamageIsFromProjectile()
    {
        return damageIsFromProjectile;
    }

    public void DestroyDamageDealer()
    {
        Destroy(gameObject);
    }
}
