using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject _uiPopapCode;

    private bool isPlayerNearby = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _uiPopapCode.SetActive(true);
        }
    }
}
