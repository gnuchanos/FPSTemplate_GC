using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class CardisHUB : MonoBehaviour {
    public AudioClip InSound;
    public AudioSource audioSource;

    public GameObject R0;
    public GameObject R1;
    public GameObject R2;
    public GameObject R3;

    public GameObject EnegySpiner;

    public float r0r;
    public float r1r;
    public float r2r;
    public float r3r;
    public float timer;

    public Material targetMaterial;
    public string fileName = "screen.png";
    public Texture2D mainTexture;
    public bool MainTextureIsRender = true;

    void Start() {
        InitializeRandoms();
        SetupAudioSource();
        PlayInSound();
    }

    public void InitializeRandoms() {
        r0r = Random.Range(-5f, 5f);
        r1r = Random.Range(-5f, 5f);
        r2r = Random.Range(-5f, 5f);
        r3r = Random.Range(-5f, 5f);
        timer = Random.Range(1, 5);
    }

    public void SetupAudioSource() {
        audioSource = FindObjectOfType<AudioSource>();

        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayInSound() {
        if (InSound != null && !audioSource.isPlaying) {
            audioSource.PlayOneShot(InSound);
        }
    }

    void Update() {
        UpdateTimerAndRandoms();
        RotateObjects();
        PlayInSound();
    }

    public void UpdateTimerAndRandoms() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            InitializeRandoms();
        }
    }

    public void RotateObjects() {
        R0.transform.Rotate(0f, 0f, r0r * Time.deltaTime);
        R1.transform.Rotate(0f, 0f, r1r * Time.deltaTime);
        R2.transform.Rotate(0f, 0f, r2r * Time.deltaTime);
        R3.transform.Rotate(0f, 0f, r3r * Time.deltaTime);

        EnegySpiner.transform.Rotate(0f, 0f, 50f * Time.deltaTime);
    }


}