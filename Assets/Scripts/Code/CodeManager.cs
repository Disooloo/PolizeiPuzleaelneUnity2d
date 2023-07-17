using UnityEngine;
using UnityEngine.UI;

public class UIPopapCode : MonoBehaviour
{
    private ParkourMovement player;
    public string code;
    public Text textCode;

    public GameObject _uiPopapCode;
    public GameObject TitleThedoorisunlocked;
    public GameObject _wallForCodeController;
    public GameObject _buttonOpenCode;
    public GameObject Effect;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ParkourMovement>();
    }

    private void Update()
    {
        if (code == "")
            textCode.text = "Enter code...";
        else
            textCode.text = code;
        
    }

    #region buttons
    public void Button_1() => code += 1;
    public void Button_2() => code += 2;
    public void Button_3() => code += 3;
    public void Button_4() => code += 4;
    public void Button_5() => code += 5;
    public void Button_6() => code += 6;
    public void Button_7() => code += 7;
    public void Button_8() => code += 8;
    public void Button_9() => code += 9;
    public void Button_0() => code += 0;


    public void Button_Enter()
    {

        if (textCode.text == "1276")
        {
            _uiPopapCode.SetActive(false);
            _wallForCodeController.SetActive(false);
            _buttonOpenCode.SetActive(false);

            TitleThedoorisunlocked.SetActive(true);
            Instantiate(Effect, new Vector3(88, -70, 0), Quaternion.identity);

        }

        else
            textCode.text = "Not code"; 

        code = "";
    }
    public void Button_Clear()
    {
        code = "";
    }

    #endregion
}

