using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    public float lifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeSpan());
    }

    IEnumerator LifeSpan() {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

}
