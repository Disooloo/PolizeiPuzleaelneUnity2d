using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu (fileName = "new Map", menuName = "Scriptable objects/Map")]
public class MapLevelMenu : ScriptableObject
{
    public int _indexMap;
    public string _mapName;
    public string _mapDescription;
    public Sprite _imageMap;
    //public SceneAsset sceneToObject;
    public string sceneToObject;
}

