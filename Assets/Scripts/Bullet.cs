using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 10f;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        rb.velocity = transform.right * -1 * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            bulletSpeed = 0;
            animator.SetBool("Hit", true);
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
