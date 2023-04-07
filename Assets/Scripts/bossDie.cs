using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossDie : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidbody2D;
    GameObject[] breakableWalls;
    private void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        breakableWalls = GameObject.FindGameObjectsWithTag("Destructible");
    }

    void FixedUpdate(){
        if(transform.position.y < -3.5f){
            _animator.enabled = false;
        }
        if(transform.position.x > 11.01f){
            foreach (GameObject wall in breakableWalls){
                wall.GetComponent<Destructible>().DestroySelf();
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Destructible")) {
            other.GetComponent<Destructible>().DestroySelf();
            print("Hit");
        }
    }
}
