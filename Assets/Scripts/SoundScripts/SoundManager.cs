using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class manages the audio for entire game
 * Class SoundType is serialized so that we can assign clips to different sound type
*/
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SoundEffect;
    [SerializeField] private AudioSource SoundMusic;
    [SerializeField] public SoundType[] Sou;

    private void Start()
    {
        PlayMusic();
    }
    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sou, i => i.soundtype == sound);
        if (item != null)
        {
            return item.soundclip;
        }
        else
        {
            return null;
        }
    }
    public void PlaySound(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Audio Not Assigned");
        }
    }
    private void PlayMusic()
    {
        SoundMusic.Play();
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundtype;
    public AudioClip soundclip;
}
public enum Sounds
{
    ButtonClickSound,
    CorrectWordSound,
    IncorrectWordSound,
    CellClickSound,
    LevelOverSound,   
}