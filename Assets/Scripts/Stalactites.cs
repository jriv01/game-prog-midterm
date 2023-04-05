using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactites : MonoBehaviour
{
    public GameObject fallingStalactitePrefab;
    public GameObject debrisPrefab;
    public Transform[] spawnPoints;
    public int dropChance = 100;
    public float dropDelay = 3f;
    public AudioClip debrisSound;
    AudioSource audioSource;
    bool droppingStalactite = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int chance = Random.Range(0, dropChance);
        if(!droppingStalactite && chance == 0) {
            droppingStalactite = true;
            StartCoroutine(DropStalactite());
        }
    }

    IEnumerator DropStalactite() {
        GameObject debris = Instantiate(debrisPrefab, spawnPoints[0].position, Quaternion.Euler(90, 0, 0));
        audioSource.PlayOneShot(debrisSound);
        yield return new WaitForSeconds(dropDelay);
        audioSource.Stop();
        Instantiate(fallingStalactitePrefab, spawnPoints[0].position, Quaternion.identity);
        Destroy(debris);
        droppingStalactite = false;
    }
}
