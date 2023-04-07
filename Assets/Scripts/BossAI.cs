using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    float moveSpeed = .01f;
    int rotationSpeed = 120;
    float aimSpeed = .5f;
    int arrowForce = 1500;
    public GameObject arrowPrefab,swordEnemy,summonBubble,bossDiePrefab;
    public AudioClip fireArrow,summon_clip,laughter_clip;
    public Transform shotPos;
    Transform player;
    AudioSource _audioSource;

    int hp = 3;
    

    private Animator _animator;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        NextAttack();
    }

    void NextAttack(){
        StopAllCoroutines();
        _audioSource.PlayOneShot(laughter_clip, 1);
        int state = Random.Range(0,5);
        print(state);
        _animator.SetBool("isFired",false);
        _animator.SetBool("isButcher",false);
        if(!(player.position.x > transform.position.x && transform.localScale.x < 0 || 
            player.position.x < transform.position.x && transform.localScale.x > 0)){
                transform.localScale *= new Vector2(-1,1);
        }
        // state = 3;
        switch (state) {
            case 0:
                StartCoroutine(SpinAttack());
                break;
            case 1:
                StartCoroutine(AimAttack());
                break;
            case 2:
                StartCoroutine(MoveToPlayer());
                break;
            case 3:
                StartCoroutine(SpawnEnemy());
                break;
            default:
                StartCoroutine(Idle());
                break;
        }
    }

    IEnumerator Idle(){
        yield return new WaitForSeconds(1);
        NextAttack();
    }

    IEnumerator SpinAttack(){
        StartCoroutine(Fire(10,.5f, .5f));
        float t = 0;
        while(t < 360f/rotationSpeed){
            transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(ResetRotation());
        NextAttack();
    }

    IEnumerator AimAttack(){
        StartCoroutine(Fire(4,.5f,.5f));
        float t = 0;
        while( t < 1 ){
            transform.up = Vector3.Lerp(transform.up, player.position-transform.position,t);
            t += aimSpeed * Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(ResetRotation());
        NextAttack();
    }

    IEnumerator MoveToPlayer(){
        float t = 0;
        while (t<.8f){
            transform.position = Vector2.Lerp(transform.position, player.position, t*moveSpeed);
            t += Time.deltaTime;
            yield return null;
        }
        _animator.SetBool("isButcher",true);
        yield return new WaitForSeconds( 2.5f);
        NextAttack();
    }

    IEnumerator SpawnEnemy(){
        GameObject sum_text = Instantiate(summonBubble, new Vector2(shotPos.position.x+3.5f,shotPos.position.y), Quaternion.identity);
        _audioSource.PlayOneShot(summon_clip, 1);
        yield return new WaitForSeconds(2);
        for(int i = 0; i < 2; i++){
            float x_pos = Random.Range(-3,30);
            float y_pos = -4.01f;
            GameObject enemy = Instantiate(swordEnemy,new Vector2(x_pos,y_pos),Quaternion.identity);
            EnemyAI script = enemy.GetComponent<EnemyAI>();
            script.lookDst = 20;
        }
        Destroy(sum_text);
        NextAttack();

    }



    IEnumerator Fire(int shotNum, float reload, float delay){
        _animator.SetBool("isFired",true);
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < shotNum; i++){
            _audioSource.PlayOneShot(fireArrow, 1);
            GameObject newArrow = Instantiate(arrowPrefab,shotPos.position, transform.rotation*Quaternion.Euler(0,0,-20));
            newArrow.GetComponent<Rigidbody2D>().AddForce(transform.up*arrowForce);
            yield return new WaitForSeconds(reload);
        }
    }

    IEnumerator ResetRotation(){
        float t = 0;
        while (t<1){
            transform.up = Vector3.Lerp(transform.up, Vector2.up , t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other){
    
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
            Instantiate(bossDiePrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
        

    }
}

