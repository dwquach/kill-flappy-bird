using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

    public GameObject boss;
    public GameObject enemy;
    public float attackRate = 2f;
    public AudioClip hitSound;

    private float health = 100;
    private Animator anim;
    private Rigidbody2D rb;
    private Text healthText;
    private float timeSinceLastAttack = 0;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthText = GameController.instance.killGoalText;

        healthText.text = "Boss HP: " + health;

        rb.velocity = new Vector2(-3f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Die");
            GameController.instance.PlayerWon();
        }

        if (health > 0)
        {
            healthText.text = "Boss HP: " + health;
            timeSinceLastAttack += Time.deltaTime;

            if (timeSinceLastAttack > attackRate)
            {    
                float attack = Random.Range(0, 3);

                if (attack == 0)
                {
                    Charge();
                }
                else
                {
                    Summon();
                }

                timeSinceLastAttack = 0;
            }

            SetMovement();
        }
    }

    private void SetMovement()
    {
        float horizontal = rb.velocity.x;

        if (transform.position.x > 10)
        {
            horizontal = -3f;
        } else if (transform.position.x < -7)
        {
            horizontal = 7f;
        }

        if (transform.position.y >= 2.8)
        {
            rb.velocity = new Vector2(horizontal, -1.5f);
          
        } 
        else if (transform.position.y <= -2.5)
        {
            rb.velocity = new Vector2(horizontal, 1.5f);
       
        } else
        {
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
        }   

         
    }

    private void Charge()
    {
        rb.velocity = new Vector2(-7f, rb.velocity.y);
    }

    private void Summon()
    {
        int formation = Random.Range(0, 3);
        if (formation == 0)
        {
            PyramidSummon();
        }
        else if (formation == 1)
        {
            TenSpray();
        }
        else
        {
            VFormation();
        }
    }

    private void PyramidSummon()
    {
        float maxHeight = 2.5f;
        float minHeight = -2.5f;
        GameObject minion;

        for (float i = maxHeight; i > minHeight; i--)
        {
            minion = Instantiate(enemy, new Vector2(17, i), Quaternion.identity);
            minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, 0);
        }

        for (float i = maxHeight-1; i > minHeight+1; i--)
        {
            minion = Instantiate(enemy, new Vector2(19, i), Quaternion.identity);
            minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, 0);
        }

        minion = Instantiate(enemy, new Vector2(21, maxHeight-2), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, 0);
    }

    private void TenSpray()
    {
        float heightMax = 2.8f;
        float heightMin = -heightMax;
        float spawnXPosition = 19f;

        for (int i = 0; i < 10; ++i)
        {
            float spawnYPosition = Random.Range(heightMin, heightMax);
            GameObject minion = Instantiate(enemy, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity);
            minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            spawnXPosition += 0.5f;
        }
    }

    private void VFormation()
    {
        float maxHeight = 2.5f;
        float minHeight = -2.5f;
        float horizontal = 19f;
        GameObject minion;

        minion = Instantiate(enemy, new Vector2(horizontal, maxHeight - 2), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 0);

        horizontal += 1f;

        minion = Instantiate(enemy, new Vector2(horizontal, maxHeight - 1), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 0);

        minion = Instantiate(enemy, new Vector2(horizontal, minHeight + 2f), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 0);

        horizontal += 1f;

        minion = Instantiate(enemy, new Vector2(horizontal, maxHeight), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 0);

        minion = Instantiate(enemy, new Vector2(horizontal, minHeight + 1f), Quaternion.identity);
        minion.GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            float vol = Random.Range(volLowRange, volHighRange);
            GameController.instance.hitSource.PlayOneShot(hitSound, vol);
            if (health > 0)
            {
                health -= 2;
            }
            healthText.text = "Boss HP: " + health;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
