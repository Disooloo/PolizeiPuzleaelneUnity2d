using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_clickFX;

    public void ClickSouse() => 
        m_AudioSource.PlayOneShot(m_clickFX);
}
