using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyZone : MonoBehaviour
{
    public string[] tagsToDestroy;

    void OnTriggerEnter2D(Collider2D other) {
        if(tagsToDestroy.Contains(other.tag)) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
