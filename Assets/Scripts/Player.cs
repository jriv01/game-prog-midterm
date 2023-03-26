using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    public LayerMask whatIsGround;

    public Transform feet;

    bool grounded = false;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position,.3f,whatIsGround);

        if(Input.GetKeyDown("space") && grounded){
            _rigidBody.AddForce(new Vector2(0,jumpForce));
        }       
    }

    void FixedUpdate(){
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidBody.velocity = new Vector2(xSpeed, _rigidBody.velocity.y);
    }

}
