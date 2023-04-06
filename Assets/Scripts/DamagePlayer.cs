using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool destroyOnTrigger = false;
    public int damage;
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _gameManager.TakeDamage(damage);
            if(destroyOnTrigger) {
                Destroy(gameObject);
            }
        }
    }
}
