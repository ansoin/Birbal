using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI rivalScore1;
    [SerializeField] TextMeshProUGUI rivalScore2;
    [SerializeField] TextMeshProUGUI rivalScore3;

    private string playerText = "Player: ";
    private string rival1Text = "Blue: ";
    private string rival2Text = "Red: ";
    private string rival3Text = "Pink: ";

    public void UpdateScore(string birbalID, int score)
    {
        // Check which score to update
        switch (birbalID)
        {
            case "Player":
                playerScore.text = playerText + score;
                Debug.Log("Score updated, in theory. " + playerText + score);
                break;
            case "Rival 1":
                rivalScore1.text = rival1Text + score;
                Debug.Log("Score updated, in theory. " + rival1Text + score);
                break;
            case "Rival 2":
                rivalScore2.text = rival2Text + score;
                Debug.Log("Score updated, in theory. " + rival2Text + score);
                break;
            case "Rival 3":
                rivalScore3.text = rival3Text + score;
                Debug.Log("Score updated, in theory. " + rival3Text + score);
                break;
        }
        // 
    }
}
