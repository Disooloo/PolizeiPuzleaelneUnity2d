using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _mapName;
    [SerializeField] private TMP_Text _mapDescription;
    [SerializeField] private Image _mapImage;
    [SerializeField] private Button _mapButtonPlay;
    [SerializeField] private GameObject _mapLockImage;

    public void DisplayMap(MapLevelMenu map)
    {
        _mapName.text = map._mapName;
        _mapDescription.text = map._mapDescription;
        _mapImage.sprite = map._imageMap;

        bool mapUnLocked = PlayerPrefs.GetInt("currentScene", 0) >= map._indexMap;

        _mapLockImage.SetActive(!mapUnLocked);
        _mapButtonPlay.interactable = mapUnLocked;

        if (mapUnLocked)
            _mapImage.color = Color.white;
        else
            _mapImage.color = Color.grey;

        _mapButtonPlay.onClick.RemoveAllListeners();
        _mapButtonPlay.onClick.AddListener(() => SceneManager.LoadScene(map.sceneToObject));
    }
}
