using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource footStep1;
    [SerializeField] AudioSource footStep2;
    [SerializeField] AudioSource jump;

    [SerializeField] Collider collider;
    [SerializeField] AudioSource ouch;
    float ouchtimer;

    void Start()
    {
        ouchtimer = 0;
    }

    void Update()
    {
        ouchtimer += Time.deltaTime;
        if(ouchtimer >= 100) { ouchtimer = 10; }
    }

    void PlayFootStep1()
    {
        footStep1.Play();
    }
    void PlayFootStep2()
    {
        footStep2.Play();
    }

    void PlayJump()
    {
        jump.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bee" && ouchtimer >= 1.5)
        {
            ouch.Play();
            ouchtimer = 0;
        }
    }
}
