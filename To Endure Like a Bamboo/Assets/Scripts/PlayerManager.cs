using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerRed;
    public GameObject playerGreen;
    public GameObject playerBlue;
    public GameObject playerYellow;

    public GameObject playerRedIcon;
    public GameObject playerGreenIcon;
    public GameObject playerBlueIcon;
    public GameObject playerYellowIcon;
    
    void Awake()
    {
        switch (MenuManager.numberOfPlayer)
        {
            case 2:
                playerBlue.SetActive(false);
                playerBlueIcon.SetActive(false);
                playerYellow.SetActive(false);
                playerYellowIcon.SetActive(false);
                break;
            case 3:
                playerYellow.SetActive(false);
                playerYellowIcon.SetActive(false);
                break;
            case 4:
                break;
        }
    }

    void Update()
    {
        
    }
}
