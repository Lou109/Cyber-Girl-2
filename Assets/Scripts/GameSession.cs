using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;
    [SerializeField] int hacksToOpenDoor = 3;
    LevelExit levelExit;
   


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {

        livesText.text = playerLives.ToString();
        scoreText.text = "Hacks To Open Door: " + score + "/" + hacksToOpenDoor;
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
        
        if (score == hacksToOpenDoor)
        {
            ResetPickupScore();
        }   
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
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void ResetPickupScore()
    {
        score = 0;
        scoreText.text = "Hacks To Open Door: " + score + "/" + hacksToOpenDoor;
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(+1);
        Destroy(gameObject);
    }
}
