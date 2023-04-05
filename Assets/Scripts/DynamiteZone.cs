using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteZone : MonoBehaviour
{
    public string destroyTag = "Destructible";
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(destroyTag)) {
            other.GetComponent<Destructible>().DestroySelf();
        }
    }
}
