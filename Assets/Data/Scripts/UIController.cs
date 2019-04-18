using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text scoreText;
    public Text bestScoreText;
    public Text roundTimerText;
    public Text scoreWelldoneText;
    public Text scoreUndercookText;
    public Text scoreOvercookText;
    public Text scoreBurnedText;
    public Text bestScoreResultText;

    public Canvas gameOverCanvas;
    public Text scoreGameOverText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetScoreText();
        SetRoundTimerText();
        GameManager.Instance.SetBestScore();

        if (GameManager.Instance.isRoundEnd == true)
        {
            gameOverCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (GameManager.Instance.isRoundEnd == false)
        {
            gameOverCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score = " + GameManager.Instance.score.ToString();
        bestScoreText.text = "Best Score = " + GameManager.Instance.bestScore.ToString();
        scoreGameOverText.text = "Score = " + GameManager.Instance.score.ToString();

        scoreBurnedText.text = GameManager.Instance.scoreBurned.ToString();
        scoreOvercookText.text = GameManager.Instance.scoreOvercook.ToString();
        scoreUndercookText.text = GameManager.Instance.scoreUndercook.ToString();
        scoreWelldoneText.text = GameManager.Instance.scoreWelldone.ToString();
        bestScoreResultText.text = "Best Score = " + GameManager.Instance.bestScore.ToString();
    }
    void SetRoundTimerText()
    {
        roundTimerText.text = ((int)GameManager.Instance.roundTimer).ToString();
    }

    public void RestartButton()
    {
        GameManager.Instance.SetBestScore();
        GameManager.Instance.SetDefaultParameter();
    }
}
