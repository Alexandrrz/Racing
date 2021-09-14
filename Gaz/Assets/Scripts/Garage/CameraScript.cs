using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CameraScript : MonoBehaviour
{
    public Transform Camera;
    public Transform[] Chunk_ = new Transform[4]; //точки полета камеры
    public GameObject[] Cars = new GameObject[4];
    public GameObject[] _paintBuy = new GameObject[7]; //картинки покупки красок
    public float smoothing = 5f;
    public float speed_move = -15f;
    public Button Right_btn, Left_btn;
    public GameObject HATCHBACK, AUDI, GELENTWAGEN; //префабы которые апгрейдим
    public Scrollbar speedIndicator, handleIndicator, brakeIndicator;
    public Text SpeedNumbers, HandleNumbers, BrakeNumbers, _dollars, _buyCar;
    public Material Hatchback_paint, Audi_paint, Gelentwagen_paint;
    public GameObject _completeLogo, _buyBtn, _selectCar; //картинка, которая выскакивает при покупке нитро, машины

    private bool Right_ = false, Left_ = false, Black_color = false, White_color = false, Blue_color = false, Red_color = false, Yellow_color = false, Green_color = false, Purple_color = false, Gray_color = false; // Цвета в которые красятся тачки
    private bool _nos1 = false, _nos2 = false, _nos3 = false, _nos4 = false; //Проверка на наличие нитро у каждой машины
    private bool _car2, _car3; //переменные после которых спадает картинка покупки авто и его можно выбирать
    private bool _black = false, _blue = false, _red = false, _yellow = false, _green = false, _purple = false, _gray = false; //Переменные для ПОКУПКИ цветов для хэтчбэка
    private bool _black1 = false, _blue1 = false, _red1 = false, _yellow1 = false, _green1 = false, _purple1 = false, _gray1 = false; //Переменные для ПОКУПКИ цветов для хэтчбэка

    private int i = 0, plusUpgrade_speed = 16, plusUpgrade_brake = 5, plusUpgrade_handle = 5; //16 это 5% от 320 (от максимально прокаченной скорости),, brake и handle максимальное значение 100, а 5 это 5% от 100,,,,  а i это текущая машина, нумерация идет с 0
    private int x_HATCHBACK = 35, x_AUDI = 40, x_GELENTWAGEN = 55; //значения скорости у машин, которые мы запоминаем и улучшаем
    private int y_HATCHBACK = 10, y_AUDI = 20, y_GELENTWAGEN = 25; //значения тормозов у машин, которые мы запоминаем и улучшаем
    private int z_HATCHBACK = 40, z_AUDI = 45, z_GELENTWAGEN = 55; //значения рулежки у машин, которые мы запоминаем и улучшаем
    private int j = 0; //переменная, которая при нажатии на кнопку выбора авто запоминает его и меняет
    private float _dollarsBuy;
    private void Start()
    {
        
        SpeedNumbers.text = PlayerPrefs.GetInt("Speed_hatchback").ToString();
        BrakeNumbers.text = PlayerPrefs.GetInt("Brake_hatchback").ToString();
        HandleNumbers.text = PlayerPrefs.GetInt("Handle_hatchback").ToString();
        Cars[0].SetActive(true);
        Cars[1].SetActive(false);
        Cars[2].SetActive(false);
        Cars[3].SetActive(false);

        _dollarsBuy = PlayerPrefs.GetFloat("Dollars");

        j = PlayerPrefs.GetInt("Player_car"); //Машина на которой ездит игрок

        _dollars.text = PlayerPrefs.GetFloat("Dollars").ToString(); //Бабки в кошельке

        _car2 = (PlayerPrefs.GetInt("CAR2") != 0); //запоминает покупку машины
        _car3 = (PlayerPrefs.GetInt("CAR3") != 0); //запоминает покупку машины

        //Запоминание какой цвет куплен
        _black = (PlayerPrefs.GetInt("Black") != 0);
        _black1 = (PlayerPrefs.GetInt("Black1") != 0);

        _blue = (PlayerPrefs.GetInt("Blue") != 0);
        _blue1 = (PlayerPrefs.GetInt("Blue1") != 0);

        _red = (PlayerPrefs.GetInt("Red") != 0);
        _red1 = (PlayerPrefs.GetInt("Red1") != 0);

        _yellow = (PlayerPrefs.GetInt("Yellow") != 0);
        _yellow1 = (PlayerPrefs.GetInt("Yellow1") != 0);

        _green = (PlayerPrefs.GetInt("Green") != 0);
        _green1 = (PlayerPrefs.GetInt("Green1") != 0);

        _purple = (PlayerPrefs.GetInt("Purple") != 0);
        _purple1 = (PlayerPrefs.GetInt("Purple1") != 0);

        _gray = (PlayerPrefs.GetInt("Gray") != 0);
        _gray1 = (PlayerPrefs.GetInt("Gray1") != 0);

        _nos1 = (PlayerPrefs.GetInt("NOS1") != 0);
        //_nos2 = (PlayerPrefs.GetInt("NOS2") != 0); Показывает комплит после покупки нитро для второй тачки, при добавлении машин просто удалить коммент и спустится в метод для кнопки
    }
    private void Update()
    {
        Camera.transform.Rotate(0, speed_move * Time.deltaTime, 0);
        _dollars.text = PlayerPrefs.GetFloat("Dollars").ToString();
        PlayerPrefs.SetInt("Player_car", j);

        //ПЕРЕМЕЩЕНИЕ КАМЕРЫ ВПРАВО
        if (Right_)
        {
            Camera.position = Vector3.MoveTowards(Camera.position, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), smoothing * Time.deltaTime);
           
            if (Camera.position.x >= Chunk_[i + 1].position.x)
            {
                Right_ = false;                
                i++;
                Cars[i].SetActive(true);
                Cars[i-1].SetActive(false);
            }            
        }

        //ПЕРЕМЕЩЕНИЕ КАМЕРЫ ВЛЕВО
        if (Left_)
        {
            Camera.position = Vector3.MoveTowards(Camera.position, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), smoothing * Time.deltaTime);

            if (Camera.position.x <= Chunk_[i - 1].position.x)
            {
                Left_ = false;
                i--;
                Cars[i].SetActive(true);
                Cars[i+1].SetActive(false);
            }
        }

        //ПРАВАЯ КНОПКА ОТКЛЮЧАЕТСЯ ЕСЛИ ЗНАЧЕНИЕ БУДЕТ БОЛЬШЕ ЧЕМ МАШИН (ОТСЧЕТ С НУЛЯ)
        if (i < 2)
            Right_btn.interactable = true;
        if (i == 2)
            Right_btn.interactable = false;

        //ЛЕВАЯ КНОПКА
        if (i > 0)
            Left_btn.interactable = true;


        if (i == 0)
        {
            _selectCar.SetActive(true);
            _buyBtn.SetActive(false);
            Left_btn.interactable = false;
            
            //полоса скорости для хэтчбека

            SpeedNumbers.text = PlayerPrefs.GetInt("Speed_hatchback").ToString();
            speedIndicator.size = PlayerPrefs.GetInt("Speed_hatchback") / 320f; //индикатор скорости бмв (зеленая полоска)

            //полоса тормоза
            BrakeNumbers.text = PlayerPrefs.GetInt("Brake_hatchback").ToString();
            brakeIndicator.size = PlayerPrefs.GetInt("Brake_hatchback") / 100f;

            //полоса управления
            HandleNumbers.text = PlayerPrefs.GetInt("Handle_hatchback").ToString();
            handleIndicator.size = PlayerPrefs.GetInt("Handle_hatchback") / 100f;


            //изменение цвета машин
            if (White_color)
            {
                Hatchback_paint.color = Color.white;
                White_color = false;
            }

            if (Black_color)
            {
                
                Hatchback_paint.color = Color.black;
                Black_color = false;
            }
            if (_black)
                _paintBuy[0].SetActive(false);
            if (!_black)
                _paintBuy[0].SetActive(true);


            if (Blue_color)
            {
                Hatchback_paint.color = Color.blue;
                Blue_color = false;
            }
            if (_blue)
                _paintBuy[1].SetActive(false);
            if (!_blue)
                _paintBuy[1].SetActive(true);


            if (Red_color)
            {
                Hatchback_paint.color = Color.red;
                Red_color = false;
            }
            if (_red)
                _paintBuy[2].SetActive(false);
            if (!_red)
                _paintBuy[2].SetActive(true);


            if (Yellow_color)
            {
                Hatchback_paint.color = Color.yellow;
                Yellow_color = false;
            }
            if (_yellow)
                _paintBuy[3].SetActive(false);
            if (!_yellow)
                _paintBuy[3].SetActive(true);


            if (Green_color)
            {
                Hatchback_paint.color = Color.green;
                Green_color = false;
            }
            if (_green)
                _paintBuy[4].SetActive(false);
            if (!_green)
                _paintBuy[4].SetActive(true);


            if (Purple_color)
            {
                Hatchback_paint.color = Color.magenta;
                Purple_color = false;
            }
            if (_purple)
                _paintBuy[5].SetActive(false);
            if (!_purple)
                _paintBuy[5].SetActive(true);


            if (Gray_color)
            {
                Hatchback_paint.color = Color.gray;
                Gray_color = false;
            }
            if (_gray)
                _paintBuy[6].SetActive(false);
            if (!_gray)
                _paintBuy[6].SetActive(true);



            if (_nos1)
            {
                _completeLogo.SetActive(true);
                HATCHBACK.GetComponent<RCC_CarControllerV3>().useNOS = true;
            } else
            {
                _completeLogo.SetActive(false);
            }

        }
        if (i == 1)
        {
            if (_car2)
            {
                _selectCar.SetActive(true);
                _buyBtn.SetActive(false);
            }
            if (!_car2)
            {
                _buyCar.text = "25 000";
                _selectCar.SetActive(false);
                _buyBtn.SetActive(true);
            }
            //PlayerPrefs.SetInt("Speed_AUDI", x_AUDI);
            SpeedNumbers.text = PlayerPrefs.GetInt("Speed_AUDI").ToString();
            speedIndicator.size = PlayerPrefs.GetInt("Speed_AUDI") / 320f; //индикатор скорости ауди (зеленая полоска)

            if (_nos2)
            {
                _completeLogo.SetActive(true);
                HATCHBACK.GetComponent<RCC_CarControllerV3>().useNOS = true;
            }
            else
            {
                _completeLogo.SetActive(false);
            }

            //изменение цвета машин
            if (White_color)
            {
                Audi_paint.color = Color.white;
                White_color = false;
            }

            if (Black_color)
            {

                Audi_paint.color = Color.black;
                Black_color = false;
            }
            if (_black1)
                _paintBuy[0].SetActive(false);
            if (!_black1)
                _paintBuy[0].SetActive(true);


            if (Blue_color)
            {
                Audi_paint.color = Color.blue;
                Blue_color = false;
            }
            if (_blue1)
                _paintBuy[1].SetActive(false);
            if (!_blue1)
                _paintBuy[1].SetActive(true);

            if (Red_color)
            {
                Audi_paint.color = Color.red;
                Red_color = false;
            }
            if (_red1)
                _paintBuy[2].SetActive(false);
            if (!_red1)
                _paintBuy[2].SetActive(true);

            if (Yellow_color)
            {
                Audi_paint.color = Color.yellow;
                Yellow_color = false;
            }
            if (_yellow1)
                _paintBuy[3].SetActive(false);
            if (!_yellow1)
                _paintBuy[3].SetActive(true);

            if (Green_color)
            {
                Audi_paint.color = Color.green;
                Green_color = false;
            }
            if (_green1)
                _paintBuy[4].SetActive(false);
            if (!_green1)
                _paintBuy[4].SetActive(true);

            if (Purple_color)
            {
                Audi_paint.color = Color.magenta;
                Purple_color = false;
            }
            if (_purple1)
                _paintBuy[5].SetActive(false);
            if (!_purple1)
                _paintBuy[5].SetActive(true);

            if (Gray_color)
            {
                Audi_paint.color = Color.gray;
                Gray_color = false;
            }
            if (_gray1)
                _paintBuy[6].SetActive(false);
            if (!_gray1)
                _paintBuy[6].SetActive(true);
        }


        if (i == 2)
        {
            if (!_car3)
            {
                _buyCar.text = "250 000";
                _selectCar.SetActive(false);
                _buyBtn.SetActive(true);
            }
            if (_car3)
            {
                _selectCar.SetActive(true);
                _buyBtn.SetActive(false);
            }


            if (White_color)
            {
                Gelentwagen_paint.color = Color.white;
                White_color = false;
            }

            if (Black_color)
            {

                Gelentwagen_paint.color = Color.black;
                Black_color = false;
            }
            if (_black1)
                _paintBuy[0].SetActive(false);
            if (!_black1)
                _paintBuy[0].SetActive(true);


            if (Blue_color)
            {
                Gelentwagen_paint.color = Color.blue;
                Blue_color = false;
            }
            if (_blue1)
                _paintBuy[1].SetActive(false);
            if (!_blue1)
                _paintBuy[1].SetActive(true);

            if (Red_color)
            {
                Gelentwagen_paint.color = Color.red;
                Red_color = false;
            }
            if (_red1)
                _paintBuy[2].SetActive(false);
            if (!_red1)
                _paintBuy[2].SetActive(true);

            if (Yellow_color)
            {
                Gelentwagen_paint.color = Color.yellow;
                Yellow_color = false;
            }
            if (_yellow1)
                _paintBuy[3].SetActive(false);
            if (!_yellow1)
                _paintBuy[3].SetActive(true);

            if (Green_color)
            {
                Gelentwagen_paint.color = Color.green;
                Green_color = false;
            }
            if (_green1)
                _paintBuy[4].SetActive(false);
            if (!_green1)
                _paintBuy[4].SetActive(true);

            if (Purple_color)
            {
                Gelentwagen_paint.color = Color.magenta;
                Purple_color = false;
            }
            if (_purple1)
                _paintBuy[5].SetActive(false);
            if (!_purple1)
                _paintBuy[5].SetActive(true);

            if (Gray_color)
            {
                Gelentwagen_paint.color = Color.gray;
                Gray_color = false;
            }
            if (_gray1)
                _paintBuy[6].SetActive(false);
            if (!_gray1)
                _paintBuy[6].SetActive(true);
        }
        Debug.Log(j);
    }
    public void road()
    {
        SceneManager.LoadScene("Highway");
    }

    public void money()
    {
        _dollarsBuy += 50000;
        PlayerPrefs.SetFloat("Dollars", _dollarsBuy);
    }



    //Апгрейд характеристик
    public void BrakeUp()
    {
        if (i == 0 && PlayerPrefs.GetInt("Brake_hatchback") < 60 && PlayerPrefs.GetFloat("Dollars") >= 500)
        {
            HATCHBACK.GetComponent<RCC_CarControllerV3>().brakeTorque += 10f;

            _dollarsBuy -= 500;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            y_HATCHBACK += plusUpgrade_brake;
            PlayerPrefs.SetInt("Brake_hatchback", y_HATCHBACK);
            y_HATCHBACK = PlayerPrefs.GetInt("Brake_hatchback");
            BrakeNumbers.text = PlayerPrefs.GetInt("Brake_hatchback").ToString();
        }
    }

    public void SpeedUp()
    {
        if (i == 0 && PlayerPrefs.GetInt("Speed_hatchback") < 60 && PlayerPrefs.GetFloat("Dollars") >= 500)
        {
            HATCHBACK.GetComponent<RCC_CarControllerV3>().maxEngineTorque += 5f;
            HATCHBACK.GetComponent<RCC_CarControllerV3>().maxEngineTorqueAtRPM += 10f;
            HATCHBACK.GetComponent<RCC_CarControllerV3>().maxspeed += 5f;

            _dollarsBuy -= 500;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            x_HATCHBACK += plusUpgrade_speed;
            PlayerPrefs.SetInt("Speed_hatchback", x_HATCHBACK);
            x_HATCHBACK = PlayerPrefs.GetInt("Speed_hatchback");
            SpeedNumbers.text = PlayerPrefs.GetInt("Speed_hatchback").ToString();
        }
        /*
        if (i == 1 && x_AUDI < 80 )
        {

            //AUDI.GetComponent<RCC_CarControllerV3>().maxEngineTorque += 5f;
            //AUDI.GetComponent<RCC_CarControllerV3>().maxEngineTorqueAtRPM += 10f;
            //AUDI.GetComponent<RCC_CarControllerV3>().maxspeed += 5f;

            _dollarsBuy -= 500;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            x_AUDI += plusUpgrade_speed;
            PlayerPrefs.SetInt("Speed_AUDI", x_AUDI);
            SpeedNumbers.text = PlayerPrefs.GetInt("Speed_AUDI").ToString();
        } */
    }

    public void HandleUp()
    {
        if (i == 0 && PlayerPrefs.GetInt("Handle_hatchback") < 60 && PlayerPrefs.GetInt("Dollars") >= 500)
        {
            HATCHBACK.GetComponent<RCC_CarControllerV3>().highspeedsteerAngle += 0.5f;
            HATCHBACK.GetComponent<RCC_CarControllerV3>().highspeedsteerAngleAtspeed += 0.5f;

            _dollarsBuy -= 500;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            z_HATCHBACK += plusUpgrade_handle;
            PlayerPrefs.SetInt("Handle_hatchback", z_HATCHBACK);
            z_HATCHBACK = PlayerPrefs.GetInt("Handle_hatchback");
            HandleNumbers.text = PlayerPrefs.GetInt("Handle_hatchback").ToString();
        }
    }



    //Изменение цвета
    public void White_paint()
    {
        White_color = true;
    }
    public void Black_paint()
    {
        if (i == 0 && !Black_color && !_black && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Black_color = true;
            
            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _black = true;
            PlayerPrefs.SetInt("Black", (_black ? 1 : 0));
        }
        if(_black && i == 0 && !Black_color)
            Black_color = true;

        
        //Цвет для второй машины также делаем и с остальными цветами
        if (i == 1 && !Black_color && !_black1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Black_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _black1 = true;
            PlayerPrefs.SetInt("Black1", (_black1 ? 1 : 0));
        }
        if (_black1 && i == 0 && !Black_color)
            Black_color = true;


    }
    public void Blue_paint()
    {
        if (i == 0 && !Blue_color && !_blue && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Blue_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _blue = true;
            PlayerPrefs.SetInt("Blue", (_blue ? 1 : 0));
        }
        if (_blue && i == 0 && !Blue_color)
            Blue_color = true;



        if (i == 1 && !Blue_color && !_blue1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Blue_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _blue1 = true;
            PlayerPrefs.SetInt("Blue1", (_blue1 ? 1 : 0));
        }
        if (_blue1 && i == 1 && !Blue_color)
            Blue_color = true;
    }
    public void Red_paint()
    {
        if (i == 0 && !Red_color && !_red && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Red_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _red = true;
            PlayerPrefs.SetInt("Red", (_red ? 1 : 0));
        }
        if (_red && i == 0 && !Red_color)
            Red_color = true;


        if (i == 1 && !Red_color && !_red1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Red_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _red1 = true;
            PlayerPrefs.SetInt("Red1", (_red1 ? 1 : 0));
        }
        if (_red1 && i == 1 && !Red_color)
            Red_color = true;
    }
    public void Yellow_paint()
    {
        if (i == 0 && !Yellow_color && !_yellow && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Yellow_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _yellow = true;
            PlayerPrefs.SetInt("Yellow", (_yellow ? 1 : 0));
        }
        if (_yellow && i == 0 && !Yellow_color)
            Yellow_color = true;


        if (i == 1 && !Yellow_color && !_yellow1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Yellow_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _yellow1 = true;
            PlayerPrefs.SetInt("Yellow1", (_yellow1 ? 1 : 0));
        }
        if (_yellow1 && i == 1 && !Yellow_color)
            Yellow_color = true;
    }
    public void Green_paint()
    {
        if (i == 0 && !Green_color && !_green && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Green_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _green = true;
            PlayerPrefs.SetInt("Green", (_green ? 1 : 0));
        }
        if (_green && i == 0 && !Green_color)
            Green_color = true;


        if (i == 1 && !Green_color && !_green1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Green_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _green1 = true;
            PlayerPrefs.SetInt("Green1", (_green1 ? 1 : 0));
        }
        if (_green1 && i == 1 && !Green_color)
            Green_color = true;

    }
    public void Purple_paint()
    {
        if (i == 0 && !Purple_color && !_purple && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Purple_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _purple = true;
            PlayerPrefs.SetInt("Purple", (_purple ? 1 : 0));
        }
        if (_purple && i == 0 && !Purple_color)
            Purple_color = true;


        if (i == 1 && !Purple_color && !_purple1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Purple_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _purple1 = true;
            PlayerPrefs.SetInt("Purple1", (_purple1 ? 1 : 0));
        }
        if (_purple1 && i == 1 && !Purple_color)
            Purple_color = true;
    }
    public void Gray_paint()
    {
        if (i == 0 && !Gray_color && !_gray && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Gray_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _gray = true;
            PlayerPrefs.SetInt("Gray", (_gray ? 1 : 0));
        }
        if (_gray && i == 0 && !Gray_color)
            Gray_color = true;


        if (i == 1 && !Gray_color && !_gray1 && PlayerPrefs.GetFloat("Dollars") >= 5000)
        {
            Gray_color = true;

            _dollarsBuy -= 5000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);

            _gray1 = true;
            PlayerPrefs.SetInt("Gray1", (_gray1 ? 1 : 0));
        }
        if (_gray1 && i == 1 && !Gray_color)
            Gray_color = true;
    }



    //Покупка нитро
    public void Nos_buy()
    {
        if (i == 0 && !_nos1 && PlayerPrefs.GetFloat("Dollars") >= 15000)
        {
            _nos1 = true;
            PlayerPrefs.SetInt("NOS1", (_nos1 ? 1 : 0));

            _dollarsBuy -= 15000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);
        }


        //Нитро для последующих тачек
       /* if (i == 1 && !_nos2 && PlayerPrefs.GetFloat("Dollars") >= 15000)
        {
            _nos2 = true;
            PlayerPrefs.SetInt("NOS2", (_nos2 ? 1 : 0));
        }
       */
    }


    //Выбор и покупка машин
    public void Select_car()
    {
        if (i == 0)
        {
            j = 0;
        }

        // Покупка тачки
        if ( i == 1 && PlayerPrefs.GetFloat("Dollars") >= 25000 && !_car2)
        {
            _dollarsBuy -= 25000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);
            _car2 = true;
            PlayerPrefs.SetInt("CAR2", (_car2 ? 1 : 0));
        }
        // Ее выбор
        if (i == 1 &&_car2)
            j = 1;



        if (i == 2 && PlayerPrefs.GetFloat("Dollars") >= 250000 && !_car3)
        {
            _dollarsBuy -= 250000;
            _dollarsBuy = (float)Math.Round((float)_dollarsBuy, 1);
            PlayerPrefs.SetFloat("Dollars", _dollarsBuy);
            _car3 = true;
            PlayerPrefs.SetInt("CAR3", (_car3 ? 1 : 0));
        }
        // Ее выбор
        if (i == 2 && _car3)
            j = 2;
    }


    //переключение машин
    public void Right_button()
    {
        Right_ = true;
    }
    public void Left_button ()
    {
        Left_ = true;
    }

    public void Back_menu()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
