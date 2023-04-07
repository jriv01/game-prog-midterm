using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager _gameManager;
    private void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")) {
            _gameManager.TakeDamage(5);
        }

    }
}
