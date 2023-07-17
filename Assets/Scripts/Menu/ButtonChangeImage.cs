using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeImage : MonoBehaviour
{
    [SerializeField] private Sprite _newButtonImage;

    public void OnClick() =>
        GetComponent<Image>().sprite = _newButtonImage;

}
