using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    public AudioClip BGMforMenu;

    void Start()
    {
        AudioSource MenuBGM = GetComponent<AudioSource>();
        MenuBGM.clip = BGMforMenu;
        MenuBGM.loop = false;
        MenuBGM.Play();
    }
}
