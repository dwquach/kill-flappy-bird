using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float upForce = 150f;
    public float bulletForce = 10f;
    public GameObject projectile;
    public GameObject enemySpawner;
    public AudioClip shootSound;

    private bool isDead = false;
    private Rigidbody2D rb;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false && GameController.instance.gameOver == false)
        {   
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                startGame();
                rb.velocity = Vector2.zero; // for consistent behavior
                rb.AddForce(new Vector2(0, upForce));

            } else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {
                startGame();
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(shootSound, vol);
                GameObject bullet = Instantiate(projectile, new Vector2(transform.position.x + 1, transform.position.y)
                    , Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-GameController.instance.scrollSpeed + bulletForce, 0);
            }
        }

        if (GameController.instance.gameOver)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }

        if (GameController.instance.gameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || 
                Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}

    private void KillPlayer()
    {
        isDead = true;
        GameController.instance.PlayerDied();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameController.instance.gameOver == false)
        {
            KillPlayer();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("New Level") && GameController.instance.gameOver == false)
        {
            KillPlayer();
        }
    }

    private void startGame()
    {
        if (GameController.instance.gameStarted == false)
        {
            GameController.instance.gameStarted = true;
            GameController.instance.instructionText.SetActive(false);
            rb.gravityScale = 1.5f;
            Instantiate(enemySpawner, Vector2.zero, Quaternion.identity);
        }
    }
}
