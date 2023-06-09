using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialPlayer : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    //private static int hp = 10;

    public LayerMask whatIsGround;

    public Transform feet;

    bool grounded = false;

    private SpriteRenderer _spriteRenderer;

    public float speed = 10;

    public GameObject bullet;

    public TextMeshProUGUI spikeText;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        //spikeText.enabled = false;
        //spikeText.Text = "hp - 1";
    }

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
            //hp-=1;
            StartCoroutine(DamageTaken());
            //SceneManager.LoadScene("Fail");
            //spikeText.enabled = true;
            spikeText.text = "Spike: \n hp - 1";
        }
        if(other.CompareTag("Fireball")){
            //hp-=2;
            StartCoroutine(DamageTaken());
            //SceneManager.LoadScene("Fail");
        }
        if(other.CompareTag("LavaPool")){
            //hp-=10;
            StartCoroutine(DamageTaken());
            //SceneManager.LoadScene("Fail");
        }
    }

    IEnumerator DamageTaken(){
        _spriteRenderer.color = new Color(1.0f,0.0f,0.0f,255.0f);
        yield return new WaitForSeconds(.5f);
        _spriteRenderer.color = new Color(255.0f,255.0f,255.0f,255.0f);
        
    }
}
