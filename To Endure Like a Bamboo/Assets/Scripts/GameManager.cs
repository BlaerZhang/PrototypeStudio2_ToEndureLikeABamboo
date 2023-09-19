using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public static GameManager instance;
    public GameObject[] playerArray;
    public bool isAllPressed;
    public bool isAllLanded;
    public bool hit;
    public CinemachineVirtualCamera topCam;
    private GameObject target;
    private bool isCamMoved = false;

    public static bool isInGame;
    public static bool isFirstTimeInGame = true;
    private bool isRoundReset = false;
    private static int roundIndicator = 0;

    public SpriteRenderer targetAlive;
    public SpriteRenderer shinobiExecution;
    
    // public static bool isWinning;

    // private void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    void Start()
    {
        isAllPressed = false;
        hit = false;
        isInGame = true;
        isAllLanded = false;
        isRoundReset = false;
        playerArray = GameObject.FindGameObjectsWithTag("Player");
        target = GameObject.Find("Target");

        if (isFirstTimeInGame)
        {
            topCam.transform.position = new Vector3(0, -32f, -15);
        }
        else
        {
            topCam.transform.position = new Vector3(0, 33, -15);
        }
    }
    
    void Update()
    {
        CheckIfAllPressed();
        CheckIfHitOrLanded();
        CheckIfFirstTimeInGane();
        
        Reset();

        if (hit)
        {
            foreach (GameObject player in playerArray)
            {
                player.GetComponent<ShinobiControl>().rb2D.bodyType = RigidbodyType2D.Kinematic;
                player.GetComponent<ShinobiControl>().rb2D.velocity = Vector2.zero;
            }

            if (isAllPressed)
            {
                MoveDownCam();
                isInGame = false;
                shinobiExecution.enabled = true;
            }
        }

        if (isAllLanded)
        {
            MoveDownCam();
            isInGame = false;
            targetAlive.enabled = true;
        }

        if ((isAllLanded || (hit && isAllPressed)) && !isRoundReset)
        {
            isRoundReset = true;
            isInGame = false;
            if (roundIndicator < 2)
            {
                Invoke("ResetRound", 11);
            }
            else if (roundIndicator == 2)
            {
                Invoke("ResetGame", 11);
                Invoke("ShowWinner", 6);
            }
            print("Reset!");
        }

        if (!AnimationManager.isPlayingFT && !isAllLanded && !(hit && isAllPressed))
        {
            isInGame = true;
        }
    }

    private void CheckIfAllPressed()
    {
        List<bool> isKeyPressedList = new List<bool>();
        foreach (GameObject player in playerArray)
        {
            isKeyPressedList.Add(player.GetComponent<ShinobiControl>().isKeyPressed);
        }

        if (!isKeyPressedList.Contains(false))
        {
            isAllPressed = true;
        }
    }

    private void CheckIfHitOrLanded()
    {
        List<bool> hitList = new List<bool>();
        List<bool> landedList = new List<bool>();
        foreach (GameObject player in playerArray)
        {
            hitList.Add(player.GetComponent<ShinobiControl>().hit);
            landedList.Add(player.GetComponent<ShinobiControl>().landed);
        }

        if (hitList.Contains(true))
        {
            hit = true;
        }

        if (!landedList.Contains(false))
        {
            isAllLanded = true;
            target.transform.DOKill();
        }
    }

    private void CheckIfFirstTimeInGane()
    {
        if (roundIndicator == 0)
        {
            isFirstTimeInGame = true;
        }
        else
        {
            isFirstTimeInGame = false;
        }
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void ResetRound()
    {
        // isInGame = true;
        // isFirstTimeInGame = false;
        // isAllLanded = false;
        // hit = false;
        // isAllPressed = false;
        // isRoundReset = false;
        roundIndicator++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Invoke("GameManagerFindPlayers", 0.1f);
    }

    private void ResetGame()
    {
        PointManager.bluePt = 0;
        PointManager.redPt = 0;
        PointManager.yellowPt = 0;
        PointManager.greenPt = 0;
        roundIndicator = 0;
        isFirstTimeInGame = true;
        SceneManager.LoadScene(0);
    }

    private void MoveDownCam()
    {
        if (!isCamMoved)
        {
            isCamMoved = true;
            topCam.transform.DOMoveY(-30.5f, 6).SetEase(Ease.Linear);
        }
    }

    private void ShowWinner()
    {
        GetComponent<AnimationManager>().ShowWinner(GetComponent<PointManager>().GetWinnerName());
    }

    // void GameManagerFindPlayers()
    // {
    //     playerArray = GameObject.FindGameObjectsWithTag("Player");
    // }
}
