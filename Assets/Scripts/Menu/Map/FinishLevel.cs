using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public void FinishLVL()
    {
        LoadScene();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.tag == "Player")
            {
                PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(1);
            }
        }
    }
    /*
    private void LoadScene()
    {
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }
    */

    private void LoadScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("currentScene", nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
