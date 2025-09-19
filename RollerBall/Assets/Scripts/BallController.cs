using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float forceMultiplier = 10f;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {

#if UNITY_EDITOR

        float left = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        float right = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        float up = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        float down = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;

        Vector3 tilt = new Vector3(right - left, up - down, 0);

#else
        
        // Read tilt from device (accelerometer)
        Vector3 tilt = Input.acceleration;

#endif        
        
        // Map X/Y to ball movement (ignore Z)
        Vector3 force = new Vector3(tilt.x, 0, tilt.y) * forceMultiplier;

        // Apply force relative to world
        rb.AddForce(force, ForceMode.Acceleration);

        // respawn when falling down
        if (this.transform.position.y < -2) {
            this.transform.position = new Vector3(2, 2, 0);
        }
    }
}
