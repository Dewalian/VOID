using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnCD = 2f;
    [SerializeField] private Collider2D tankAntiSpawn;
    [SerializeField] private GameObject enemy;

    public float moveSpeed { get; private set; } = 3f;
    public int health { get; private set; } = 1;

    private Vector2 screen;
    private bool onCD = false;

    private void Start()
    {
        screen = new Vector2(transform.localScale.x, transform.localScale.y);
        screen = Camera.main.ScreenToWorldPoint(screen);
        tankAntiSpawn = GameObject.FindGameObjectWithTag("AntiSpawn").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!onCD)
        {
            CheckifOutsideAntiSpawn();
        }
    }

    private void CheckifOutsideAntiSpawn()
    {
        float x = Random.Range(-screen.x, screen.x)/2;
        float y = Random.Range(-screen.y, screen.y)/2;
        Vector2 spawnPosition = new Vector2(x, y);
        if (!tankAntiSpawn.OverlapPoint(spawnPosition) && transform.childCount <= 20)
        {

            StartCoroutine(SpawnEnemy(spawnPosition));
        }
    }

    private IEnumerator SpawnEnemy(Vector2 spawnPosition)
    {
        onCD = true;
        yield return new WaitForSeconds(spawnCD);
        GameObject enemies = Instantiate(enemy, spawnPosition, Quaternion.identity);
        enemies.transform.parent = gameObject.transform;
        onCD = false;
    }

    public void IncreaseSpawnRate()
    {
        spawnCD -= 0.1f;
    }

    //public void IncreaseEnemyHealth()
    //{
    //    if(GameManager.instance.level % 5 == 0)
    //    {
    //        health++;
    //    }
    //}

    public void IncreaseEnemySpeed()
    {
        if(GameManager.instance.level % 2 == 0)
        {
            moveSpeed += 1f;
        }
    }
}
