using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnEnable()
    {
        //FindObjectOfType<GameManager>().btnPause.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().Reset();
    }


    public void ResumeGame()
    {
      
        if (GameObject.FindGameObjectWithTag("Ragdoll") == null)
        {
            FindObjectOfType<GameManager>().Resume();
            pauseMenu.SetActive(false);
        }
        else
        {

            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
