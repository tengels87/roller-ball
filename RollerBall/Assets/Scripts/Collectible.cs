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
        print(other.tag);
        if (other.tag == "Player") {
            if (this.gameObject.tag == "Finish") {
                GameManager gameManager = FindFirstObjectByType<GameManager>();
                if (gameManager != null) {
                    gameManager.setLevelFinished(true);
                }
            }

            kill();
        }
    }

    private void kill() {
        if (particlesEffect != null) {
            particlesEffect.Play();
        }

        Object.Destroy(this.gameObject);
    }
}
