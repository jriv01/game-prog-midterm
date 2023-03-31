using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    private static int hp = 10;

    public LayerMask whatIsGround;

    public Transform feet;

    bool grounded = false;

    private SpriteRenderer _spriteRenderer;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(hp);
        grounded = Physics2D.OverlapCircle(feet.position,.3f,whatIsGround);

        if(Input.GetKeyDown("space") && grounded){
            _rigidBody.AddForce(new Vector2(0,jumpForce));
        }       
    }

    void FixedUpdate(){
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidBody.velocity = new Vector2(xSpeed, _rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Spike")){
            hp-=1;
            StartCoroutine(DamageTaken());
            SceneManager.LoadScene("Fail");
        }
        if(other.CompareTag("Fireball")){
            hp-=2;
            StartCoroutine(DamageTaken());
            SceneManager.LoadScene("Fail");
        }
        if(other.CompareTag("LavaPool")){
            hp-=10;
            StartCoroutine(DamageTaken());
            SceneManager.LoadScene("Fail");
        }
    }

    IEnumerator DamageTaken(){
        _spriteRenderer.color = new Color(1.0f,0.0f,0.0f,0.0f);
        yield return new WaitForSeconds(.5f);
    }
}

