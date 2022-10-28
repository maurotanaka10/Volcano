using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float speedMultiplier = 1;
    public float spawnMultiplier = 1;

    #region startgame
    [Header("Reference StartGame")]
    [SerializeField] private GameObject parentGround;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject spawnMeteor;

    #endregion
    [Header("Points")]
    [SerializeField] private TMP_Text textCurrentPoints;
    private int currentPoints = 0;
    [SerializeField] private TMP_Text textHighScore;
    [SerializeField] private TMP_Text textFinalScore;
    [SerializeField] private GameObject panelGameOver;
    private bool gameIsOver;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        NextMap();
        textCurrentPoints.text = "" + currentPoints;

        if (gameIsOver)
            return;
    }

    public void IncreaseScore(int pointsToWin)
    {
        currentPoints += pointsToWin;
    }

    public void NextMap()
    {
        if (GameObject.FindGameObjectWithTag("Hero").transform.position.x >= 9.5f)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        //liga todos objetos da cena
        foreach (Transform child in parentGround.transform)
        {
            child.gameObject.SetActive(true);
        }
        wall.gameObject.SetActive(true);
        hero.GetComponent<Hero>().RestartPosition();
        wall.GetComponent<Wall>().RestoreWall();
        //COMECOU O JOGO
        speedMultiplier += 0.5f;
        spawnMultiplier -= 0.1f;
    }

    public void GameOver()
    {
        gameIsOver = true;
        panelGameOver.SetActive(true);
        spawnMeteor.SetActive(false);
        textFinalScore.text = "" + currentPoints;

        if (currentPoints > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentPoints);
        }

        textHighScore.text = "" + PlayerPrefs.GetInt("HighScore");
    }
}
