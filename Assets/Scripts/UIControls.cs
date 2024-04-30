using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject endObj;
    PlayerSetup setup;


    public TextMeshProUGUI text;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;
    public TextMeshProUGUI headline;

    public GameObject compassUIImage;
    public GameObject compassUIButton;
    public GameObject winner;
    public GameObject timer;
    public GameObject startUI;
    public GameObject scoreboard;
    public GameObject menuPanel;
    public GameObject selectedButton;
    public int score;
    //public GameObject playerManager;
    public Image liveRed;
    public Image readyImage;
    public Sprite[] readySprites;
    private PlayerInput input;
    private Image setScale;
    private GameManager manager;
    private PlayerController player;
    private End end;
    private string playerNumber;
    public int placement;
    public bool timerIsRunning = false;
    public bool startIsRunning = false;
    public bool gotPlacement = false;
    public bool ready = false;
    

    EventSystem m_EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        m_EventSystem = EventSystem.current;
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        endObj = GameObject.FindGameObjectWithTag("End");

        manager = playerManager.GetComponent<GameManager>();
        end = endObj.GetComponent<End>();
        input = GetComponent<PlayerInput>();
        setScale = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        player = input.GetComponent<PlayerController>();
        playerNumber = player.name;
        //text.text = playerNumber;
        if(timerIsRunning)
        {
            timer.SetActive(true);
            int time = (int)end.TimeRemaining;
            timerText.text = time.ToString();
        }
        if (startIsRunning)
        {
            timer.SetActive(true);
            int time = (int)manager.TimeRemaining;
            timerText.text = time.ToString();
        }
        else if(!startIsRunning && !timerIsRunning)
        {
            timer.SetActive(false);
        }

        if(player.compass)
        {
            compassUIImage.SetActive(true);
            compassUIButton.SetActive( true );
        }
        else
        {
            compassUIImage.SetActive(false);
            compassUIButton.SetActive( false );
        }
    }
    public void SetWinner()
    {

        switch (placement)
        {
            case 1: winnerText.text = "Du hast gewonnen!";
                winner.SetActive(true);
                //GetComponent<PlayerSetup>().text_PointsAdded[GetComponent<PlayerSetup>().playerNumber].text = "+3";
                //score += 3;
                break;
            case 2: winnerText.text = "Du bist Zweiter";
                winner.SetActive(true);
                /*GetComponent<PlayerSetup>().text_PointsAdded [ GetComponent<PlayerSetup>().playerNumber ].text = "+2";*/
                //score += 2;
                break;
            case 3: winnerText.text = "Du bist Dritter";
                winner.SetActive(true);
                /*GetComponent<PlayerSetup>().text_PointsAdded [ GetComponent<PlayerSetup>().playerNumber ].text = "+1";*/
                //score += 1;
                break;
            case 4: winnerText.text = "Du bist Vierter";
                winner.SetActive(true);
                /*GetComponent<PlayerSetup>().text_PointsAdded [ GetComponent<PlayerSetup>().playerNumber ].text = "+0";*/
                break;
            default: winnerText.text = "Du bist nicht ans Ziel gekommen";
                winner.SetActive(true);
                //GetComponent<PlayerSetup>().text_PointsAdded [ GetComponent<PlayerSetup>().playerNumber ].text = "+0";

                break;

        }

        winner.SetActive(true);
    }

    public void SetPlacement()
    {
        Debug.Log(manager.players);
        if(!gotPlacement)
        {
            switch (manager.players)
            {
                case 4:
                    placement = 1;
                    score += 3;
                    
                    gotPlacement = true;
                    break;
                case 3:
                    placement = 2;
                    score += 2;
                    
                    gotPlacement = true;
                    break;
                case 2:
                    placement = 3;
                    score += 1;
                    
                    gotPlacement = true;
                    break;
                case 1:
                    placement = 4;
                    //manager.text_PointsAdded [ GetComponent<PlayerSetup>().playerNumber ].text = "+0";
                    gotPlacement = true;
                    break;
                default: break;
            }
            manager.players--;
        }

    }
    void OnReady()
    {
        if(!ready)
        {
            manager.Ready();
            readyImage.sprite = null;
            readyImage.sprite = readySprites[1];
            ready = true;
        }

        
    }
    void OnPause()
    {
        if ( menuPanel.activeSelf == false && !manager.ingameMenuActive )
        {
            manager.ingameMenuActive = true;
            endObj = GameObject.FindGameObjectWithTag( "End" );
            end = endObj.GetComponent<End>();
            menuPanel.SetActive( true );
            m_EventSystem.SetSelectedGameObject( selectedButton );
            GetComponent<PlayerSetup>().SetDefaultMap( "UI" );
            Time.timeScale = 0;
        }        
    }
    public void Continue()
    {
        menuPanel.SetActive(false);
        GetComponent<PlayerSetup>().SetDefaultMap("Player");
        manager.ingameMenuActive = false;
        Time.timeScale = 1;

    }

    public void BackToMenu()
    {
        menuPanel.SetActive(false);
        GetComponent<PlayerSetup>().SetDefaultMap("UI");
        manager.ingameMenuActive = false;
        Time.timeScale = 1;
        end.LoadMenu();
    }

    public void Unready()
    {
        ready = false;
        readyImage.sprite = readySprites[0];
    }
    public void DisableStartUI()
    {
        startUI.SetActive(false);
    }
    public void EnableStartUI()
    {
        startUI.SetActive(true);
        Unready();
    }
    //public void ActivateScoreBoard()
    //{
    //    scoreboard.SetActive( true );
    //}
    //public void DeactivateScoreBoard ()
    //{
    //    scoreboard.SetActive( false );
    //}

    public void OnCompass()
    {
        if(player.compass)
        {
            player.navigator.SetActive(true);
            player.compass = false;
            player.StartCounter(0);
            player.StartCounter(1);
        }

    }
    
}
