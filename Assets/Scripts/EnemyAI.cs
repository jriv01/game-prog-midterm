using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    Rigidbody2D _rigidbody2D;
    public float x_speed, lookDst;
    public bool avian;
    Transform player;
    Vector2 starting_pos;
    public int hp=3;



    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        starting_pos = transform.position;
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop(){
        while (true){
            yield return new WaitForSeconds(.1f);
            if (Vector2.Distance(transform.position,player.position) < lookDst){
                if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                    player.position.x < transform.position.x && transform.localScale.x > 0){
                        transform.localScale *= new Vector2(-1,1);
                    }

                Vector2 dir = (player.position - transform.position);
                if(avian){
                    _rigidbody2D.velocity = dir.normalized * x_speed;
                } else {
                    _rigidbody2D.velocity = new Vector2(dir.normalized.x * x_speed, 0);
                }
                
            } else {
                Vector2 dir_center = new Vector2(starting_pos.x - transform.position.x, starting_pos.y - transform.position.y);
                if(avian){
                    _rigidbody2D.velocity = dir_center.normalized * x_speed*0.5f;
                } else {
                    _rigidbody2D.velocity = new Vector2(dir_center.normalized.x * x_speed*0.5f, 0);
                }
                float margin_err = .5f;
                if((transform.position.x >= starting_pos.x-margin_err && transform.position.x <= starting_pos.x+margin_err)
                  && (transform.position.y >= starting_pos.y-margin_err && transform.position.y <= starting_pos.y + margin_err)){
                    _rigidbody2D.velocity = Vector2.zero;
                }
                

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
    
        if(other.CompareTag("bounds")){
            print("HIT the bounds");
            x_speed = 0;
            
            
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
        if(hp<=0 || hp==0){
            print("dead");
            Destroy(gameObject);
        }
        

    }


}
