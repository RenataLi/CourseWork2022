using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public static MusicManager Mu;

    private void Awake()
    {
        if (Mu == null)
        {
            Mu = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float value)
    {
        _audioSource.volume = value;
    }

}
