using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public ParticleSystem particlesEffect;

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
        particlesEffect.Play();

        Object.Destroy(this.gameObject);
    }
}
