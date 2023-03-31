using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    private static int hp = 10;

    public float bulletForce = 200f;

    public LayerMask whatIsGround;

    public Transform feet;

    public Transform throwFrom;

    bool grounded = false;

    private SpriteRenderer _spriteRenderer;

    public float speed = 10;

    public int rockCount = 10;

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
            if(rockCount>0){
                StartCoroutine(fire());
                rockCount--;
            }
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
        _spriteRenderer.color = new Color(1.0f,0.0f,0.0f,255.0f);
        yield return new WaitForSeconds(.5f);
        _spriteRenderer.color = new Color(255.0f,255.0f,255.0f,255.0f);
        
    }
    IEnumerator fire(){
        GameObject shot;
        Vector3 mousePos = Input.mousePosition;   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 Worldpos2D=new Vector2(Worldpos.x,Worldpos.y);
        shot = Instantiate(bullet, throwFrom.position, Quaternion.identity);
        Vector2 BulletVector = new Vector2((Worldpos2D.x-throwFrom.position.x),(Worldpos2D.y-throwFrom.position.y));
        shot.GetComponent<Rigidbody2D>().AddForce(BulletVector*bulletForce);
        return;
    }
}

