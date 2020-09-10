using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    private AudioSource gameSounds;

    private void Awake() => gameSounds = GetComponent<AudioSource>();


    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

    private void PlaySound() => gameSounds.Play();

}
