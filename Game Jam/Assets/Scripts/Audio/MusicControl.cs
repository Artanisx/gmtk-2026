using System;
using UnityEngine;

/// <summary>
/// Class that controls music using events that will change music
/// </summary>
public class MusicControl : MonoBehaviour
{
    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _alertedTheme;

    private AudioSource _source;
    
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        
        _source.clip = _mainTheme;
        _source.Play();
    }
}
