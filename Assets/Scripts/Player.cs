using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position,.3f,whatIsGround);

        if(Input.GetKeyDown("space") && grounded){
            _rigidBody.AddForce(new Vector2(0,jumpForce));
        }       

        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Input.mousePosition;   
            mousePos.z=UnityEngine.Camera.main.nearClipPlane;
            Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);  
            Vector2 Worldpos2D=new Vector2(Worldpos.x,Worldpos.y);
            print(Worldpos2D);
            print(transform.position);
            Instantiate(bullet, Worldpos2D, Quaternion.identity);
            
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

        }
        if(other.CompareTag("Fireball")){
            hp-=2;
            StartCoroutine(DamageTaken());
        }
        if(other.CompareTag("LavaPool")){
            hp-=10;
            StartCoroutine(DamageTaken());
        }
    }

    IEnumerator DamageTaken(){
        _spriteRenderer.color = new Color(1.0f,0.0f,0.0f,255.0f);
        yield return new WaitForSeconds(.5f);
        _spriteRenderer.color = new Color(255.0f,255.0f,255.0f,255.0f);
        
    }
}

