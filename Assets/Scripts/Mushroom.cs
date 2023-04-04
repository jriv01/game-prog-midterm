using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    void OnTrigger2DEnter(Collider2D other) {
        if(other.CompareTag("Player")) {
            // TODO: Increase player lives
            Destroy(gameObject);
        }
    }
}
