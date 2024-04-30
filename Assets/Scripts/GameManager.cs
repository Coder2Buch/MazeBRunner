using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int players = 4;
    public GameObject mysteryBoxPrefab;
    public GameObject endPrefab;
    public GameObject cam;
    public GameObject scoreboard;
    public int activePlayers;
    int playersReady;
    int roundsPlayed;
    PlayerInputManager manager;
    public bool gameRunning = false;
    bool timerRunning = false;
    //Change time also in NewRound() Default: 180 sec
    float gameTime = 180;
    public string gameTimeText;
    static float startTimer = 3f;
    public bool cupIsPlayed;
    public bool cupHasBeenPlayed = false;
    bool roundFinished = false;
    Data data;
    GameObject dataObj;
    
    public int[] playerPos;

    public TextMeshProUGUI[] text_PlayerScore;
    public TextMeshProUGUI[] text_Player;
    public TextMeshProUGUI[] text_PointsAdded;
    public TextMeshProUGUI headline;

    bool player0AlreadySet;
    bool player1AlreadySet;
    bool player2AlreadySet;
    bool player3AlreadySet;
    public bool ingameMenuActive = false;

    public string playerName0;
    public string playerName1;
    public string playerName2;
    public string playerName3;

    int playerPlacement0;
    int playerPlacement1;
    int playerPlacement2;
    int playerPlacement3;

    int[] arrayPlayerScores;
    public int playerScore0;
    public int playerScore1;
    public int playerScore2;
    public int playerScore3;

    public List<int> listPlayerScores;
    public List<int> listPlacementsSet;
    public List<int> listPointsSet;
    public List<string> listPlayers;

    public GameObject[] scoreboardLines;

    public float TimeRemaining
    {
        get { return startTimer; }
    }
    // Start is called before the first frame update
    void Awake ()
    {
        roundsPlayed = 1;
        dataObj = GameObject.FindGameObjectWithTag( "Data" );
        data = dataObj.GetComponent<Data>();
        cupIsPlayed = data.cupMode;
        manager = GetComponent<PlayerInputManager>();
        startTimer = 3f;
        SpawnMysteryBoxes();
        SpawnEndPoint();

        //listPlayers.Add( playerName );
        //listPlayers.Add( playerName1 );
        //listPlayers.Add( playerName2 );
        //listPlayers.Add( playerName3 );


    }

    public void SetPlayerData ()
    {
        var p = GameObject.FindGameObjectsWithTag("Player");
        foreach ( var player in p )
        {

            if ( player.GetComponent<PlayerSetup>().playerNumber == 0 )
            {
                playerName0 = player.GetComponent<PlayerSetup>().playerName;
                playerScore0 = player.GetComponent<PlayerSetup>().playerScore;
                listPlayerScores.Add( playerScore0 );
                playerPlacement0 = player.GetComponent<UIControls>().placement;
                player0AlreadySet = false;

                Debug.Log( "Spieler1 Name:" + playerName0 );
            }
            else if ( player.GetComponent<PlayerSetup>().playerNumber == 1 )
            {
                playerName1 = player.GetComponent<PlayerSetup>().playerName;
                playerScore1 = player.GetComponent<PlayerSetup>().playerScore;
                listPlayerScores.Add( playerScore1 );
                playerPlacement1 = player.GetComponent<UIControls>().placement;
                player1AlreadySet = false;

                Debug.Log( "Spieler2 Name:" + playerName1 );
            }
            else if ( player.GetComponent<PlayerSetup>().playerNumber == 2 )
            {
                playerName2 = player.GetComponent<PlayerSetup>().playerName;
                playerScore2 = player.GetComponent<PlayerSetup>().playerScore;
                listPlayerScores.Add( playerScore2 );
                playerPlacement2 = player.GetComponent<UIControls>().placement;
                player2AlreadySet = false;

                Debug.Log( "Spieler3 Name:" + playerName2 );
            }
            else if ( player.GetComponent<PlayerSetup>().playerNumber == 3 )
            {
                playerName3 = player.GetComponent<PlayerSetup>().playerName;
                playerScore3 = player.GetComponent<PlayerSetup>().playerScore;
                listPlayerScores.Add( playerScore3 );
                playerPlacement3 = player.GetComponent<UIControls>().placement;
                player3AlreadySet = false;

                Debug.Log( "Spieler3 Name:" + playerName3 );

            }
            //listPlayerScores.Add( player.GetComponent<PlayerSetup>().playerScore );


        }
    }

    void OnGUI ()
    {
        //var current = (int)(1f / Time.unscaledDeltaTime);
        // GUI.Label(new Rect(5, 5, 30, 25), current.ToString());
    }
    // Update is called once per frame
    void Update ()
    {
        StartTimer();
        GameTimer();
    }
    void StartTimer ()
    {
        if ( timerRunning )
        {
            startTimer -= Time.deltaTime;
            if ( startTimer <= 0 && timerRunning )
            {
                startTimer = 0;
                gameRunning = true;
                var players = GameObject.FindGameObjectsWithTag("Player");
                foreach ( GameObject player in players )
                {
                    player.GetComponent<PlayerSetup>().EnableController();
                    player.GetComponent<UIControls>().DisableStartUI();
                    player.GetComponent<UIControls>().startIsRunning = false;
                }
                timerRunning = false;
            }

        }
    }
    void GameTimer ()
    {
        if ( gameRunning )
        {
            gameTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60F);
            int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
            gameTimeText = string.Format( "{0:0}:{1:00}" , minutes , seconds );

            //GUI.Label(new Rect(10, 10, 250, 100), niceTime);
            if ( gameTime <= 0 )
            {
                gameTime = 0.1f;
                minutes = Mathf.FloorToInt( gameTime / 60F );
                seconds = Mathf.FloorToInt( gameTime - minutes * 60 );
                gameTimeText = string.Format( "{0:0}:{1:00}" , minutes , seconds );
                gameRunning = false;
                var obj = GameObject.FindGameObjectWithTag("End");
                obj.GetComponent<End>().SetupWinner();
                obj.GetComponent<End>().FinishRound();

            }
        }
    }
    void OnPlayerJoined ()
    {
        //cam.SetActive(false);
        activePlayers++;
        playersReady = 0;
        var playersOnline = GameObject.FindGameObjectsWithTag("Player");
        foreach ( var obj in playersOnline )
        {

            obj.GetComponent<UIControls>().Unready();
        }
    }

    public void Ready ()
    {
        playersReady++;
        if ( playersReady == activePlayers )
        {
            timerRunning = true;
            manager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach ( GameObject player in players )
            {
                player.GetComponent<UIControls>().DisableStartUI();
                player.GetComponent<UIControls>().startIsRunning = true;
            }
        }
    }

    public void NewRound ()
    {
        if ( roundsPlayed < data.rounds )
        {
            Debug.Log( roundsPlayed );
            roundsPlayed++;
            playersReady = 0;
            startTimer = 3f;
            gameTime = 180f;
            timerRunning = false;
            gameRunning = false;
            players = 4;
            DestroyEndPoint();
            SpawnEndPoint();
            DestroyMysteryBoxes();
            SpawnMysteryBoxes();
            DestroyTraps();
            DestroyParticle();
            //TODO: DestroyTraps();
            //TODO: Choose new Spawn Points for different Map
            var plays = GameObject.FindGameObjectsWithTag("Player");
            foreach ( GameObject player in plays )
            {
                player.GetComponent<UIControls>().placement = 0;
                player.GetComponent<UIControls>().gotPlacement = false;
                //Spawn Player
                player.GetComponent<PlayerSetup>().SetupPlayer();
                //Reset Items for Player
                player.GetComponent<ItemControls>().ItemsReset();
                //Enable ReadyUp UI 
                player.GetComponent<UIControls>().EnableStartUI();
                player.GetComponent<UIControls>().timerIsRunning = false;
                player.GetComponent<UIControls>().winner.SetActive( false );

            }
            if ( roundsPlayed == data.rounds && cupIsPlayed )
            {
                cupHasBeenPlayed = true;
                cupIsPlayed = false;
            }
        }

    }

    void SpawnMysteryBoxes ()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("MysterySpawnPoint");
        for ( int i = 0 ; i < spawnPoints.Length ; i++ )
        {
            Instantiate( mysteryBoxPrefab , spawnPoints [ i ].transform.position , Quaternion.identity );
        }
    }

    void DestroyMysteryBoxes ()
    {
        var mysteryBoxes = GameObject.FindGameObjectsWithTag("MysteryBox");
        for ( int i = 0 ; i < mysteryBoxes.Length ; i++ )
        {
            Destroy( mysteryBoxes [ i ] );
        }
    }
    void DestroyParticle ()
    {
        var particles = GameObject.FindGameObjectsWithTag("Particle");
        for ( int i = 0 ; i < particles.Length ; i++ )
        {
            Destroy( particles [ i ] );
        }
    }
    void SpawnEndPoint ()
    {
        var endSpawnPoints = GameObject.FindGameObjectsWithTag("EndSpawnPoint");
        int rnd = Random.Range(0, endSpawnPoints.Length);
        Instantiate( endPrefab , endSpawnPoints [ rnd ].transform.position , endSpawnPoints [ rnd ].transform.rotation );

    }

    void DestroyEndPoint ()
    {
        var endObject = GameObject.FindGameObjectWithTag("End");
        Debug.Log( "Destroying EndBox" );
        Destroy( endObject );
    }

    void DestroyTraps ()
    {
        var traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach ( var trap in traps )
        {
            Destroy( trap );
        }
    }

    public void SortScores ( List<int> scorelist )
    {
        int k = 0; // Hilfsvariable
        int j = 0; // Hilfsvariable

        for ( int i = 0 ; i < scorelist.Count ; i++ )
        {
            j = i;
            while ( ( j > 0 ) && ( scorelist [ j - 1 ] > scorelist [ j ] ) )
            {
                k = scorelist [ j - 1 ];
                scorelist [ j - 1 ] = scorelist [ j ];
                scorelist [ j ] = k;
                j--;
            }
        }
    }


    public void SetSortedScores ()   // TODO FIX Placement Points // TODO Add Bool for Saftey if points are equal
    {
        // neue liste in der 'i' gespeichert wird damit scores nicht mehrfach gesetzt werden, i darf noch nicht benutzt worden sein
        
        //Debug.Log( "Active Players: " + activePlayers );
        //Debug.Log( "PLATZIERUNGSLISTE: " + listPlayerScores[0] + " " + listPlayerScores [ 1 ] + " " + listPlayerScores [ 2 ]  );
        
        SortScores( listPlayerScores );// Sicherheit dass name nicht mehrfach gesetzt wird mithilfe von bool abfragen fuer jeden namen

        for ( int i = 0 ; i < activePlayers ; i++ )
        {
           
                text_PlayerScore [ activePlayers - 1 - i ].text = listPlayerScores [ i ].ToString();
                
            


        }

        

        for ( int i = 0 ; i < activePlayers ; i++ )
        {
            //Debug.Log( "PlayerScore: " + playerScore0 );
            if ( listPlayerScores [ i ] == playerScore0 && !player0AlreadySet )
            {
                text_Player [ activePlayers - 1 - i ].text = playerName0;
                //Debug.Log( "Spieler1 Name: " + playerName0 + " Platzierung " + ( i + 1 ) );
                player0AlreadySet = true;
                listPlacementsSet.Add( i );
                //text_PointsAdded [ activePlayers - 1 - i ].text = "+" + (4-playerPlacement0); deutlich bessere Variante

                switch ( playerPlacement0 )
                {
                    case 1:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+3";
                        break;
                    case 2:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+2";
                        break;
                    case 3:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+1";
                        break;
                    case 4:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+0";
                        break;
                    default:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "DNF";
                        break;
                }

            }
        }
        for ( int i = 0 ; i < activePlayers ; i++ )
        {
            //Debug.Log( "PlayerScore1: " + playerScore1 );

            if ( listPlayerScores [ i ] == playerScore1  && activePlayers > 1 && !player1AlreadySet 
                && !listPlacementsSet.Contains(i))
            {
                text_Player [ activePlayers - 1 - i ].text = playerName1;
                //Debug.Log( "Spieler2 Name: " + playerName1 + " Platzierung " + (i + 1) );
                player1AlreadySet = true;
                listPlacementsSet.Add( i );
                switch ( playerPlacement1 )
                {
                    case 1:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+3";
                        break;
                    case 2:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+2";
                        break;
                    case 3:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+1";
                        break;
                    case 4:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+0";
                        break;
                    default:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "DNF";
                        break;
                }
            }
        }
        for ( int i = 0 ; i < activePlayers ; i++ )
        {
            //Debug.Log( "PlayerScore2: " + playerScore2 );

            if ( listPlayerScores [ i ] == playerScore2 && activePlayers > 2 && !player2AlreadySet
                && !listPlacementsSet.Contains( i ) )
            {
                text_Player [ activePlayers - 1 - i ].text = playerName2;
                //Debug.Log( "Spieler3 Name: " + playerName2 + " Platzierung " + (i + 1) );
                player2AlreadySet = true;
                listPlacementsSet.Add( i );
                switch ( playerPlacement2 )
                {
                    case 1:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+3";
                        break;
                    case 2:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+2";
                        break;
                    case 3:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+1";
                        break;
                    case 4:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+0";
                        break;
                    default:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "DNF";
                        break;
                }

            }
        }
        for ( int i = 0 ; i < activePlayers ; i++ )
        {
            //Debug.Log( "PlayerScore3: " + playerScore3 );

            if ( listPlayerScores [ i ] == playerScore3 && activePlayers > 3 && !player3AlreadySet
                && !listPlacementsSet.Contains( i ) )
            {
                text_Player [ activePlayers - 1 - i ].text = playerName3;
                player3AlreadySet = true;
                listPlacementsSet.Add( i );
                //Debug.Log( "Spieler4 Name: " + playerName3 + " Platzierung " + i + 1 );
                switch ( playerPlacement3 )
                {
                    case 1:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+3";
                        break;
                    case 2:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+2";
                        break;
                    case 3:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+1";
                        break;
                    case 4:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "+0";
                        break;
                    default:
                        text_PointsAdded [ activePlayers - 1 - i ].text = "DNF";
                        break;
                }

            }
        }
        listPlayerScores.Clear();
        listPlacementsSet.Clear();
       


    }
    public void ActivateScoreBoard ()
    {
        scoreboard.SetActive( true );
    }
    public void DeactivateScoreBoard ()
    {
        scoreboard.SetActive( false );
    }

}