using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    //public Text Dollars; //����� �������� ���������� � ����
    public Text ScoreResult, ScoreLoose; //���� ������� ������ �� ��, ������� ����� �� �������� �� ������ � ����������� ����

    private void FixedUpdate()
    {
        //Dollars.text = "�����: " + PlayerPrefs.GetFloat("dollars");
        ScoreResult.text = PlayerPrefs.GetFloat("score").ToString();


        if (PlayerPrefs.GetFloat("score") >= PlayerPrefs.GetFloat("maxScore"))
            ScoreLoose.text = "<size=36>����</size><color=#27C000>\n" + PlayerPrefs.GetFloat("maxScore") + "\n<size=36>����� ������ !</size></color>";
        if (PlayerPrefs.GetFloat("score") < PlayerPrefs.GetFloat("maxScore"))
        {
            ScoreLoose.text = "<size=36>����</size><color=#27C000>\n" + PlayerPrefs.GetFloat("score") + "\n</color><size=36>������ ���������: </size><size=50>" + PlayerPrefs.GetFloat("maxScore")+"</size>";
        }
    }
}
