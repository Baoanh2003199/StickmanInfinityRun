using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text score;
    public Text highScore;
    public Text newRecord;
    public int scoreint;

    // Start is called before the first frame update
    void Start()
    {

        scoreint = (int)FindObjectOfType<GameManager>().score;
        score.text = scoreint.ToString();
        int highscoreint = (int)FindObjectOfType<GameManager>().highScore;
        highScore.text = highScore.ToString();
  
    }

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
        FindObjectOfType<GameManager>().btnPause.gameObject.SetActive(true);
    }
    

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        scoreint = (int)FindObjectOfType<GameManager>().score;
        score.text = scoreint.ToString();
        highScore.text = ((int)FindObjectOfType<GameManager>().highScore).ToString();
    }

    private void OnEnable()
    {
        FindObjectOfType<GameManager>().btnPause.gameObject.SetActive(false);
        ScoreBoardUpdate();
    }
    private void ScoreBoardUpdate()
    {
        if ((int)FindObjectOfType<GameManager>().score > PlayerPrefs.GetInt("scoreBoard_0"))
        {
            for (int i = 5; i > 0; i--)
            {
                int newScore = PlayerPrefs.GetInt("scoreBoard_"+(i-1));
                PlayerPrefs.SetInt("scoreBoard_" + i, newScore);
            }
            newRecord.color = new Color(133f / 255.0f, 255f / 255.0f, 78f / 255.0f);
            score.color = new Color(133f / 255.0f, 255f / 255.0f, 78f / 255.0f);
            PlayerPrefs.SetInt("scoreBoard_0", (int)FindObjectOfType<GameManager>().score);
            newRecord.text = "Congratulations it’s a new high score !";
        }
        else
        {
            score.color = Color.white;
            newRecord.text = "";
        }

    }
}
