using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] float baseScrollSpeed = 0.5f;
    [SerializeField] float minScrollSpeed = 0.3f;
    [SerializeField] float maxScrollSpeed = 0.7f;

    float scrollSpeed;

    Material material;
    Transform player;
    Vector2 offset;
   
    // Use this for initialization
    void Start ()
    {
        scrollSpeed = baseScrollSpeed;
        material = GetComponent<Renderer>().material;
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (player != null)
        {
            scrollSpeed = Mathf.Clamp(player.GetComponent<Rigidbody2D>().velocity.y + baseScrollSpeed, minScrollSpeed, maxScrollSpeed);
        }

        offset = new Vector2(0f, scrollSpeed);
        material.mainTextureOffset +=  offset * Time.deltaTime;
	}
}
