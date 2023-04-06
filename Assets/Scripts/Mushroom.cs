using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public AudioClip pickupSound;
    AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(HandleCollision());
        }
    }
    
    IEnumerator HandleCollision() {
        // TODO: Increase player lives

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        audioSource.PlayOneShot(pickupSound);
        yield return new WaitForSeconds(2);
        audioSource.Stop();
        Destroy(gameObject);
    }
}
