using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelecter : MonoBehaviour {

    public static CharaSelecter ins;

    public enum Chara { none, mari, sehui, c, d}
    public Chara chara;

    public Button[] charaSelectBtn = new Button[4];

    public GameObject charaMainPanel;
    public Sprite[] charaImage = new Sprite[4];
    public Text[] charaExplainText = new Text[2];

    public Color[] color = new Color[2];

    private void Awake()
    {
        if (ins == null)
            ins = this;
        else if (ins != null)
            Destroy(gameObject);
    }

    public void Btn_CharaImage(string num)
    {
        charaMainPanel.GetComponent<Image>().sprite = charaImage[int.Parse(num)];
        charaExplainText[0].text = "";

        switch(num)
        {
            case "0":
                charaExplainText[1].text = "마리";
                chara = Chara.mari;
                break;
            case "1":
                charaExplainText[1].text = "세희";
                chara = Chara.sehui;
                break;
            case "2":
                charaExplainText[1].text = "미정";
                chara = Chara.c;
                break;
            case "3":
                charaExplainText[1].text = "미정";
                chara = Chara.d;
                break;
        }
    }
}
