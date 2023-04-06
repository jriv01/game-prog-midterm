using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject particlePrefab; 
    public AudioClip destroySound;
    public float soundDelay;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void DestroySelf() {
        GetComponent<Collider2D>().enabled = false;
        foreach(Transform child in transform) {
            Instantiate(particlePrefab, child.position, Quaternion.Euler(-90, 0, 0));
            Destroy(child.gameObject);
        }
        audioSource.PlayOneShot(destroySound);
    }
}
