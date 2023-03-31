using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    private static int hp = 10;

    public float bulletForce = 2000f;
    public float rockForce = 100f;

    public float dynamiteForce = 100f;

    public LayerMask whatIsGround;

    public Transform feet;

    public Transform throwFrom;

    bool grounded = false;

    private SpriteRenderer _spriteRenderer;

    public float speed = 10;

    public int rockCount = 10;

    public int bulletCount = 10;

    public int dynamiteCount = 10;
    public string weaponType = "rock";

    public GameObject bullet;
    public GameObject dynamite;

    public GameObject rock;

    public GameObject gun;
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

        if(Input.GetMouseButtonDown(1) && dynamiteCount > 0){
            StartCoroutine(throwDynamite());
        }

        if(Input.GetMouseButtonDown(0)){
            if(weaponType == "rock"){
                if(rockCount>0){
                    StartCoroutine(throwRock());
                    rockCount--;
                }
            }
            if(weaponType == "pistol"){
                if(bulletCount>0){
                    StartCoroutine(firePistol());
                    bulletCount--;
                }
            }
            
        }

    }

    void FixedUpdate(){
        
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidBody.velocity = new Vector2(xSpeed, _rigidBody.velocity.y);

        float xScale = transform.localScale.x;

        if((xScale > 0 && xSpeed < 0) || (xScale < 0 && xSpeed > 0)){
            transform.localScale *= new Vector2(-1,1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Spike")){
            hp-=1;
            StartCoroutine(DamageTaken());

            SceneManager.LoadScene("Fail");

        }
        if(other.CompareTag("Enemy")){
            print("HIT");
            hp -= 1;
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
    IEnumerator throwRock(){
        GameObject rockObj;
        Vector3 mousePos = Input.mousePosition;   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 Worldpos2D=new Vector2(Worldpos.x,Worldpos.y);
        rockObj = Instantiate(rock, throwFrom.position, Quaternion.identity);
        Vector2 rockVector = new Vector2((Worldpos2D.x-throwFrom.position.x),(Worldpos2D.y-throwFrom.position.y));
        rockObj.GetComponent<Rigidbody2D>().AddForce(rockVector*rockForce);
        yield return new WaitForSeconds(0);
    }

    IEnumerator throwDynamite(){
        GameObject dynamiteObj;
        Vector3 mousePos = Input.mousePosition;   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 Worldpos2D=new Vector2(Worldpos.x,Worldpos.y);
        dynamiteObj = Instantiate(dynamite, throwFrom.position, Quaternion.identity);
        Vector2 dynamiteVector = new Vector2((Worldpos2D.x-throwFrom.position.x),(Worldpos2D.y-throwFrom.position.y));
        dynamiteObj.GetComponent<Rigidbody2D>().AddForce(dynamiteVector*dynamiteForce);
        dynamiteObj.GetComponent<Rigidbody2D>().AddTorque(1 * 1 * 20f);
        yield return new WaitForSeconds(0);
    }

    IEnumerator firePistol(){
        GameObject shot;
        GameObject gunObj;
        Vector3 mousePos = Input.mousePosition;   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 Worldpos2D=new Vector2(Worldpos.x,Worldpos.y);
        float angle = Mathf.Atan2(Worldpos2D.y - transform.position.y, Worldpos2D.x - transform.position.x) * Mathf.Rad2Deg;
        shot = Instantiate(bullet, throwFrom.position, Quaternion.identity);
        gunObj = Instantiate(gun, transform.position, Quaternion.identity);
        gunObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Vector2 BulletVector = new Vector2((Worldpos2D.x-throwFrom.position.x),(Worldpos2D.y-throwFrom.position.y));
        shot.GetComponent<Rigidbody2D>().AddForce(BulletVector*bulletForce);
        yield return new WaitForSeconds(.1f);
        Destroy(gunObj);
    }
}

