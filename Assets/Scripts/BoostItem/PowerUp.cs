using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = PlayerMovement.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.Boosting();
            Destroy(gameObject);
        }
    }
}
