using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public static int numberOfPlayer = 4;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            numberOfPlayer = 2;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            numberOfPlayer = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            numberOfPlayer = 4;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
