using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource playerShootingAudio;
    
    public void PlayShootingClip()
    {
        if (playerShootingAudio != null)
        {
            playerShootingAudio.Play();
        }
    }
   
}
