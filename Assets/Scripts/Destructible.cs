using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject particlePrefab; 
    public void DestroySelf() {
        foreach(Transform child in transform) {
            Instantiate(particlePrefab, child.position, Quaternion.Euler(-90, 0, 0));
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}
