using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidBody; 
    public float jumpForce = 200f;

    public static int hp;

    public float bulletForce = 2000f;
    public float rockForce = 100f;

    public float dynamiteForce = 100f;

    public LayerMask whatIsGround;

    public Transform feet;

    public Transform throwFrom;

    bool grounded = false;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    public float speed = 10;

    public int rockCount = 10;

    public int bulletCount = 10;

    public int dynamiteCount = 3;
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
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public static void resetHPTo(int val){
        hp = val;
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position,.1f,whatIsGround);
        _animator.SetBool("IsGrounded", grounded);
        if(hp<=0){
            SceneManager.LoadScene("Fail");
        }

        if(Input.GetKeyDown("space") && grounded){
            _rigidBody.AddForce(new Vector2(0,jumpForce));
        }       

        

        if(Input.GetMouseButtonDown(1) && dynamiteCount > 0){
            StartCoroutine(throwDynamite());
            dynamiteCount--;
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
                    _animator.SetBool("IsShooting", true);
                    StartCoroutine(firePistol());
                    bulletCount--;
                    
                }
            }
            
        }

    }

    void FixedUpdate(){
        
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidBody.velocity = new Vector2(xSpeed, _rigidBody.velocity.y);
        _animator.SetFloat("Speed", Math.Abs(xSpeed));
        float xScale = transform.localScale.x;

        if((xScale > 0 && xSpeed < 0) || (xScale < 0 && xSpeed > 0)){
            transform.localScale *= new Vector2(-1,1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Spike")){
            print(hp);
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
        if(other.CompareTag("gunCollect")){
            weaponType = "pistol";
        }
        if(other.CompareTag("magCollect")){
            bulletCount+=10;
        }
        if(other.CompareTag("rockPileCollect")){
            rockCount+=10;
        }
        if(other.CompareTag("TNTBox")){
            dynamiteCount+=5;
        }

        // if(other.CompareTag("Enemy")){
        //     print("HIT");
        //     hp -= 1;
        //     StartCoroutine(DamageTaken());
        // }

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
        float xScale = transform.localScale.x;

        if((xScale > 0 && Worldpos2D.x < transform.position.x) || (xScale < 0 && Worldpos2D.x > transform.position.x)){
            transform.localScale *= new Vector2(-1,1);
        }

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
        shot = Instantiate(bullet, transform.position + new Vector3(-.058f,.287f,0), Quaternion.identity);
        gunObj = Instantiate(gun, transform.position + new Vector3(-.058f,.287f,0), Quaternion.identity);
        gunObj.transform.parent = transform;
        float xScale = transform.localScale.x;
        if(xScale < 0){
            gunObj.transform.localScale *= new Vector2(1,-1);
        }
        if((xScale > 0 && ((angle>=90 && angle<180) || (angle>-180 && angle<=-90))) || (xScale < 0 && ((angle<=90 && angle>0) || (angle<0 && angle>=-90)))){
            transform.localScale *= new Vector2(-1,1);
            gunObj.transform.localScale *= new Vector2(-1,1);
            gunObj.transform.localScale *= new Vector2(1,-1);
        }
        if(angle<0){
            gunObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180+(180-(-angle))));
        }
        else{
            gunObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        
        Vector2 BulletVector = new Vector2((Worldpos2D.x-throwFrom.position.x),(Worldpos2D.y-throwFrom.position.y));
        shot.GetComponent<Rigidbody2D>().AddForce(BulletVector*bulletForce);
        yield return new WaitForSeconds(.1f);
        Destroy(gunObj);
        _animator.SetBool("IsShooting", false);
    }
}
