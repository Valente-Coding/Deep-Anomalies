using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    public void ChangeMaster(float p_Value)
    {
        m_AudioMixer.SetFloat("MasterVolume", Mathf.Log10(p_Value) * 20);
    }

    public void ChangeMusic(float p_Value)
    {
        m_AudioMixer.SetFloat("MusicVolume", Mathf.Log10(p_Value) * 20);
    }
}
