using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public AudioClip pickupSound;
    public int healValue = 10;
    AudioSource audioSource;
    GameManager _gameManager;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(HandleCollision());
        }
    }
    
    IEnumerator HandleCollision() {
        _gameManager.HealPlayer(10);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        audioSource.PlayOneShot(pickupSound);
        yield return new WaitForSeconds(2);
        audioSource.Stop();
        Destroy(gameObject);
    }
}
