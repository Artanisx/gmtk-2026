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
        EventManager.PlayerWasSeen.AddListener(ChangeMusic);
    }
    
    //Make changes to source audio via EventManager
    private void ChangeMusic(bool wasSeen)
    {
        if (wasSeen && _source.clip != _alertedTheme)
        {
            _source.clip = _alertedTheme;
            _source.Play();
            return;
        }

        if (_source.clip == _mainTheme)
        {
            return;
        }
        
        _source.clip = _mainTheme;
        _source.Play();
    }
}
