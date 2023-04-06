using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPool : MonoBehaviour
{
    public int fireballChance = 20;
    public GameObject fireballPrefab;
    public FireballSpawnPoint[] spawnPoints;
    public AudioClip fireballSound;
    AudioSource audioSource_;
    GameObject player;

    void Start() {
        audioSource_ = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        audioSource_.enabled = player.transform.position.y >= transform.position.y;
        int chance = Random.Range(0, fireballChance);
        if(chance == 0) {
            audioSource_.PlayOneShot(fireballSound);
            
            FireballSpawnPoint point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Transform pos = point.spawnPoint;
            GameObject fireball = Instantiate(fireballPrefab, pos.position, Quaternion.identity);

            float xOffset = Random.Range(-point.xForceRange, point.xForceRange);

            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xOffset, point.yForce));
        }
    }
}

[System.Serializable]
public class FireballSpawnPoint {
    public Transform spawnPoint;
    public int yForce;
    public int xForceRange;
}
