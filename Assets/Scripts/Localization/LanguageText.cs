using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private int language;
    [SerializeField] private string[] text;

    private Text textLine;

    void Start()
    {
        language = PlayerPrefs.GetInt("language", language); //��������� ����
        textLine = GetComponent<Text>(); //���������� � ����������
        textLine.text = "" + text[language]; //��������� ����� ����� ����� ������������, � ����������� ������������ �����
    }
}