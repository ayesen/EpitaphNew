using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMats : MonoBehaviour
{
    public GameObject player;
    public float speed;
    Vector3 playerDirection;
    float startTime;
    bool flyToPlayer;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(player != null && player.GetComponent<PlayerScript>().tempInventory[3].Mats != null)
        {
            flyToPlayer = false;
        }
        if (flyToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector3(playerDirection.x, playerDirection.y, playerDirection.z) * speed * (Time.time / startTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = GameObject.Find("Player");
            if(player.GetComponent<PlayerScript>().tempInventory[3].Mats == null)
            {
                startTime = Time.time;
                flyToPlayer = true;
            }
        }
    }
}
