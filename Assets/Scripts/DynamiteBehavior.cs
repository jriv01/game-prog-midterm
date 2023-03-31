using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBehavior : MonoBehaviour
{

    public GameObject explosion;
    public GameObject damageZone;

    public float timeToBlow = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blowUp());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator blowUp(){
        yield return new WaitForSeconds(timeToBlow);
        Instantiate(damageZone, transform.position, Quaternion.identity);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
