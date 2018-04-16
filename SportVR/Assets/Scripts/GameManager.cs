using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("UI Timer")]
    public Text delayTimetext;
    public Text countDownTimeText;
    public int countDownTime;

    [Header("UI Score")]
    public Text scoreText;

    [Header("UI Player Life")]
    public GameObject Life;
    public GameObject[] LifeImage;

    [Header("UI Message Show")]
    public GameObject gameOverMessage;
    public GameObject gameEndMessage;

    [Header("Game Object")]
    public GameObject GazeController;
    public GameObject OVRCameraRig;
    public GameObject HitColliderLeft;
    public GameObject HitColliderRight;

    public bool dieBall;
   
    private int score;//排名用途，儲存

    private int delayCount;
    private int minute;
    private int heartCount;
    private float _bounceBallForce;
    private float _ballCollision;
    private float _shootBall;
    private bool _CanSaveData;

    public  static GameManager _Instance;
    
    // All Game state in this game 
    public enum GameStarte
    {
        WAIT,//Delay to start
        PLAY,// play
        EXIT,// When player choise exit，back to menu
        PAUSE,// When the game end，set active Gamer over UI to player choise restart or exit
        RESTART,// restart game
        GAMEROVER// When player lofe is 0
        
    }
    static GameStarte _state;
    

    void Awake()
    {
        _Instance = this;

        if (StartManager._Levelchoise == 1) //Level 1
        {
            _bounceBallForce = 500f;
            _ballCollision = 550f;
            _shootBall = 400;
            _CanSaveData = false;
        }
        else if (StartManager._Levelchoise == 2) //Level 2
        {
            _bounceBallForce = 600f;
            _ballCollision = 650f;
            _shootBall = 500;
            _CanSaveData = false;
        }
        else if(StartManager._Levelchoise == 3) //Level 3
        {
            HitColliderLeft.SetActive(false);
            HitColliderRight.SetActive(false);
            _bounceBallForce = 1000f;
            _ballCollision = 1050f;
            _shootBall = 800f;
            _CanSaveData = true;
        }

    }
    void Start()
    {
        delayCount = 3;
        //countDownTime = 60;
        minute = 0;
        score = 0;
        dieBall = false;
        heartCount = LifeImage.Length;
        gameOverMessage.SetActive(false);

        delayTimetext.text = String.Empty;
        countDownTimeText.text = String.Empty;
        scoreText.text = String.Empty;
        SetState(GameStarte.WAIT);

        
    }

    public float _getbounceball()
    {
        return _bounceBallForce;
    }

    public float _getballcollision()
    {
        return _ballCollision;
    }

    public float _getshootball()
    {
        return _shootBall;
    }

    private void Update()
    {

        
    }

    //計算顯示分數
    public void SetScore(int hitScore)
    {
        score += hitScore;
        scoreText.text = "Score : " + score;
    }
    

    // Set the Game state
    public void SetState(GameStarte state)
    {
        switch (state)
        {
            case GameStarte.WAIT:
                WaitForGame();
                break;
            case GameStarte.PLAY:
                PlayTheGame();
                break;
            case GameStarte.GAMEROVER:
                GamerOverTheGame();
                break;
            case GameStarte.PAUSE:
                PauseTheGame();
                break;
            case GameStarte.RESTART:
                RestartTheGame();
                break;
            case GameStarte.EXIT:
                //回到選單畫面
                break;
        }
        
    }
    // Get the Game state by other Script
    public GameStarte GetState()
    {
        return _state;
    }


    // Wait for second to start game
    void WaitForGame()
    {
        _state = GameStarte.WAIT;

        PlayerHealth();
        gameEndMessage.SetActive(false);
        StartCoroutine(DelayGameToStart());
       
    }

    // Start to play the game
    void PlayTheGame()
    {
        _state = GameStarte.PLAY;

        StopCoroutine(DelayGameToStart());      
        delayTimetext.text = String.Empty;
        StartCoroutine(CountDownTimer());
        GameObject.Find("SpawnBall").GetComponent<ShootBall>().enabled = true;
        gameEndMessage.SetActive(false);
        PlayerHealth();
        scoreText.text = "Score : ";
        

    }

    // The game is GmaeOver
    void GamerOverTheGame()
    {
        _state = GameStarte.GAMEROVER;

        GazeController.SetActive(false);
        gameOverMessage.SetActive(true);
        gameEndMessage.SetActive(false);
        OVRCameraRig.GetComponent<ControllerRayCast>().enabled = true;
        Statistics();

    }

    // Pause The Game
    void PauseTheGame()
    {
        _state = GameStarte.PAUSE;

        GazeController.SetActive(false);
        gameEndMessage.SetActive(true);
        OVRCameraRig.GetComponent<ControllerRayCast>().enabled = true;
        Statistics();
        
    }

    // Restart the game
    void RestartTheGame()
    {
        _state = GameStarte.RESTART;

        PlayerHealth();
    }

   
    // Change tje scene
    public void ChangeScene(String scene)
    {

        GC.Collect();
        SceneManager.LoadScene(scene);

    }

    //玩家生命值
    void PlayerHealth()
    {
        if (_state == GameStarte.PLAY )//開始顯示生命值
        {
            
            Life.SetActive(true);
        }
        else if(_state == GameStarte.PAUSE )//時間到計算生命來結算成績加成
        {
            
        }
        else//隱藏生命值 (Delay，Exit，Restart 的時候)
        {
            
            Life.SetActive(false);
        }
    }

    //扣血
    public void DestroyHealth()
    {
        
        LifeImage[--heartCount].SetActive(false);
        
    }

    // 結算
    void Statistics()
    {
        StopCoroutine(CountDownTimer());
        GameObject.Find("SpawnBall").GetComponent<ShootBall>().enabled = false;
        dieBall = false;
        if (heartCount >0)// 還有生命值
        {
            PlayerHealth();//進入計算生命加成
        }
        countDownTimeText.text = String.Empty;//時間文字清空
        scoreText.text = "Your Score " + score;//顯示最終成績
        if (_CanSaveData)
        {
            LeaderBoard._instance.NewScore(score);
        }

    }

    //Delay 3 seconds to start
    IEnumerator DelayGameToStart()
    {
        yield return new WaitForSeconds(0.5f);
        while (delayCount>0)
        {
            delayTimetext.text = delayCount + "";
            delayCount--;
            if (delayCount == 0)
            {
                yield return new WaitForSeconds(1f);
                delayTimetext.text = "GO";
                yield return new WaitForSeconds(1f);
                SetState(GameStarte.PLAY);
                
            }
            yield return new WaitForSeconds(1f);
        }
    }



    //倒數計時
    IEnumerator CountDownTimer()
    {
        while (countDownTime >0)
        {
            countDownTimeText.text  = (countDownTime / 60)>0 ? ""+ (countDownTime / 60) +":" + (countDownTime % 60) : countDownTimeText.text = countDownTime + "";
            countDownTime--;
            if (countDownTime == 0)//時間到
            {               
                yield return new WaitForSeconds(1f);
                SetState(GameStarte.PAUSE);//遊戲狀態設定為 暫停
            }
            if (countDownTime >0 && heartCount <=0)//時間還沒到 並且血量已歸零
            {
                SetState(GameStarte.GAMEROVER);//遊戲狀態設定為 死亡
            }
            yield return new WaitForSeconds(1);
        }
    }





}
