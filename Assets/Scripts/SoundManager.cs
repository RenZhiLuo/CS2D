using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ClipType
{
    Footstep,
    Shoot,
    PlayerHurt,
    EnemyHurt,
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;

    [Header("Charater")]
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        GameSettingPanel.instance.volumeSlide.onValueChanged.AddListener((value) => { audioSource.volume = value;audioSource2.volume = value; });
        audioSource.volume = GameSettingPanel.instance.volumeSlide.value;
        audioSource2.volume = GameSettingPanel.instance.volumeSlide.value;
    }

    public void PlayAudio(ClipType index)
    {
        audioSource.PlayOneShot(audioClips[(int)index]);
    }
    public void PlayAttack()
    {
        audioSource2.PlayOneShot(audioClips[1]);
    }

}
