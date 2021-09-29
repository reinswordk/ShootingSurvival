using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    private PlayerHealth player;

    private void Awake()
    {
        player = PlayerHealth.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.AddHealth();
            Destroy(gameObject);
        }
    }
}
