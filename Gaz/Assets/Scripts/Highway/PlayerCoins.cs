using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(RCC_DashboardInputs))]
public class PlayerCoins : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform CameraRCC;
    public Text DistanceTxt; //расстояние которое проехали, выводим на экран
    public GameObject[] scoreAnim = new GameObject[9];
    public GameObject LoosePanel, SpeedPanel, Obgons;
    public GameObject[] InvizDashboardLoose = new GameObject[8];
    public Rigidbody RbPlayer;
    public Text SpeedTime, SpeedTimeResult, DistanceResult, ObgonResult;
    public Text DollarsObgoon, DollarsTiime, DollarsRaange, DollarsRessult, sssssss;

    private Vector3 CameraEnd;
    private bool CamMoove = false;

    private float Distance; //расстояние которое проехали
    private int i = 100, n = 1, ObgonNumbers = 0, ObgonNumbersResult = 0;
    private float j = 10f;
    private float maxScore;

    private float DistanceTransform = 20f;
    private int peremen = 0, prover;
    private float ScoreResult = 0f; //Очки которые влияют на то, сколько бабок ты получишь от обгона и пройденного пути
    private bool enab = true;

    private float DollarsRange = 0f; //Бабки за пройденный путь
    private float DollarsObgon = 0f; //Бабки за обгоны
    private float DollarsTime = 0f; //Бабки за время на высокой скорости
    private float DollarsResult; //Бабки для записи в PlayerPrefs

    //Переменные, которые отвечают за начисление очков после 100км
    private RCC_DashboardInputs inputs;
    private float speedMin;

    //Таймер для обгона
    private DateTime timeBegin;
    private TimeSpan timeTimer, timerZero = new TimeSpan (0,0,0);
    private bool sec = true, timerActivated = false, IsLoos;

    //таймер для остановки камеры
    private DateTime timeBeginCamera;
    private TimeSpan timeTimerCamera;
    private bool secCamera = true, timerActivatedCamera = false;

    //2 Таймера для подсчета высокой скорости, один для вывода в момент игры, а второй для подсчета общего времени и вывода очков
    private DateTime timeBeginSpeed, timeBeginSpeedGame;
    private TimeSpan timeTimerSpeed, timeTimerSpeedGame;
    private bool secSpeed = true, secSpeedGame = true, timerActivatedSpeed = false, timerActivatedSpeedGame = false;
    private double SpeedTimeMax, SpeedTimeMaxResult;
    public Text ann;


    private float _dollarsPlus; //Записываем сюда бабки после аварии и постоянно складываем
    private bool _babki = false; //дополнительная проверка, чтобы после аварии деньги начислялись только 1 раз
    private void Start()
    {
        inputs = GetComponent<RCC_DashboardInputs>(); //берем переменную из rcc для подсчета скорости
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

        speedMin = inputs.KMH; //Записываем текущую скорость в переменную, чтобы можно было работать

        //Таймер для обгона и добавлению очков
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
            i = 100 * n; // очки
            j = 15 * n; // бабки
            ObgonNumbers = 0;
            Obgons.SetActive(false);
        }

      /*  //Максимальное количество очков которое набрал игрок               !!!!!!!!!!!!переместил в столкновение
        if (PlayerPrefs.GetFloat("score") > PlayerPrefs.GetFloat("maxScore"))
        {
            maxScore = PlayerPrefs.GetFloat("score");
            PlayerPrefs.SetFloat("maxScore", maxScore);
        }       */

        //PlayerPrefs.SetFloat("dollars", Dollars);

        //РАСЧЕТ ПРОЙДЕННОГО РАССТОЯНИЯ И ДОБАВЛЕНИЯ ОЧКОВ 
        if (PlayerTransform.position.z >= DistanceTransform)
        {
            Distance += 0.01f;
            DollarsRange += 1.5f;
            Distance = (float)Math.Round((float)Distance, 4);
            PlayerPrefs.SetFloat("distance", Distance);
            
            DistanceTxt.text = PlayerPrefs.GetFloat("distance") + " KM";
            DistanceTransform = PlayerTransform.position.z + 40f;

            // Записываем очки и баксы за обгон
            PlayerPrefs.SetFloat("score", ScoreResult);
            ScoreResult += 10f; //Очки за 0,01 КМ пройденного пути
            //Dollars += 1.5f; //Баксы за 0.01 КМ пройденного пути            
        }

        //ТАЙМЕР ДЛЯ ПОДСЧЕТА ОБЩЕГО ВРЕМЕНИ НА ВЫСОКОЙ СКОРОСТИ (для вывода в окно проигрыша и подсчета очков/бабок)
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
            timeBeginSpeed = DateTime.Now - timeTimerSpeed; // Позволяет считывать все время когда у игрока была скорость больше 100км.ч для окна проигрыша
            //Debug.Log(timeBeginSpeed);
        }

        //ТАЙМЕР ДЛЯ ПОДСЧЕТА ВРЕМЕНИ НА ВЫОСКОЙ СКОРОСТИ ВО ВРЕМЯ ИГРЫ (показ пользователю во время игры)
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
                    SpeedTime.text = "  НА ВЫСОКОЙ СКОРОСТИ: <color=#27C000>" + SpeedTimeMax + "c</color>";
                    
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

    // Выход из обгона когда и начсляются очки
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bot" && enab && speedMin >= 100)
        {
            
            enab = false;
            peremen++;
            ScoreResult += i; //очки
            DollarsObgon += j; //бабки
           
            timerActivated = true;
            sec = true;
            timeTimer = timerZero;
            ObgonNumbersResult++;

            //Если время на таймере меньше 5, то очки при каждом обгоне уведичиваются на 100 до 900, а если будет больше 5 сек то скидываются до 100 обратно, это прописано в апдейте
            if (timeTimer.Seconds <= 5)
            {
                Obgons.SetActive(true);
                
                ObgonNumbers++;
                ann.text = "КОМБО:  <color=#27C000>" + ObgonNumbers.ToString() + "</color>";
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
                
                //Debug.Log("Очки: " + i);
                //Debug.Log("n: " + n);
                //Debug.Log(DollarsObgon);
            }            
        }
        
    }

     // Вход в обгон в котором проверяется одну ли тачку мы обгоняем
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

    //Куратина которая срабатывает через 0,5 сек после столкновения игрока
    IEnumerator StopCar()
    {
        yield return new WaitForSeconds(0.5f);
        if (!IsLoos)
        {
            LoosePanel.SetActive(true);
            
            IsLoos = true;
        }
        
    }

    //СОбыитие при аварии
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bot") || col.gameObject.CompareTag("Roads")) {

            //Максимальное количество очков которое набрал игрок
            if (PlayerPrefs.GetFloat("score") > PlayerPrefs.GetFloat("maxScore"))
            {
                maxScore = PlayerPrefs.GetFloat("score");
                PlayerPrefs.SetFloat("maxScore", maxScore);
            }

            RbPlayer.velocity = Vector3.zero; //останавливает игрока
            RbPlayer.isKinematic = true; //останавливает и не дает управлять игроком

            //Скрывает элементы UI
            InvizDashboardLoose[0].SetActive(false);
            InvizDashboardLoose[1].SetActive(false);
            InvizDashboardLoose[2].SetActive(false);
            InvizDashboardLoose[3].SetActive(false);
            InvizDashboardLoose[4].SetActive(false);
            InvizDashboardLoose[5].SetActive(false);
            InvizDashboardLoose[6].SetActive(false);
            InvizDashboardLoose[7].SetActive(false);


            SpeedTimeResult.text = "<color=#DDB11E>ВЫСОКАЯ СКОРОСТЬ:    " + SpeedTimeMaxResult + "c</color>";
            DistanceResult.text = "<color=#DDB11E>ОБЩЕЕ РАССТОЯНИЕ:    " + PlayerPrefs.GetFloat("distance") + " KM</color>";
            ObgonResult.text = "<color=#DDB11E>ОПАСНЫЕ ОБГОНЫ:    " + ObgonNumbersResult.ToString() + "</color>";
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

            //Начисление заработанных бабок в кошелек
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

