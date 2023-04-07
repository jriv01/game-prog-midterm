using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    public GameObject arrow;
    public Transform center;
    public float lookDst;
    public AudioClip fireArrow;
    Transform player;
    Rigidbody2D _rigidbody2D;
    AudioSource _audioSource;
    // Start is called before the first frame update
    float arrow_speed = 5.5f;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FireArrow());
    }

    IEnumerator FireArrow(){
        while(true){
            yield return new WaitForSeconds(2);
            //Plays a specific clip on an Audio Source once
            _audioSource.PlayOneShot(fireArrow, 1);
            if (Vector2.Distance(transform.position,player.position) < lookDst){
                    GameObject arrow_obj;
                    Rigidbody2D arrow_rb; 

                    if(transform.localScale.x < 0){
                        arrow_obj = Instantiate(arrow, center.position, Quaternion.identity);
                        arrow_obj.transform.eulerAngles = new Vector3(0,0,45.5f);
                        arrow_rb = arrow_obj.GetComponent<Rigidbody2D>();
                        arrow_rb.velocity = new Vector2(-1*arrow_speed,0);
                        
                    } else {
                        arrow_obj = Instantiate(arrow, center.position, Quaternion.identity);
                        arrow_obj.transform.eulerAngles = new Vector3(0,0,-138);
                        arrow_rb = arrow_obj.GetComponent<Rigidbody2D>();
                        arrow_rb.velocity = new Vector2(arrow_speed,0);
                    }
            }
            

            
        }
        

    }
}
