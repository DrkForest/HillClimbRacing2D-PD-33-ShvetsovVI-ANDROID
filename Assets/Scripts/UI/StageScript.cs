using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageScript : MonoBehaviour
{
    [SerializeField] protected Button level1;

    [SerializeField] protected Button back;

    [SerializeField] private TextMeshProUGUI coinsText;
    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        level1.onClick.AddListener(Level1);
        back.onClick.AddListener(Back);
        CoinTextChanger();
    }

    private void OnDestroy()
    {
        level1.onClick.RemoveListener(Level1);
        back.onClick.RemoveListener(Back);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Level1()
    {
        SceneManager.LoadScene(2);
    }
    void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
    }


    public void LoadCoins()
    {
        string key = "Coins";
        if (PlayerPrefs.HasKey(key))
        {
            this.coins = PlayerPrefs.GetInt(key);
        }

    }
    public void CoinTextChanger()
    {
        LoadCoins();
        coinsText.SetText(this.coins.ToString());
    }
}
