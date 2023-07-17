using UnityEngine;

public class ScriptableObjectController : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] _scriptableObject;
    [SerializeField] private MapDisplay _mapDisplay;
    [SerializeField] private int _currentIndex;


    private void Awake()
    {
        ChangeScriptableObject(0);
    }

    public void ChangeScriptableObject(int change)
    {
        _currentIndex += change;
        if (_currentIndex < 0 ) _currentIndex = _scriptableObject.Length - 1;
        else if (_currentIndex > _scriptableObject.Length - 1) _currentIndex = 0;

        if (_mapDisplay != null) _mapDisplay.DisplayMap((MapLevelMenu)_scriptableObject[_currentIndex]);
    }
}
