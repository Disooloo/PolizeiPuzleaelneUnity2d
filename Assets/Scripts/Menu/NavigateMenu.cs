using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateMenu : MonoBehaviour
{
    public void OpenLevelMenu() =>
        SceneManager.LoadScene("LevelMenu");

    public void OpenStartingWindows() =>
        SceneManager.LoadScene("Menu");

    public void LoadScene() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
