using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float bounceMagnitude = 1f;
    public float bounceTime = 1;
    Vector3 initialPosition;
    void Start() {
        initialPosition = gameObject.transform.position;
    }

    void Update() {
        gameObject.transform.position = new Vector3(
            initialPosition.x,
            initialPosition.y + Mathf.SmoothStep(-1 * bounceMagnitude, 1 * bounceMagnitude, Mathf.PingPong(Time.time / bounceTime, 1)),
            initialPosition.z
        );
    }
}
