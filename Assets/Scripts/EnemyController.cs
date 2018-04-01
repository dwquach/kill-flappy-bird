using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public AudioClip dyingSound;

    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Projectile"))
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                float vol = Random.Range(volLowRange, volHighRange);
                GameController.instance.hitSource.PlayOneShot(dyingSound, vol);
                GameController.instance.killGoal--;
            }
            Destroy(gameObject);
            GameController.instance.spawnedEnemies--;
        } else if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
