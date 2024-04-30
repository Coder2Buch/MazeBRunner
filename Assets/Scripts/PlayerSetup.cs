using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    public GameObject keyboard;
    GameObject playerManager;
    PlayerInput input;
    PlayerController controller;
    PlayerAttack attack;
    public string playerName;
    public int playerNumber;
    int numberOfPlayers = 0;


    public int playerScore;
    //public TextMeshProUGUI[] text_PlayerScore;
    //public TextMeshProUGUI[] text_Player;
    //public TextMeshProUGUI[] text_PointsAdded;
    public TextMeshProUGUI gameTimer;
    GameManager manager;


    bool engine = true;

    // Start is called before the first frame update
    void Awake()
    {
        //Application.targetFrameRate = -1;

        playerManager = GameObject.FindGameObjectWithTag("Manager");

        manager = playerManager.GetComponent<GameManager>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        attack = GetComponent<PlayerAttack>();
        SetupPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.gameRunning)
        {
            gameTimer.gameObject.SetActive(true);
            gameTimer.text = manager.gameTimeText;
        }
        else
        {
            gameTimer.gameObject.SetActive(false);
        }

        //-CHEAT CODE
        if(playerName == "ENGINE" && engine)
        {
            controller.movespeed = 8f;
            Invoke("TrollEngine", 15f);
            engine = false;

        }
    }
    public void SetupPlayer()
    {
        playerNumber = input.playerIndex;
        //numberOfPlayers++;
        if (!manager.cupIsPlayed)
        {
            keyboard.SetActive(false);
            SetDefaultMap("Player");

        }

        DisableController();
        // Put player at spawn point.
        transform.position = PickSpawnPoint().transform.position;
    }
    private GameObject PickSpawnPoint()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        //Debug.Log(input.playerIndex + " Spawned");
        return spawnPoints[input.playerIndex];
    }

    public void EnableController()
    {
        controller.enabled = true;
        attack.enabled = true;

    }

    public void DisableController()
    {
        controller.enabled = false;
        attack.enabled = false;

    }
    public void SetScore()
    {
        playerScore = GetComponent<UIControls>().score;
    }

    //public void SetText()
    //{
    //    text_Player[0].text = manager.playerName;
    //    text_Player[1].text = manager.playerName1;
    //    text_Player[2].text = manager.playerName2;
    //    text_Player[3].text = manager.playerName3;

    //    text_PlayerScore[0].text = manager.playerScore.ToString();
    //    text_PlayerScore[1].text = manager.playerScore1.ToString();
    //    text_PlayerScore[2].text = manager.playerScore2.ToString();
    //    text_PlayerScore[3].text = manager.playerScore3.ToString();
    //    Debug.Log( "Punktzahl: " + manager.playerScore );
    //}
    //public void SetSortedScores ()
    //{
    //    manager.SortScores( manager.listPlayerScores );
    //    for ( int i = 0 ; i < manager.activePlayers ; i++ )
    //    {
    //        text_PlayerScore [ 3-i ].text = manager.listPlayerScores [ i ].ToString();


    //    }

    //    //if ( roundsPlayed > 0 )
    //    //{

    //    for ( int i = 0 ; i < manager.activePlayers ; i++ )
    //    {
    //        if ( manager.listPlayerScores [ i ] == manager.playerScore && manager.activePlayers == 1  )
    //        {
    //            text_Player [ i ].text = manager.playerName3;

    //        }
    //    }
    //    for ( int i = 0 ; i < manager.activePlayers ; i++ )
    //    {
    //        if ( manager.listPlayerScores [ i ] == manager.playerScore1 && manager.activePlayers == 2 )
    //        {
    //            text_Player [ i ].text = manager.playerName2;
    //        }
    //    }
    //    for ( int i = 0 ; i < manager.activePlayers ; i++ )
    //    {
    //        if ( manager.listPlayerScores [ i ] == manager.playerScore2 && manager.activePlayers == 3 )
    //        {
    //           text_Player [ i ].text = manager.playerName1;
    //        }
    //    }
    //    for ( int i = 0 ; i < manager.activePlayers ; i++ )
    //    {
    //        if ( manager.listPlayerScores [ i ] == manager.playerScore3 && manager.activePlayers == 4 )
    //        {
    //            text_Player [ i ].text = manager.playerName;
    //        }
    //    }

        //}






    //}


public void SetDefaultMap(string _map)
    {
        input.SwitchCurrentActionMap(_map);
    }

    public void TrollEngine()
    {
        controller.movespeed = 2.25f;
    }
}
