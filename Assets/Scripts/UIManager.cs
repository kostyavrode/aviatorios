using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text bestScoreText;
    public GameObject deadPanel;
    public Button shootButton;
    public GameObject inGameJoystick;
    public GameObject inGameUI;
    public static UIManager instance;
    public TMP_Text scoreBar;
    public Slider slider;
    private float spendedTime;
    private int score;
    private int bestScore=0;
    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        instance = this;
        inGameUI.SetActive(true);
        inGameJoystick.SetActive(true);
        shootButton.onClick.AddListener(AirplaneShoot.instance.Shoot);
    }
    public void FixedUpdate()
    {
        if (AirplaneController.instance.GetIsCanFly())
        {
            slider.value = AirplaneController.instance.GetFuel() / 100f;
            spendedTime += Time.deltaTime;
            if (spendedTime>1)
            {
                scoreBar.text = "Score: " + score++;
                spendedTime = 0;
            }
        }
        else
        {
            audioSource.mute = true;
        }
        if (!AirplaneController.instance.GetIsAlive())
        {
            CheckBestScore();
            StartCoroutine(DeadCoroutine());
        }
    }
    private void CheckBestScore()
    {
        if (score>bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    public void GetExtraScore(int extraScore)
    {
        score += extraScore;
        scoreBar.text = "Score: " + score;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        deadPanel.SetActive(true);
        bestScoreText.text = "Best Score: " + bestScore;
        shootButton.enabled = false;
        inGameJoystick.SetActive(false);
        inGameUI.SetActive(false);
    }
}
