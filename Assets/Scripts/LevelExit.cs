using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] int numberofhacksneeded = 3;
    [SerializeField] Animator animator = null;
    [SerializeField] string actionParameter = null;

    SceneLoader sceneLoader;  
    GameSession gameSession;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        int getTheScore = gameSession.GetScore();


        if (other.tag == "Player" && getTheScore == numberofhacksneeded)
        {
            animator.SetTrigger(actionParameter);
            Invoke("LoadNextLevel", levelLoadDelay);
        }
    }
        public void LoadNextLevel()
        {
        sceneLoader.LoadNextScene();
        gameSession.ResetPickupScore();
        }
    }
