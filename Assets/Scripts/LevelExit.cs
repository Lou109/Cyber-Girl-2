using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] int numberofhacksneeded = 3;
    GameSession gameSession;
    [SerializeField] Animator animator = null;
    [SerializeField] string actionParameter = null;
   
    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();    
    }
 
    public void OnTriggerEnter2D(Collider2D other)
    {
        int getTheScore = gameSession.GetScore();

        if (other.tag == "Player" && getTheScore == numberofhacksneeded)
        {
            animator.SetTrigger(actionParameter);
            StartCoroutine(LoadNextLevel());
        }

        if (other.tag == "Player" && getTheScore <= numberofhacksneeded)
        {
            return;
        }
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
       
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
        FindObjectOfType<GameSession>().ResetPickupScore();

    }
}
