using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Image[]liveS;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;
    [SerializeField] int hacksToOpenDoor = 3;
    
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = "Hacks To Open Door: " + score + "/" + hacksToOpenDoor;
      
    }

   
    public void Update()
    {
        for (int i = 0; i < liveS.Length; i++)
        {
            if (i < playerLives)
            {
                liveS[i].color = Color.white;
            }
            else
            {
                liveS[i].color = Color.clear;
               
            } 
        }
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsForComputerPickup)
    {
        score += pointsForComputerPickup;
        scoreText.text = "Hacks To Open Door: " + score + "/" + hacksToOpenDoor;
    }

    public int GetScore()
    {
        return score;
    }

    void TakeLife()
    {
        playerLives --;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);     
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(4);
        Destroy(gameObject);
    }

     public void ResetPickupScore()
    {   
        score = 0;
        scoreText.text = "Hacks To Open Door: " + score + "/" + hacksToOpenDoor;
        Destroy(gameObject);
    }
}
