using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            if (GameController.instance.killGoal > 0)
            {
                GameController.instance.spawnedEnemies--;
            }
            
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Bullet Catcher") || other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
