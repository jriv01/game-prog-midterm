using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool dropping = false;
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player") && !dropping){
            dropping = true;
            StartCoroutine(Drop());
        }
    }
    
    IEnumerator Drop()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
