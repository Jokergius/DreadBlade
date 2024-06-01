using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public AudioSource audioSrc;


	void Start () {
        if(!PlayerPrefs.HasKey("volume")) audioSrc.volume = 1;
	}

	void Update () {

        audioSrc.volume = PlayerPrefs.GetFloat("volume");
	}

}
