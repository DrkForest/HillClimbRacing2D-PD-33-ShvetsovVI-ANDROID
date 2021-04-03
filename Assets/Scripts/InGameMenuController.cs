using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    [Header("InGame UI")]
    [SerializeField] GameObject InGameMenu;
    [SerializeField] protected Button Resume;
    [SerializeField] protected Button Restart;
    [SerializeField] protected Button Exit;

    [Header("GameOver UI")]
    [SerializeField] protected Button GameOverOK;

    [SerializeField] private Image ImgPause;
    //[SerializeField] private Image ImgPause1;
    //[SerializeField] private Image ImgPause2;
    private Vector4 Color;
    private Vector4 ColorPressed;

    [SerializeField] private PauseClick PauseClick;
    [SerializeField] Button Pause;


    // Start is called before the first frame update
    void Start()
    {
        Resume.onClick.AddListener(ChangeMenuStatus);
        Restart.onClick.AddListener(RestartLevel);
        //GameOverOK.onClick.AddListener(RestartLevel);
        Exit.onClick.AddListener(MainMenu);
        GameOverOK.onClick.AddListener(MainMenu);
        Pause.onClick.AddListener(ChangeMenuStatus);
    }

    private void OnDestroy()
    {
        Resume.onClick.RemoveListener(ChangeMenuStatus);
        Restart.onClick.RemoveListener(RestartLevel);
        //GameOverOK.onClick.RemoveListener(RestartLevel);
        Exit.onClick.RemoveListener(MainMenu);
        GameOverOK.onClick.RemoveListener(MainMenu);
        Pause.onClick.RemoveListener(ChangeMenuStatus);
    }

    // Update is called once per frame
    void Update()
    {


        /*if (PauseClick.PauseIsClicked == true)
        {
            Color = new Vector4(200 / 255.0f, 200 / 255.0f, 200 / 255.0f, 1);
            ImgPause.GetComponent<Image>().color = Color;
            ChangeMenuStatus();
            //ImgPause1.GetComponent<Image>().color = Color;
            //ImgPause2.GetComponent<Image>().color = Color;
        }
        else
        {
            ColorPressed = new Vector4(241 / 255.0f, 241 / 255.0f, 241 / 255.0f, 1);
            ImgPause.GetComponent<Image>().color = ColorPressed;
            //ImgPause1.GetComponent<Image>().color = ColorPressed;
            //ImgPause2.GetComponent<Image>().color = ColorPressed;
        }*/
    }

    public void ChangeMenuStatus()
    {
        InGameMenu.SetActive(!InGameMenu.activeInHierarchy);
        Time.timeScale = InGameMenu.activeInHierarchy ? 0 : 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
