using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GarageScript : MonoBehaviour
{
    [Header("GameOver UI")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [Header("GameOver UI")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI suspensionText;
    [SerializeField] private TextMeshProUGUI wheelsText;
    [SerializeField] private TextMeshProUGUI gasText;
    [Header("BuyUpdate UI")]
    [SerializeField] private Button speedBuyButtons;
    [SerializeField] private Button suspensionBuyButtons;
    [SerializeField] private Button wheelsBuyButtons;
    [SerializeField] private Button gasBuyButtons;
    [Header("Menus UI")]
    [SerializeField] protected GameObject NoMoneyMenu;
    [SerializeField] private Button NoMoneyMenuOkButton;

    [SerializeField] protected Button StageButton;
    [SerializeField] protected Button ExitButton;
    [Header("Reset UI")]
    [SerializeField] protected Button ResetButton;
    [SerializeField] protected Image SpeedBuy;

    private int coins;

    private int speed;
    private float suspension;
    private float wheels;
    private float gas;

    private int speedLvl;
    private int suspensionLvl;
    private int wheelsLvl;
    private int gasLvl;

    private int speedCost;
    private int suspensionCost;
    private int wheelsCost;
    private int gasCost;

    private int speedLvlMax = 5;
    private int suspensionLvlMax = 4;
    private int wheelsLvlMax = 3;
    private int gasLvlMax = 6;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //first tima play
        coins = 0;
        speed = 1800;
        suspension = 3;
        wheels = 0.20f;
        gas = 5;


        CoinTextChanger();
        CheckCharasteristics();
        
        NoMoneyMenuOkButton.onClick.AddListener(NoMoneyMenuActive);
        ResetButton.onClick.AddListener(DeleteCh);

        speedBuyButtons.onClick.AddListener(BuySpeed);
        suspensionBuyButtons.onClick.AddListener(BuySuspension);
        wheelsBuyButtons.onClick.AddListener(BuyWheels);
        gasBuyButtons.onClick.AddListener(BuyGas);

        StageButton.onClick.AddListener(GoStage);
        ExitButton.onClick.AddListener(Exit);
    }
    
    private void OnDestroy()
    {
        NoMoneyMenuOkButton.onClick.RemoveListener(NoMoneyMenuActive);
        ResetButton.onClick.RemoveListener(DeleteCh);

        speedBuyButtons.onClick.RemoveListener(BuySpeed);
        suspensionBuyButtons.onClick.RemoveListener(BuySuspension);
        wheelsBuyButtons.onClick.RemoveListener(BuyWheels);
        gasBuyButtons.onClick.RemoveListener(BuyGas);

        StageButton.onClick.RemoveListener(GoStage);
        ExitButton.onClick.RemoveListener(Exit);
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        CheckCharasteristics();
    }

    #region coins methods
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
        
        SaveCoins();
    }

    public void CoinTextChanger()
    {
        LoadCoins();
        coinsText.SetText(this.coins.ToString());
    }
    #endregion

    #region characteristics methods
    void SaveCharacteristics()
    {
        string speed = "Speed";
        string suspension = "Suspension";
        string wheels = "Wheel";
        string gas = "Gas";
        PlayerPrefs.SetInt(speed, this.speed);
        PlayerPrefs.SetFloat(suspension, this.suspension);
        PlayerPrefs.SetFloat(wheels, this.wheels);
        PlayerPrefs.SetFloat(gas, this.gas);
        PlayerPrefs.Save();
        LoadCharacteristics();

    }
    void LoadCharacteristics()
    {
        string speed = "Speed";
        string suspension = "Suspension";
        string wheels = "Wheel";
        string gas = "Gas";
        if (PlayerPrefs.HasKey(speed))
        {
            this.speed = PlayerPrefs.GetInt(speed);
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
            this.gas = PlayerPrefs.GetFloat(gas);
        }
    }

    void CheckCharasteristics()
    {
        LoadCharacteristics();

        switch (speed)
        {
            case 1900:
                speedLvl = 1;
                speedCost = 8000;
                break;
            case 2000:
                speedLvl = 2;
                speedCost = 12000;
                break;
            case 2100:
                speedLvl = 3;
                speedCost = 16000;
                break;
            case 2200:
                speedLvl = 4;
                speedCost = 20000;
                break;
            case 2300:
                speedLvl = 5;
                speedCost = 0;
                break;

            default:
                speedLvl = 0;
                speedCost = 4000;
                break;
        }
        if (speedLvl == speedLvlMax)
        {
            speedText.SetText("Level " + speedLvl + " Max");
        }
        else
        {
            speedText.SetText("Level " + speedLvl + "/5 Cost:" + speedCost);
        }
        

        switch (suspension)
        {
            case 3.25f:
                suspensionLvl = 1;
                suspensionCost = 6000;
                break;
            case 3.5f:
                suspensionLvl = 2;
                suspensionCost = 9000;
                break;
            case 3.75f:
                suspensionLvl = 3;
                suspensionCost = 12000;
                break;
            case 4f:
                suspensionLvl = 4;
                suspensionCost = 0;
                break;

            default:
                suspensionLvl = 0;
                suspensionCost = 3000;
                break;
        }
        if (suspensionLvl == suspensionLvlMax)
        {
            suspensionText.SetText("Level " + suspensionLvl + " Max");
        }
        else
        {
            suspensionText.SetText("Level " + suspensionLvl + "/4 Cost:" + suspensionCost);
        }

        switch (wheels)
        {
            case 0.3f:
                wheelsLvl = 1;
                wheelsCost = 8000;
                break;
            case 0.4f:
                wheelsLvl = 2;
                wheelsCost = 16000;
                break;
            case 0.5f:
                wheelsLvl = 3;
                wheelsCost = 0;
                break;
            /*case 0.4f:
                wheelsLvl = 4;
                wheelsCost = 16000;
                break;
            case 0.45f:
                wheelsLvl = 5;
                wheelsCost = 20000;
                break;
            case 0.5f:
                wheelsLvl = 6;
                wheelsCost = 0;
                break;*/

            default:
                wheelsLvl = 0;
                wheelsCost = 2000;
                break;
        }
        if (wheelsLvl == wheelsLvlMax)
        {
            wheelsText.SetText("Level " + wheelsLvl + " Max");
        }
        else
        {
            wheelsText.SetText("Level " + wheelsLvl + "/3 Cost:" + wheelsCost);
        }

        switch (gas)
        {

            case 4.5f:
                gasLvl = 1;
                gasCost = 6000;
                break;
            case 4f:
                gasLvl = 2;
                gasCost = 9000;
                break;
            case 3.5f:
                gasLvl = 3;
                gasCost = 12000;
                break;
            case 3f:
                gasLvl = 4;
                gasCost = 15000;
                break;
            case 2.5f:
                gasLvl = 5;
                gasCost = 18000;
                break;
            case 2f:
                gasLvl = 6;
                gasCost = 0;
                break;
            

            default:
                gasLvl = 0;
                gasCost = 3000;
                break;
        }
        if (gasLvl == gasLvlMax)
        {
            gasText.SetText("Level " + gasLvl + " Max");
        }
        else
        {
            gasText.SetText("Level " + gasLvl + "/6 Cost:" + gasCost);
        }

        /*speed;
        suspension;
        wheels;
        gas;*/
        
    }
    #endregion

    /*    void BuyClick()
        {
            //string characteristics;
            if (BuyButtons[0].IsClicked == true)
            {
                BuySwitch("speed");
                Debug.Log(coins);
                Debug.Log(speed);
                Debug.Log(suspension);
                Debug.Log(wheels);
                Debug.Log(gas);
            }
            else if (BuyButtons[1].IsClicked == true)
            {
                BuySwitch("suspension");
                Debug.Log(coins);
                Debug.Log(speed);
                Debug.Log(suspension);
                Debug.Log(wheels);
                Debug.Log(gas);
            }
            else if (BuyButtons[2].IsClicked == true)
            {
                BuySwitch("wheels");
                Debug.Log(coins);
                Debug.Log(speed);
                Debug.Log(suspension);
                Debug.Log(wheels);
                Debug.Log(gas);
            }
            else if (BuyButtons[3].IsClicked == true)
            {
                BuySwitch("gas");
                Debug.Log(coins);
                Debug.Log(speed);
                Debug.Log(suspension);
                Debug.Log(wheels);
                Debug.Log(gas);
            }

            CheckCharasteristics();


        }*/

    #region BuyButtons
    void BuySpeed()
    {
        BuySwitch("speed");
    }

    void BuySuspension()
    {
        BuySwitch("suspension");
    }
    void BuyWheels()
    {
        BuySwitch("wheels");
    }
    void BuyGas()
    {
        BuySwitch("gas");
    }
    #endregion


    void BuySwitch(string characteristics)
    {
        LoadCharacteristics();
        
        switch (characteristics)
        {

            case "speed":
                if (coins >= speedCost && speedLvl < speedLvlMax)
                {
                    coins -= speedCost;
                    speed += 100;
                    SaveCoins();
                    CoinTextChanger();
                    SaveCharacteristics();
                    CheckCharasteristics();
                }
                else if(speedLvl < speedLvlMax)
                {
                    NoMoneyMenuActive();
                }
                break;
            case "suspension":
                if (coins >= suspensionCost && suspensionLvl < suspensionLvlMax)
                {
                    coins -= suspensionCost;
                    suspension += 0.25f;
                    SaveCoins();
                    CoinTextChanger();
                    SaveCharacteristics();
                    CheckCharasteristics();
                }
                else if(suspensionLvl < suspensionLvlMax)
                {
                    NoMoneyMenuActive();
                }
                break;
            case "wheels":
                if (coins >= wheelsCost && wheelsLvl < wheelsLvlMax) 
                {
                    coins -= wheelsCost;
                    wheels = wheels + 0.1f;
                    SaveCoins();
                    CoinTextChanger();
                    SaveCharacteristics();
                    CheckCharasteristics();
                }
                else if(wheelsLvl < wheelsLvlMax)
                {
                    NoMoneyMenuActive();
                }
                break;
            case "gas":
                if (coins >= gasCost && gasLvl < gasLvlMax)
                {
                    coins -= gasCost;
                    gas -= 0.5f;
                    SaveCoins();
                    CoinTextChanger();
                    SaveCharacteristics();
                    CheckCharasteristics();

                }
                else if(gasLvl < gasLvlMax)
                {
                    NoMoneyMenuActive();
                }
                break;
            
            default:
                NoMoneyMenuActive();
                break;   
        }
        

    }

    //menus
    void NoMoneyMenuActive()
    {
        NoMoneyMenu.SetActive(!NoMoneyMenu.activeInHierarchy);
        Debug.Log(coins);
        Debug.Log(speed);
        Debug.Log(suspension);
        Debug.Log(wheels);
        Debug.Log(gas);
    }

    void DeleteCh()
    {
        coins = 999999999;
        speed = 1800;
        suspension = 3;
        wheels = 0.20f;
        gas = 5;
        SaveCoins();
        CoinTextChanger();
        SaveCharacteristics();
        CheckCharasteristics();
    }

    void GoStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
