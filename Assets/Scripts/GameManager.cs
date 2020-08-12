using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Player player;
    private Vector3 playerStartPoint;

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public Text text;
    // Start is called before the first frame update
    public float score;
    public float pointIncreasePerSecond;

    public DeathMenu deathMenu;

    private PlatformDestruct[] platformList;
    public GameObject countDown;
    private Text cdText;
    public int[] scoreBoard;
    public int highScore;
    public Button btnPause;


    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        cdText = countDown.GetComponent<Text>();
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
        score = 0.0f;
        scoreBoard = new int[5] { 0, 0, 0, 0, 0 };
        pointIncreasePerSecond = 5.0f;
        for(int i = 0; i < 5; i ++)
        {
            if (!PlayerPrefs.HasKey("scoreBoard_" + i))
                PlayerPrefs.SetInt("scoreBoard_" + i, 0);
        }
        StartCoroutine("getReady");
        

    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isDead)
        {
            text.text = "Score: " + (int)score;
            score += pointIncreasePerSecond * Time.deltaTime;
        }
        cdText = countDown.GetComponent<Text>();
        for(int i = 0; i < 5; i ++)
        {
            scoreBoard[i] = PlayerPrefs.GetInt("scoreBoard_" + i);
        }
        highScore = scoreBoard[0];

    }

    public void RestartGame()
    {
      
        StartCoroutine("WaitCo");
   
        //StartCoroutine("RestartGameCo");
    }

    public IEnumerator WaitCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        deathMenu.gameObject.SetActive(true);
    }

    public void Reset()
    {
        deathMenu.gameObject.SetActive(false);
        score = 0.0f;
        platformList = FindObjectsOfType<PlatformDestruct>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        Destroy(player.clone);
        player.col.size = player.colSize;
        player.col.offset = player.colOffset;
        player.gameObject.SetActive(true);
        player.isDead = false;
    }

    public void Resume()
    {
           StartCoroutine("getReady");
    }

    IEnumerator getReady()
    {
        countDown.SetActive(true);
        btnPause.gameObject.SetActive(false);
        cdText.text = "3";
        yield return StartCoroutine(WaitForRealSeconds(0.5f));

        cdText.text = "2";
        yield return StartCoroutine(WaitForRealSeconds(0.5f));

        cdText.text = "1";
        yield return StartCoroutine(WaitForRealSeconds(0.5f));

        cdText.text = "GO !";
        yield return StartCoroutine(WaitForRealSeconds(0.5f));

        cdText.text = "";
        countDown.SetActive(false);
        
        Time.timeScale = 1;
        btnPause.gameObject.SetActive(true);

    }

    IEnumerator WaitForRealSeconds(float waitTime)
    {
        float endTime = Time.realtimeSinceStartup + waitTime;

        while (Time.realtimeSinceStartup < endTime)
        {
            yield return null;
        }
    }

    //public IEnumerator RestartGameCo()
    //{
    //    player.gameObject.SetActive(false);
    //    score = 0.0f;
    //    yield return new WaitForSeconds(3f);
    //    platformList = FindObjectsOfType<PlatformDestruct>();
    //    for(int i = 0; i<platformList.Length;i++)
    //    {
    //        platformList[i].gameObject.SetActive(false);
    //    }
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    player.transform.position = playerStartPoint;
    //    platformGenerator.position = platformStartPoint;
    //    Destroy(player.clone);
    //    player.gameObject.SetActive(true);
    //    player.isDead = false;
    //}

}
