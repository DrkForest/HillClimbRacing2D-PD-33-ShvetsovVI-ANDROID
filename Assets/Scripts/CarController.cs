using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour
{


    [SerializeField] private Rigidbody2D _carBody;
    [Header("Motors")]
    [SerializeField] private WheelJoint2D backWheel;
    [SerializeField] private WheelJoint2D frontWheel;
    [SerializeField] private JointMotor2D backWheelMotor;
    [SerializeField] private JointMotor2D frontWheelMotor;
    [SerializeField] private float motorSpeed;
    [SerializeField] private float maxMotorTorque;

    [SerializeField] private GameObject Wheel1;
    [SerializeField] private GameObject Wheel2;

    [SerializeField] private float suspension;
    [SerializeField] private float wheels;

    //
    [SerializeField] private bool isGrounded ;
    [SerializeField] private Collider2D groundCollaider;


    [Header("Items UI")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] Slider fuelSlider;
    [SerializeField] private float maxFuel;
    [SerializeField] private float fuelUsagePerS;
    private float currentFuel;

    [SerializeField] private TextMeshProUGUI metersText;
    [SerializeField] private TextMeshProUGUI recordText;
    //meters
    private float maxMeters;
    private float currentMeters;
    private float oldMeters;
    //record
    private float maxRecord = 0;
    private float currentRecord = 0;
    private float oldRecord = 0;
    //coins
    private int coins = 0;
    private int lvlCoins = 0;



    //gameovermenu
    [SerializeField] protected GameObject GameOverMenu;
    [Header("Control UI")]
    [SerializeField] private ClickController[] ContolButtons;

    [SerializeField] private Image ContolImageGas;
    [SerializeField] private Image ContolImageBrake;
    [SerializeField] private Sprite[] SpritesGas;
    [SerializeField] private Sprite[] SpritesBrake;

    [Header("GameOver UI")]
    [SerializeField] private TextMeshProUGUI coinsGameOver;
    [SerializeField] private TextMeshProUGUI metersGameOver;
    [SerializeField] private TextMeshProUGUI recordGameOver;
    
    //private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        oldMeters = 0;
        
        //motor
        backWheelMotor.maxMotorTorque = maxMotorTorque;
        frontWheelMotor.maxMotorTorque = maxMotorTorque;

        //fuel
        currentFuel = maxFuel;
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;

        //coins
        lvlCoins = 0;

        LoadCoins();
        LoadRecord();
        RecordTextChanger();

        suspension = 3;
        wheels = 0.20f;

        LoadCharacteristics();

        

        

        Wheel1.GetComponent<Rigidbody2D>().sharedMaterial.friction = wheels;
        Wheel2.GetComponent<Rigidbody2D>().sharedMaterial.friction = wheels;

        var JointSuspension1 = backWheel.suspension;
        var JointSuspension2 = frontWheel.suspension;
        JointSuspension1.frequency = suspension;
        JointSuspension2.frequency = suspension;
        backWheel.suspension = JointSuspension1;
        frontWheel.suspension = JointSuspension2;


        Debug.Log(Wheel1.GetComponent<Rigidbody2D>().sharedMaterial.friction);
        Debug.Log(Wheel2.GetComponent<Rigidbody2D>().sharedMaterial.friction);
        Debug.Log(backWheel.suspension);
        Debug.Log(frontWheel.suspension);
    }
    private void OnDestroy()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //air move
        //if (frontWheel.GetComponent<Collider2D>().IsTouchingLayers() || backWheel.GetComponent<Collider2D>().IsTouchingLayers())
        if (frontWheel.GetComponent<Collider2D>().IsTouching(groundCollaider) || backWheel.GetComponent<Collider2D>().IsTouching(groundCollaider))
        {
            isGrounded = true;
            Debug.Log(frontWheel.GetComponent<Collider2D>().IsTouchingLayers());
        }
        else
        {
            isGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        CheckFuel();

        CoinTextChanger();
        //movement
        if (isGrounded)
        {
            CarMovement();
        }
        else
        {
            MoveAir();
        }

        LoadRecord();
        RecordTextChanger();
        currentMeters = (int)Math.Round(transform.position.x);
        currentRecord = currentMeters;

        if (currentMeters > oldMeters)
        {
            maxMeters = (int)Math.Round(transform.position.x);
            //Debug.Log(maxMeters);
            oldMeters = maxMeters;
            currentRecord = maxMeters;
            MetersTextChanger();
        }
        
        if (currentRecord > oldRecord)
        {

            maxRecord = currentRecord;
            SaveRecord();
            
        }
        
    }

    void MoveAir()
    {
        Debug.Log("123");
        backWheel.useMotor = false;
        frontWheel.useMotor = false;
        if(ContolButtons[0].IsClicked == true)
        {
            if(_carBody.angularVelocity < 200)
            {
                
            }
            _carBody.AddTorque(50f);

        }
        else if (ContolButtons[1].IsClicked == true)
        {
            if (_carBody.angularVelocity > -200)
            {
                
            }
            _carBody.AddTorque(-50f);
        }
    }

    #region Movement methods
    public void CarMovement()
    {
        if (ContolButtons[0].IsClicked == true)
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            backWheel.motor = backWheelMotor;
            frontWheel.motor = frontWheelMotor;

            backWheelMotor.motorSpeed = -motorSpeed;
            frontWheelMotor.motorSpeed = -motorSpeed;

            ContolImageGas.sprite = SpritesGas[1];
        }
        else if (ContolButtons[1].IsClicked == true)
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            backWheel.motor = backWheelMotor;
            frontWheel.motor = frontWheelMotor;

            backWheelMotor.motorSpeed = motorSpeed;
            frontWheelMotor.motorSpeed = motorSpeed;

            ContolImageBrake.sprite = SpritesBrake[1];
        }
        else
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;

            backWheelMotor.motorSpeed = 0;
            frontWheelMotor.motorSpeed = 0;
            backWheel.motor = backWheelMotor;
            frontWheel.motor = frontWheelMotor;
            ContolImageGas.sprite = SpritesGas[0];
            ContolImageBrake.sprite = SpritesBrake[0];
        }

    }
    #endregion

    #region Coins methods
    public void LoadCoins()
    {
        string key = "Coins";
        if (PlayerPrefs.HasKey(key))
        {
            this.coins = PlayerPrefs.GetInt(key);
        }

    }

    public void SaveCoins()
    {
        string key = "Coins";
        PlayerPrefs.SetInt(key, this.coins);
        PlayerPrefs.Save();
    }

    public void AddCoins(int coinValue)
    {
        LoadCoins();
        this.coins = this.coins + coinValue;
        this.lvlCoins = this.lvlCoins + coinValue;
        SaveCoins();
    }

    public void CoinTextChanger()
    {
        LoadCoins();
        coinsText.SetText(this.coins.ToString());
    }
    #endregion

    #region Fuel methods
    public void CheckFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel = currentFuel - fuelUsagePerS * Time.deltaTime;
            //Debug.Log(currentFuel);
            fuelSlider.value = currentFuel;
        }
        else
        {
            OnGround();
        }
    }

    public void AddFuel(int fuelValue)
    {
        currentFuel += fuelValue;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }

        fuelSlider.value = currentFuel;
    }
    #endregion

    #region Record methods
    public void LoadRecord()
    {
        string key = "Record";
        if (PlayerPrefs.HasKey(key))
        {
            this.oldRecord = PlayerPrefs.GetFloat(key);
        }

    }

    public void SaveRecord()
    {
        string key = "Record";
        PlayerPrefs.SetFloat(key, this.maxRecord);
        PlayerPrefs.Save();
    }

    public void MetersTextChanger()
    {

        metersText.SetText(this.maxMeters.ToString() + " M");
    }

    public void RecordTextChanger()
    {

        recordText.SetText(this.oldRecord.ToString() + " M");
    }
    #endregion

    void LoadCharacteristics()
    {
        string speed = "Speed";
        string suspension = "Suspension";
        string wheels = "Wheel";
        string gas = "Gas";
        if (PlayerPrefs.HasKey(speed))
        {
            this.motorSpeed = PlayerPrefs.GetInt(speed);
        }
        if (PlayerPrefs.HasKey(suspension))
        {
            this.suspension = PlayerPrefs.GetFloat(suspension);
        }
        if (PlayerPrefs.HasKey(wheels))
        {
            this.wheels = PlayerPrefs.GetFloat(wheels);
        }
        if (PlayerPrefs.HasKey(gas))
        {
            this.fuelUsagePerS = PlayerPrefs.GetFloat(gas);
        }
    }

    #region EndGame
    public void OnGround()
    {

        //InvokeRepeating("GameOver", 0, 5f);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameOverMenu.SetActive(!GameOverMenu.activeInHierarchy);

        coinsGameOver.SetText(this.lvlCoins.ToString());
        metersGameOver.SetText(this.maxMeters.ToString() + " M");
        recordGameOver.SetText(this.oldRecord.ToString() + " M");


        Time.timeScale = GameOverMenu.activeInHierarchy ? 0 : 1;
        StopCoroutine(GameOver());
    }


    
    #endregion
}
