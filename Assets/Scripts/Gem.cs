using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int value = 10;

    void OnTrigger2DEnter(Collider other) {
        if(other.CompareTag("Player")) {
            // TODO: Add to player score
            Destroy(gameObject);
        }
    }
}
