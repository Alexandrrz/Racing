using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject StartTimer, speed;
    public GameObject[] InvisePanel = new GameObject[8];

    private bool PlayMode = true;
    private void Start()
    {
        StartCoroutine(StartAnim());
    }
    private void Update()
    {
        if (PlayMode)
        {
            StartTimer.SetActive(true);
            InvisePanel[0].SetActive(false);
            InvisePanel[1].SetActive(false);
            InvisePanel[2].SetActive(false);
            InvisePanel[3].SetActive(false);
            InvisePanel[4].SetActive(false);
            InvisePanel[5].SetActive(false);
            InvisePanel[6].SetActive(false);
            InvisePanel[7].SetActive(false);
        }
        if (!PlayMode)
        {
            StartTimer.SetActive(false);
            InvisePanel[0].SetActive(true);
            InvisePanel[1].SetActive(true);
            InvisePanel[2].SetActive(true);
            InvisePanel[3].SetActive(true);
            InvisePanel[4].SetActive(true);
            InvisePanel[5].SetActive(true);
            InvisePanel[6].SetActive(true);
            InvisePanel[7].SetActive(true);
        }
    }
    IEnumerator StartAnim()
    {
        
        yield return new WaitForSeconds(3.8f);
        PlayMode = false;
    }
}