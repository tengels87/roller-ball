using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        print(other.name);
        if (other.tag == "Player") {
            kill();
        }
    }

    private void kill() {
        Object.Destroy(this.gameObject);
    }
}
