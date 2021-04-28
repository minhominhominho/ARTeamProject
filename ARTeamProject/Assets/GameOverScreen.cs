 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public Text pointText; //Á»ºñ Á×ÀÎ ¼ö
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointText.text = score.ToString() + " POINTS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("UI");
    }
}
