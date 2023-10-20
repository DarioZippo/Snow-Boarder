using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour {
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    CircleCollider2D playerHead;
    bool hasCrashed = false;

    private void Start() {
        playerHead = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(hasCrashed == false) {
            if(other.gameObject.tag == "Ground" && playerHead.IsTouching(other.collider)) {
                ManageCrash();
            }
        }
    }

    private void ManageCrash() {
        hasCrashed = true;

        FindObjectOfType<PlayerController>().DisableControls();
        crashEffect.Play();
        GetComponent<AudioSource>().PlayOneShot(crashSFX);
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
