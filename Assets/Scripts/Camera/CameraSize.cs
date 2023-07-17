using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    [SerializeField] private GameObject _bacgroundImage;

    private void Start() => ScreenCredentoals();

    private void ScreenCredentoals()
    {
        var height = Camera.main.orthographicSize * 2;
        var width = height * Screen.width / Screen.height;
        var backg = _bacgroundImage.GetComponent<SpriteRenderer>().sprite;
        var unitwidth = backg.textureRect.width / backg.pixelsPerUnit;
        var unitHeight = backg.textureRect.height / backg.pixelsPerUnit;
        _bacgroundImage.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(width / unitwidth, height / unitHeight);
    }
}
