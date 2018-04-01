using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject gameOverText;
    public GameObject winText;
    public GameObject instructionText;
    public Button restartButton;
    public static GameController instance;
    public bool gameOver = false;
    public bool gameStarted = false;
    public float scrollSpeed = -1.5f;
    public float spawnedEnemies = 0f;
    public float killGoal = 50;
    public Text killGoalText;
    public AudioSource hitSource;
    public AudioClip bgMusic;


    // Use this for initialization
    void Awake () {
		if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
	}

    private void Start()
    {
        hitSource.GetComponent<AudioSource>();
        hitSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (killGoal > 0)
        {
            killGoalText.text = "KILL " + killGoal.ToString() + " BIRDS";
        }
    }

    public void PlayerDied()
    {
        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOver = true;
    }

    public void PlayerWon()
    {
        winText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOver = true;
    }
}
