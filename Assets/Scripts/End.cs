using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class End : MonoBehaviour
{
    public bool timerRunning = false;
    static float timeleft = 30;
    bool roundFinished = false;
    //bool m_play = true;
    public AudioSource ingameStart;
    //public AudioSource ingameLoop;
    GameObject scoreboard;
    GameManager manager;
    Animator sceneAnimator;
    // Start is called before the first frame update
    public float TimeRemaining 
    {
        get { return timeleft; } 
    }
    void Start()
    {
        ingameStart = GetComponent<AudioSource>();
        //ingameLoop = GetComponent<AudioSource>();
        sceneAnimator = GameObject.FindGameObjectWithTag("SceneAnimator").GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        timeleft = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    void Timer()
    {
        if (timerRunning)
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0)
            {
                timeleft = 0;
                SetupWinner();
            }

        }

    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            manager.gameRunning = false;
            timerRunning = true;
            _other.GetComponent<UIControls>().SetPlacement();
            _other.GetComponent<PlayerSetup>().DisableController();
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                player.GetComponent<UIControls>().timerIsRunning = true;
                
            }
            FinishRound();

            //Time.timeScale = 0.2f;
        }
    }

    public void SetupWinner()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<UIControls>().SetWinner();
            player.GetComponent<PlayerSetup>().SetScore();
        }        

        if (manager.cupIsPlayed && !roundFinished || manager.cupHasBeenPlayed && !roundFinished)
        {
            manager.SetPlayerData();
            manager.SetSortedScores();
           

            //player.GetComponent<PlayerSetup>().SetText();

        }
        
        roundFinished = true;
    }
    public void FinishRound()
    {
        if (manager.cupIsPlayed)
        {
            //TODO: SHOW SCOREBOARD
            Invoke("ActivateScore", 33f);
            Invoke("RestartGame", 40f);
            //NewGame();
        }
        else
        {
            if (manager.cupHasBeenPlayed)
            {
                Invoke("ActivateScore", 33f);
                //
            }
            Invoke("LoadMenu", 40f);
            //Debug.Log("GO BACK TO MENU");
        }
    }
    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        Destroy(GameObject.FindGameObjectWithTag("Data"));
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        sceneAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    void RestartGame()
    {
        ingameStart.Play();
        timeleft = 5f;
        timerRunning = false;        
        manager.DeactivateScoreBoard();        
        manager.NewRound();
        roundFinished = false;
    }

    void ActivateScore()
    {
       
            if ( manager.cupHasBeenPlayed )
            {
            manager.headline.text = "Endstand";
            }
            manager.ActivateScoreBoard();
        
        

    }
}
