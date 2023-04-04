using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // public LayerMask whatIsGround;

    // public Transform feet;

    // bool grounded = false;

    Rigidbody2D _rigidbody2D;
    public GameObject  xLeft, xRight;
    public float x_speed;
    float x_pos, y_pos;

    public int hp=3;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        x_pos = gameObject.transform.position.x;
        y_pos = gameObject.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if(hp<=0 || hp==0){
            print("dead");
            Destroy(gameObject);
        }
        _rigidbody2D.velocity = new Vector2(x_speed*x_pos, y_pos);
    }

    private void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("bounds")){
            x_speed = -1 * x_speed;
        }
        if(other.CompareTag("bullet")){
            hp-=3;
        }
        if(other.CompareTag("rock")){
            hp-=1;
        }
        if(other.CompareTag("dynamiteZone")){
            hp-=5;
        }

    }


}
