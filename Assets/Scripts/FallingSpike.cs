using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            print("Collide with player");
            // TODO: Add player damage code
        }
    }
}
