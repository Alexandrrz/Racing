using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    //public Text Dollars; //Ѕабки которыми пользуютс€ в игре
    public Text ScoreResult, ScoreLoose; //ќчки которые вли€ют на то, сколько бабок ты получишь от обгона и пройденного пути

    private void FixedUpdate()
    {
        //Dollars.text = "Ѕабки: " + PlayerPrefs.GetFloat("dollars");
        ScoreResult.text = PlayerPrefs.GetFloat("score").ToString();


        if (PlayerPrefs.GetFloat("score") >= PlayerPrefs.GetFloat("maxScore"))
            ScoreLoose.text = "<size=36>—„≈“</size><color=#27C000>\n" + PlayerPrefs.GetFloat("maxScore") + "\n<size=36>Ќќ¬џ… –≈ ќ–ƒ !</size></color>";
        if (PlayerPrefs.GetFloat("score") < PlayerPrefs.GetFloat("maxScore"))
        {
            ScoreLoose.text = "<size=36>—„≈“</size><color=#27C000>\n" + PlayerPrefs.GetFloat("score") + "\n</color><size=36>Ћ”„Ў»… –≈«”Ћ№“ј“: </size><size=50>" + PlayerPrefs.GetFloat("maxScore")+"</size>";
        }
    }
}
