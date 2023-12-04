using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    public GameObject scoreInput;
    public GameObject scoreDisplay;
    public Text initialsText;
    public Text scoresText;
    private int newScore;
    private string newName;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;             // disable cursor lock mode

        newScore = Readout.score;
        if (newScore <= PlayerPrefs.GetInt("score5"))
        {
            scoreInput.SetActive(false);
            displayScores();
        }

        PlayerPrefs.SetInt("lives", 5);
        PlayerPrefs.SetInt("highscore", 0);
    }

    public void storeScore()
    {
        if (initialsText.text.Length >= 1 && initialsText.text.Length <= 3)
        {
            newName = initialsText.text.ToString();
            updateScores(newScore, newName);
            scoreInput.SetActive(false);
            displayScores();
        }
    }

    void updateScores(int newScore, string newName)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (newScore == 0)
            {
                break;
            }
            else if (!PlayerPrefs.HasKey("score" + i) & !PlayerPrefs.HasKey("name" + i))
            {
                PlayerPrefs.SetString("name" + i, newName);
                PlayerPrefs.SetInt("score" + i, newScore);
                break;
            }
            else if (newScore > PlayerPrefs.GetInt("score" + i))
            {
                int temp = PlayerPrefs.GetInt("score" + i);
                PlayerPrefs.SetInt("score" + i, newScore);
                newScore = temp;

                string sTemp = PlayerPrefs.GetString("name" + i);
                PlayerPrefs.SetString("name" + i, newName);
                newName = sTemp;
            }
            else if (newScore == PlayerPrefs.GetInt("score" + i))
            {
                int temp = PlayerPrefs.GetInt("score" + (i + 1));
                PlayerPrefs.SetInt("score" + (i + 1), newScore);
                newScore = temp;

                string sTemp = PlayerPrefs.GetString("name" + (i + 1));
                PlayerPrefs.SetString("name" + (i + 1), newName);
                newName = sTemp;
            }
        }
    }

    void displayScores()
    {
        string display = "";
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.HasKey("score" + i) && PlayerPrefs.HasKey("name" + i))
            {
                display += i + ".  " + string.Format("{0,-10} {1,-12}", PlayerPrefs.GetString("name" + i).ToString(), PlayerPrefs.GetInt("score" + i).ToString());
            }
            display += "\n";
        }
        scoresText.text = display;
        scoreDisplay.SetActive(true);
    }

    public void replayGame()
    {
        //Readout.score = 0;
        SceneManager.LoadScene("Level1");

        if (PlayerPrefs.HasKey("highscore"))
            PlayerPrefs.SetInt("highscore", 0);
        if (PlayerPrefs.HasKey("lives"))
            PlayerPrefs.SetInt("lives", 5);
    }

    public void exitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
