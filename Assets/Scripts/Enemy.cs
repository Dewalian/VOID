using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform tank;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource src;

    private EnemySpawner enemySpawner;
    private int health;
    private float moveSpeed;

    private Vector2 direction;

    private void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemySpawner = GetComponentInParent<EnemySpawner>();
        animator = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
        health = enemySpawner.health;
        moveSpeed = enemySpawner.moveSpeed;
    }

    private void Update()
    {
        if(health > 0)
        {
            EnemyFollow();
        }
    }

    private void EnemyFollow()
    {
        transform.position = Vector2.MoveTowards(transform.position, tank.position, moveSpeed * Time.deltaTime);
        direction = (tank.position - transform.position).normalized;
        transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            EnemyDamaged();
        }
        else if (collision.collider.CompareTag("Player") && GameManager.instance.gameState)
        {
            health -= 10;
            EnemyDamaged();
        }
        GameManager.instance.IncreaseEXP();
    }



    private void EnemyDamaged()
    {
        health--;
        if(health <= 0)
        {
            src.Play();
            moveSpeed = 0;
            animator.SetBool("enemyDeath", true);
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
