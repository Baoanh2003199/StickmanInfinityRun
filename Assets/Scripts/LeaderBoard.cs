using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] txtScore;
    public Text[] txtRank;
    public GameObject leaderBoard;
    void Start()
    {
    }

    private void OnEnable()
    {
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        for(int i = 0; i < 5; i++)
        {
            if (!PlayerPrefs.HasKey("scoreBoard_" + i))
            {
                txtScore[i].text = "0";
            }
            else
            {
                txtScore[i].text = PlayerPrefs.GetInt("scoreBoard_" + i).ToString();
            }
        }
        txtScore[0].color = new Color(255f / 255f, 215f / 255f, 0f / 255f);
        txtRank[0].color = new Color(255f / 255f, 215f / 255f, 0f / 255f);
        txtScore[1].color = new Color(192f / 255f, 192f / 255f, 192f / 255f);
        txtRank[1].color = new Color(192f / 255f, 192f / 255f, 192f / 255f);
        txtScore[2].color = new Color(205f / 255f, 127f / 255f, 50f / 255f);
        txtRank[2].color = new Color(205f / 255f, 127f / 255f, 50f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
    }

    public void btnClose()
    {
        leaderBoard.SetActive(false);
    }
}
