using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public int playerScore;
    public int opponentScore;
    public TMP_Text scoreTextPlayer;
    public TMP_Text scoreTextOpponent;
    public GameObject menu;
    public GameObject difficulty;
    public GameObject endScreen;
    private LogicManagerScript logic;
    public GameObject opponent; 

    public Action onStartGame;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();

    }

    //Adding score to text field on Game UI
    public void addScorePlayer(int scoreToAdd = 1)
    {
        playerScore += scoreToAdd;
        scoreTextPlayer.text = playerScore.ToString();

    }

    public void addScoreOpponent(int scoreToAdd = 1)
    {
        opponentScore += scoreToAdd;
        scoreTextOpponent.text = opponentScore.ToString();

    }

    //Removes menu screen and moves onto next screen, which is difficulty screen, could potentially implement state machine instead
    public void OnStartButtonClicked()
    {
        menu.SetActive(false);
        difficulty.SetActive(true);
    }

    //easy difficulty desicion, resumes game and lowers opponent movement speed
    public void OnEasyButtonClicked()
    {
        difficulty.SetActive(false);
        Time.timeScale = 1;
        onStartGame?.Invoke();
        Debug.Log("Easy");
        logic.moveSpeed = 5f;
    }

    //Medium difficulty desicion, resumes game and increases opponent movement speed and increasing paddle height
    public void OnMediumButtonClicked()
    {
        difficulty.SetActive(false);
        Time.timeScale = 1;
        onStartGame?.Invoke();
        Debug.Log("Medium");
        logic.moveSpeed = 15f;
        opponent.transform.localScale = new Vector3(1, 2, 1);
    }

    //Hard difficulty desicion, resumes game and increases opponent movement speed, increasing paddle height & decreases deadzone
    public void OnHardButtonClicked()
    {
        difficulty.SetActive(false);
        Time.timeScale = 1;
        onStartGame?.Invoke();
        Debug.Log("Hard");
        logic.moveSpeed = 15f;
        logic.aiDeadzone = 0.5f;
        opponent.transform.localScale = new Vector3(1, 2, 1);
    }


    //reloads game to initial state
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        endScreen.SetActive(false);
        menu.SetActive(true);
    }
}
