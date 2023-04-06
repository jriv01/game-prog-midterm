using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    public GameObject arrow;
    public Transform center;
    Transform player;
    Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    float arrow_speed = 5.5f;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FireArrow());
    }

    IEnumerator FireArrow(){
        while(true){
            GameObject arrow_obj;
            yield return new WaitForSeconds(2);
            

            Rigidbody2D arrow_rb; 
            
            float arr_speed_x = player.position.normalized.x * arrow_speed;
            if(transform.localScale.x < 0){
                arrow_obj = Instantiate(arrow, center.position, Quaternion.identity);
                arrow_obj.transform.eulerAngles = new Vector3(0,0,45.5f);
                arrow_rb = arrow_obj.GetComponent<Rigidbody2D>();
                arrow_rb.velocity = new Vector2(arr_speed_x,0);
                
            } else {
                arrow_obj = Instantiate(arrow, center.position, Quaternion.identity);
                arrow_obj.transform.eulerAngles = new Vector3(0,0,-138);
                arrow_rb = arrow_obj.GetComponent<Rigidbody2D>();
                arrow_rb.velocity = new Vector2(-1*arr_speed_x,0);
            }

            
        }
        

    }
}
