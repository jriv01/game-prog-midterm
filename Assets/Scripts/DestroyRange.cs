using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        print("'Triggered'");
        if(other.CompareTag("Destructible")) {
            Destructible obj = other.GetComponent<Destructible>();
            obj.DestroySelf();
        }
    }
}
