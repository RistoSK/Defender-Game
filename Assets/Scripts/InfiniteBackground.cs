using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;

    Material material;
    Vector2 offset;
    float playersPreviousPositionY = 0f;

    // Use this for initialization
    void Start ()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update ()
    {
        Transform player = transform.Find("Player");

        if(player != null)
        { 
            adjustSpeed(player);      
            playersPreviousPositionY = player.position.y;
        }

        offset = new Vector2(0f, scrollSpeed);
        material.mainTextureOffset +=  offset * Time.deltaTime;
	}

    public void adjustSpeed(Transform player)
    {
        if (player.position.y > playersPreviousPositionY)
        {
            scrollSpeed = 0.7f;
        }
        else if (player.position.y < playersPreviousPositionY)
        {
            scrollSpeed = 0.3f;
        }
        else
        {
            scrollSpeed = 0.5f;
        }
    }
}
