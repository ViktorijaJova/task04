using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballprefab;
    public GameObject paddlepefab;
    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelComplited;
    public GameObject panelGameover;
    public GameObject[] levels;
    //implement more levels

    public Text scoreText;
    public Text levelText;
    public Text ballsText;
    public Text highScoreText;
    
    public static GameManager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLITED, LOADLEVEL, GAMEOVER }
    State _state;
    GameObject _currentball;
    GameObject _currentleve;
    bool _isSwitching;



    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value;
            scoreText.text = "Score:" + _score;
        }
    }

  
    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value;
            levelText.text = "Level" + _level;
        }
    }


    private int _balls;
    public int Balls
    {
        get { return _balls; }
        set
        {
            _balls = value;
            ballsText.text = "Balls:" + _balls;
        }
    }

    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }
    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
              //  highScoreText.text = "HighScore" + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                Instantiate(paddlepefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLITED:
                Destroy(_currentball);
                Destroy(_currentleve);
                Level++;
                Score+=100; 
                panelLevelComplited.SetActive(true);
                SwitchState(State.LOADLEVEL,2f);

                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentleve = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                
                }

                break;
            case State.GAMEOVER:
              /*  if(Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("higherscore",Score);
                }*/
                panelGameover.SetActive(true);
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_currentball == null)
                {
                    if(Balls > 0)
                    {
                         _currentball = Instantiate(ballprefab);
                    }
                    else if(Balls ==0)
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                if(_currentleve !=null && _currentleve.transform.childCount == 0 && !_isSwitching)
                {
                    SwitchState(State.LEVELCOMPLITED);
                }

                break;
            case State.LEVELCOMPLITED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

   public IEnumerator SwitchDelay(State newState,float delay)
    {
        _isSwitching = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitching = false;

    }


        void EndState()
    {
            switch (_state)
            {
                case State.MENU:
                panelMenu.SetActive(false);
                    break;
                case State.INIT:
                    break;
                case State.PLAY:
                    break;
                case State.LEVELCOMPLITED:
                panelLevelComplited.SetActive(false);

                break;
                case State.LOADLEVEL:
                    break;
                case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameover.SetActive(false);
                    break;
            }
        }
    }
