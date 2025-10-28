using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip DemoIntro;
    public AudioClip DemoBGM;

    IEnumerator Start()
    {
        AudioSource BGM = GetComponent<AudioSource>();
        BGM.clip = DemoIntro;
        BGM.loop = false;
        BGM.Play();

        yield return new WaitForSeconds(DemoIntro.length);
        BGM.clip = DemoBGM;
        BGM.loop = true;
        BGM.Play();
    }
}
