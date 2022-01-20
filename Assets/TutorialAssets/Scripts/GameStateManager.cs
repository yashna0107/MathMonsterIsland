using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    private QuestionManager questionManager;
    // Start is called before the first frame update
    void Start()
    {
        questionManager = GetComponent<QuestionManager>();
        questionManager.OnGameWin += () =>
        {
            Invoke(nameof(RestartGame), 4f);
        };
    }


    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
