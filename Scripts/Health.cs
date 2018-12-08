using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Player player;

    // Use this for initialization
    void Start ()
    {
        player = FindObjectOfType<Player>(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player)
        {
            float currentHealth = player.GetCurrentHealth();
            float maxHealth = player.GetMaxHealth();

            SetSize(currentHealth / maxHealth);
        }
    }

    public void SetSize(float sizeNormalized)
    {
        Transform bar = transform.Find("Bar");

        if (sizeNormalized < 0.25f)
        {
            bar.Find("Health").GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (sizeNormalized < 0.5f)
        {
            bar.Find("Health").GetComponent<SpriteRenderer>().color = new Color(255, 140, 0); 
        }
        else 
        {
            bar.Find("Health").GetComponent<SpriteRenderer>().color = Color.green;
        }
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
