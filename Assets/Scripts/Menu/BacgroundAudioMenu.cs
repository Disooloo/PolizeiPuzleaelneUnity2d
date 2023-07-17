using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacgroundAudio : MonoBehaviour
{
    [SerializeField] private Sprite _onMusic;
    [SerializeField] private Sprite _offMusic;
    [SerializeField] private Image _musicButton;
    [SerializeField] private bool _isOn;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _isOn = true;
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("music") == 0)
        {
            _musicButton.GetComponent<Image>().sprite = _onMusic;
            _audioSource.enabled = true;
            _isOn = true;
        }
        else
        {
            _musicButton.GetComponent <Image>().sprite = _offMusic;
            _audioSource.enabled = false;  
            _isOn = false;
        }
    }

    public void OffSound()
    {
        if (!_isOn) 
            PlayerPrefs.SetInt("music", 0);
        else if (_isOn)
            PlayerPrefs.SetInt("music", 1);
    }
}
