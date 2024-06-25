using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController KomponenPlayer;
    
    void Start()
    {
        KomponenPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player")
        {
            KomponenPlayer.heart--;
            KomponenPlayer.play_again=true;
        }
    }
}
