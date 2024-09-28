using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int level { get; private set; } = 0;
    public int enemyKillQuota { get; private set; } = 3;
    public int exp { get; private set; } = 0;
    public bool gameState = true;

    private EnemySpawner enemySpawner;
    private Tank tank;

    private TMP_Text textLevel;
    private GameObject gameOver;
    private Canvas canvas;
    private AudioSource src;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        tank = FindObjectOfType<Tank>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        textLevel = GameObject.FindGameObjectWithTag("TextLevel").GetComponent<TMP_Text>();

        canvas = GameObject.FindGameObjectWithTag("Restart").GetComponent<Canvas>();

        src = GetComponent<AudioSource>();

        gameOver = GameObject.FindGameObjectWithTag("Fade");
        gameOver.SetActive(false);
       
    }

    private void Update()
    {
        if (!instance.gameState)
        {
            GameOver();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        textLevel = GameObject.FindGameObjectWithTag("TextLevel").GetComponent<TMP_Text>();
        level = 0;
        Cursor.visible = false;
        canvas = GameObject.FindGameObjectWithTag("Restart").GetComponent<Canvas>();
        canvas.sortingLayerName = "BG";
        gameOver = GameObject.FindGameObjectWithTag("Fade");
        gameOver.SetActive(false);
        src = GetComponent<AudioSource>();
    }

    public void IncreaseLevel()
    {
        if (instance.gameState)
        {
            level++;
            enemyKillQuota += 1;
            IncreaseDifficulty();
            textLevel.text = level.ToString();
        }
    }

    public void IncreaseDifficulty()
    {
        if (instance.gameState)
        {
            enemySpawner.IncreaseSpawnRate();
            //enemySpawner.IncreaseEnemyHealth();
            enemySpawner.IncreaseEnemySpeed();
            tank.DecreaseShootCD();
            tank.IncreaseMoveSpeed();
            tank.IncreaseHealth();
        }
    }

    public void IncreaseEXP()
    {
        exp++;
        if(exp >= enemyKillQuota)
        {
            IncreaseLevel();
            exp = 0;
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Debug.Log("Test");
        textLevel.color = Color.white;
        Cursor.visible = true;
        canvas.sortingLayerName = "GameOver";
        if (Input.GetMouseButton(0))
        {
            src.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            instance.gameState = true;
        }
    }
}
