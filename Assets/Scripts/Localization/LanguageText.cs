using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private int language;
    [SerializeField] private string[] text;

    private Text textLine;

    void Start()
    {
        language = PlayerPrefs.GetInt("language", language); //загружаем язык
        textLine = GetComponent<Text>(); //обращаемся к компоненту
        textLine.text = "" + text[language]; //указываем какой текст будет отображаться, в зависимости отвыбранного языка
    }
}