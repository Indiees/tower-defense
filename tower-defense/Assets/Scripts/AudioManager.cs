using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;
    private AudioSource audioSource;

    private void Awake() {
        if(ins != null && ins != this)
            Destroy(this.gameObject);
        else    
            ins = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }
}
