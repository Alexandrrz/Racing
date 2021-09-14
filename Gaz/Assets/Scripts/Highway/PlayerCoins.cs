using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(RCC_DashboardInputs))]
public class PlayerCoins : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform CameraRCC;
    public Text DistanceTxt; //���������� ������� ��������, ������� �� �����
    public GameObject[] scoreAnim = new GameObject[9];
    public GameObject LoosePanel, SpeedPanel, Obgons;
    public GameObject[] InvizDashboardLoose = new GameObject[8];
    public Rigidbody RbPlayer;
    public Text SpeedTime, SpeedTimeResult, DistanceResult, ObgonResult;
    public Text DollarsObgoon, DollarsTiime, DollarsRaange, DollarsRessult, sssssss;

    private Vector3 CameraEnd;
    private bool CamMoove = false;

    private float Distance; //���������� ������� ��������
    private int i = 100, n = 1, ObgonNumbers = 0, ObgonNumbersResult = 0;
    private float j = 10f;
    private float maxScore;

    private float DistanceTransform = 20f;
    private int peremen = 0, prover;
    private float ScoreResult = 0f; //���� ������� ������ �� ��, ������� ����� �� �������� �� ������ � ����������� ����
    private bool enab = true;

    private float DollarsRange = 0f; //����� �� ���������� ����
    private float DollarsObgon = 0f; //����� �� ������
    private float DollarsTime = 0f; //����� �� ����� �� ������� ��������
    private float DollarsResult; //����� ��� ������ � PlayerPrefs

    //����������, ������� �������� �� ���������� ����� ����� 100��
    private RCC_DashboardInputs inputs;
    private float speedMin;

    //������ ��� ������
    private DateTime timeBegin;
    private TimeSpan timeTimer, timerZero = new TimeSpan (0,0,0);
    private bool sec = true, timerActivated = false, IsLoos;

    //������ ��� ��������� ������
    private DateTime timeBeginCamera;
    private TimeSpan timeTimerCamera;
    private bool secCamera = true, timerActivatedCamera = false;

    //2 ������� ��� �������� ������� ��������, ���� ��� ������ � ������ ����, � ������ ��� �������� ������ ������� � ������ �����
    private DateTime timeBeginSpeed, timeBeginSpeedGame;
    private TimeSpan timeTimerSpeed, timeTimerSpeedGame;
    private bool secSpeed = true, secSpeedGame = true, timerActivatedSpeed = false, timerActivatedSpeedGame = false;
    private double SpeedTimeMax, SpeedTimeMaxResult;
    public Text ann;


    private float _dollarsPlus; //���������� ���� ����� ����� ������ � ��������� ����������
    private bool _babki = false; //�������������� ��������, ����� ����� ������ ������ ����������� ������ 1 ���
    private void Start()
    {
        inputs = GetComponent<RCC_DashboardInputs>(); //����� ���������� �� rcc ��� �������� ��������
        DistanceTransform += PlayerTransform.position.z;
        Distance = 0.0f;
        timerActivated = true;
        IsLoos = false;
        _dollarsPlus = PlayerPrefs.GetFloat("Dollars");
        sssssss.text = PlayerPrefs.GetFloat("Dollars").ToString();
        _babki = true;
    }
    private void FixedUpdate()
    {
        
        CameraEnd = new Vector3(CameraRCC.position.x, CameraRCC.position.y, CameraRCC.position.z + 2);
        if (CamMoove && timerActivatedCamera)
            CameraRCC.position = Vector3.MoveTowards(CameraRCC.position, CameraEnd, 25 * Time.deltaTime);

        if (timeTimerCamera.Seconds > 0.5f)
        {
            CamMoove = false;
            timerActivatedCamera = false;
        }
        if (timerActivatedCamera)
        {
            if (secCamera)
                timeBeginCamera = DateTime.Now;
            secCamera = false;
            timeTimerCamera = DateTime.Now - timeBeginCamera;
        }

        speedMin = inputs.KMH; //���������� ������� �������� � ����������, ����� ����� ���� ��������

        //������ ��� ������ � ���������� �����
        if (timerActivated)
        {
            if (sec)
                timeBegin = DateTime.Now;
            sec = false;
            timeTimer = DateTime.Now - timeBegin;            
            //Debug.Log(timeTimer);
            
        }        
        if (timeTimer.Seconds > 5)
        {
            n = 1;
            i = 100 * n; // ����
            j = 15 * n; // �����
            ObgonNumbers = 0;
            Obgons.SetActive(false);
        }

      /*  //������������ ���������� ����� ������� ������ �����               !!!!!!!!!!!!���������� � ������������
        if (PlayerPrefs.GetFloat("score") > PlayerPrefs.GetFloat("maxScore"))
        {
            maxScore = PlayerPrefs.GetFloat("score");
            PlayerPrefs.SetFloat("maxScore", maxScore);
        }       */

        //PlayerPrefs.SetFloat("dollars", Dollars);

        //������ ����������� ���������� � ���������� ����� 
        if (PlayerTransform.position.z >= DistanceTransform)
        {
            Distance += 0.01f;
            DollarsRange += 1.5f;
            Distance = (float)Math.Round((float)Distance, 4);
            PlayerPrefs.SetFloat("distance", Distance);
            
            DistanceTxt.text = PlayerPrefs.GetFloat("distance") + " KM";
            DistanceTransform = PlayerTransform.position.z + 40f;

            // ���������� ���� � ����� �� �����
            PlayerPrefs.SetFloat("score", ScoreResult);
            ScoreResult += 10f; //���� �� 0,01 �� ����������� ����
            //Dollars += 1.5f; //����� �� 0.01 �� ����������� ����            
        }

        //������ ��� �������� ������ ������� �� ������� �������� (��� ������ � ���� ��������� � �������� �����/�����)
        if (speedMin >= 100)
        {
            timerActivatedSpeed = true;           
            if (timerActivatedSpeed)
            {
                if (secSpeed)
                    timeBeginSpeed = DateTime.Now;
                secSpeed = false;
                timeTimerSpeed = DateTime.Now - timeBeginSpeed;
                SpeedTimeMaxResult = timeTimerSpeed.TotalSeconds;
                SpeedTimeMaxResult = (double)Math.Round((double)SpeedTimeMaxResult, 1);
                DollarsTime += 0.01f;
                //Debug.Log(DollarsTime);
            }
        }
        if (speedMin < 100)
        {
            //secSpeed = true;
            timerActivatedSpeed = false;
            timeBeginSpeed = DateTime.Now - timeTimerSpeed; // ��������� ��������� ��� ����� ����� � ������ ���� �������� ������ 100��.� ��� ���� ���������
            //Debug.Log(timeBeginSpeed);
        }

        //������ ��� �������� ������� �� ������� �������� �� ����� ���� (����� ������������ �� ����� ����)
        if (speedMin >= 100 && Time.timeScale == 1)
        {
            
                timerActivatedSpeedGame = true;
                SpeedPanel.SetActive(true);
                if (timerActivatedSpeedGame)
                {
                    if (secSpeedGame)
                        timeBeginSpeedGame = DateTime.Now;
                    secSpeedGame = false;
                    timeTimerSpeedGame = DateTime.Now - timeBeginSpeedGame;
                    SpeedTimeMax = timeTimerSpeedGame.TotalSeconds;
                    SpeedTimeMax = (double)Math.Round((double)SpeedTimeMax, 1);
                    SpeedTime.text = "  �� ������� ��������: <color=#27C000>" + SpeedTimeMax + "c</color>";
                    
                }

        }
        if (Time.timeScale == 0)
        {
            timerActivatedSpeedGame = false;
            SpeedPanel.SetActive(false);
            timeBeginSpeedGame = DateTime.Now;

            timerActivated = false;
            timeBegin = DateTime.Now;
        }
        if (speedMin < 100)
        {
            SpeedPanel.SetActive(false);
            secSpeedGame = true;
            timerActivatedSpeedGame = false;
        }
        
    }

    // ����� �� ������ ����� � ���������� ����
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bot" && enab && speedMin >= 100)
        {
            
            enab = false;
            peremen++;
            ScoreResult += i; //����
            DollarsObgon += j; //�����
           
            timerActivated = true;
            sec = true;
            timeTimer = timerZero;
            ObgonNumbersResult++;

            //���� ����� �� ������� ������ 5, �� ���� ��� ������ ������ ������������� �� 100 �� 900, � ���� ����� ������ 5 ��� �� ����������� �� 100 �������, ��� ��������� � �������
            if (timeTimer.Seconds <= 5)
            {
                Obgons.SetActive(true);
                
                ObgonNumbers++;
                ann.text = "�����:  <color=#27C000>" + ObgonNumbers.ToString() + "</color>";
                if (n == 1)
                    scoreAnim[0].SetActive(true);
                if (n == 2)
                    scoreAnim[1].SetActive(true);
                if (n == 3)
                    scoreAnim[2].SetActive(true);
                if (n == 4)
                    scoreAnim[3].SetActive(true);
                if (n == 5)
                    scoreAnim[4].SetActive(true);
                if (n == 6)
                    scoreAnim[5].SetActive(true);
                if (n == 7)
                    scoreAnim[6].SetActive(true);
                if (n == 8)
                    scoreAnim[7].SetActive(true);
                if (n == 9)
                    scoreAnim[8].SetActive(true);
                
                if (n < 9)
                {                    
                    j = 15 * n;
                    i = 100 * n;
                    n++;
                    
                }
                if (n >= 9)
                {
                    n = 9;
                    i = 100 * n;
                    j = 15 * n;
                    
                }
                
                //Debug.Log("����: " + i);
                //Debug.Log("n: " + n);
                //Debug.Log(DollarsObgon);
            }            
        }
        
    }

     // ���� � ����� � ������� ����������� ���� �� ����� �� ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bot" && prover <= peremen)
        {
            prover++;
            enab = true;

        }
        
        if (other.tag == "Bot" && enab && speedMin >= 100)
        {
            scoreAnim[0].SetActive(false);
            scoreAnim[1].SetActive(false);
            scoreAnim[2].SetActive(false);
            scoreAnim[3].SetActive(false);
            scoreAnim[4].SetActive(false);
            scoreAnim[5].SetActive(false);
            scoreAnim[6].SetActive(false);
            scoreAnim[7].SetActive(false);
            scoreAnim[8].SetActive(false);
        }
    }

    //�������� ������� ����������� ����� 0,5 ��� ����� ������������ ������
    IEnumerator StopCar()
    {
        yield return new WaitForSeconds(0.5f);
        if (!IsLoos)
        {
            LoosePanel.SetActive(true);
            
            IsLoos = true;
        }
        
    }

    //�������� ��� ������
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bot") || col.gameObject.CompareTag("Roads")) {

            //������������ ���������� ����� ������� ������ �����
            if (PlayerPrefs.GetFloat("score") > PlayerPrefs.GetFloat("maxScore"))
            {
                maxScore = PlayerPrefs.GetFloat("score");
                PlayerPrefs.SetFloat("maxScore", maxScore);
            }

            RbPlayer.velocity = Vector3.zero; //������������� ������
            RbPlayer.isKinematic = true; //������������� � �� ���� ��������� �������

            //�������� �������� UI
            InvizDashboardLoose[0].SetActive(false);
            InvizDashboardLoose[1].SetActive(false);
            InvizDashboardLoose[2].SetActive(false);
            InvizDashboardLoose[3].SetActive(false);
            InvizDashboardLoose[4].SetActive(false);
            InvizDashboardLoose[5].SetActive(false);
            InvizDashboardLoose[6].SetActive(false);
            InvizDashboardLoose[7].SetActive(false);


            SpeedTimeResult.text = "<color=#DDB11E>������� ��������:    " + SpeedTimeMaxResult + "c</color>";
            DistanceResult.text = "<color=#DDB11E>����� ����������:    " + PlayerPrefs.GetFloat("distance") + " KM</color>";
            ObgonResult.text = "<color=#DDB11E>������� ������:    " + ObgonNumbersResult.ToString() + "</color>";
            StartCoroutine(StopCar());
            CamMoove = true;

            timerActivatedCamera = true;

            DollarsRange = (float)Math.Round((float)DollarsRange, 1);
            DollarsTime = (float)Math.Round((float)DollarsTime, 1);

            DollarsRaange.text = DollarsRange.ToString();
            DollarsObgoon.text = DollarsObgon.ToString();
            DollarsTiime.text = DollarsTime.ToString();



            DollarsResult = DollarsRange + DollarsObgon + DollarsTime;            
            DollarsResult = (float)Math.Round((float)DollarsResult, 1);
            DollarsRessult.text = DollarsResult.ToString();

            //���������� ������������ ����� � �������
            if (_babki)
            {
                _dollarsPlus += DollarsResult;
                PlayerPrefs.SetFloat("Dollars", _dollarsPlus);
                _dollarsPlus = PlayerPrefs.GetFloat("Dollars");
                _babki = false;
            }
            Debug.Log(PlayerPrefs.GetFloat("Dollars"));
        }
    }
}

