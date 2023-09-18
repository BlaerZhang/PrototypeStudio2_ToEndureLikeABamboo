using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private GameManager gameManager;

    public int firstPoint;
    public int secondPoint;
    public int thirdPoint;
    public int fourthPoint;

    public TextMeshProUGUI redPtText;
    public TextMeshProUGUI greenPtText;
    public TextMeshProUGUI bluePtText;
    public TextMeshProUGUI yellowPtText;

    public static int redPt;
    public static int greenPt;
    public static int bluePt;
    public static int yellowPt;

    public List<GameObject> playerNotPressed;
    public List<GameObject> playerPressed;

    private bool isPointCalculated = false;
    private int hitPlayerIndex;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerNotPressed = gameManager.playerArray.ToList();
        isPointCalculated = false;
    }
    
    void Update()
    {
        UpdatePressList();
        UpdatePoints();
        DisplayPoints();
    }

    void UpdatePressList()
    {
        for (int i = 0; i < playerNotPressed.Count; i++)
        {
            GameObject player = playerNotPressed[i];
            
            if (player.GetComponent<ShinobiControl>().isKeyPressed)
            {
                playerNotPressed.Remove(player);
                playerPressed.Add(player);
            }
        }
    }

    void UpdatePoints()
    {
        if (gameManager.isAllPressed && gameManager.hit)
        {
            CalculatePoints();
            foreach (GameObject player in playerPressed)
            {
                
            }
        }

        if (gameManager.isAllLanded)
        {
            
        }
    }

    void CalculatePoints()
    {
        if (!isPointCalculated)
        {
            foreach (GameObject player in playerPressed)
            {
                string name = player.name;
                switch (name)
                {
                    case "ShinobI":
                        redPt += GetPoints(player);
                        break;
                    case "ShinobE":
                        greenPt += GetPoints(player);
                        break;
                    case "ShinoB":
                        bluePt += GetPoints(player);
                        break;
                    case "ShinobY":
                        yellowPt += GetPoints(player);
                        break;
                }
                player.GetComponentInChildren<TextMeshPro>().text += "        +" + GetPoints(player);
            }
            isPointCalculated = true;
        }
    }

    void DisplayPoints()
    {
        // player.GetComponent<ShinobiControl>().uiIcon.GetComponentInChildren<TextMeshProUGUI>().text = 0;
        redPtText.text = redPt.ToString() + " Pt";
        bluePtText.text = bluePt.ToString() + " Pt";
        greenPtText.text = greenPt.ToString() + " Pt";
        yellowPtText.text = yellowPt.ToString() + " Pt";
    }

    int GetPoints(GameObject player)
    {
        int distance;
        int point = 0;

        for (int i = 0; i < playerPressed.Count; i++)
        {
            GameObject p = playerPressed[i];
            if (p.GetComponent<ShinobiControl>().hit)
            {
                hitPlayerIndex = i;
            }
        }
        
        distance = Mathf.Abs(playerPressed.IndexOf(player) - hitPlayerIndex);
        
        switch (distance)
        {
            case 0:
                point = firstPoint;
                break;
            case 1:
                point = secondPoint;
                break;
            case 2:
                point = thirdPoint;
                break;
            case 3:
                point = fourthPoint;
                break;
        }

        return point;
    }

    public string GetWinnerName()
    {
        Dictionary<string, int> pointDict = new Dictionary<string, int>();
        pointDict.Add("<color=#ff8080>ShinobI</color>", redPt);
        pointDict.Add("<color=#8095ff>ShinoB</color>", bluePt);
        pointDict.Add("<color=#8aff80>ShinobE</color>", greenPt);
        pointDict.Add("<color=#ffea80>ShinobY</color>", yellowPt);

        string winnerName = "";

        foreach (var kvpair in pointDict)
        {
            if (kvpair.Value == pointDict.Values.Max())
            {
                winnerName += kvpair.Key;
            }
        }
        
        return winnerName;
    }
}
