using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tank : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject nozzleTip;

    [SerializeField] private Animator nozzleAnimator;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private int health = 1;
    [SerializeField] private AudioSource src;
    private float xDir;
    private float yDir;
    private Vector2 moveDir;

    private Vector2 worldPosition;
    private Vector2 direction;
    private Vector2 screen;

    [SerializeField] private float shootCD = 1f;
    private bool onCD = false;

    [SerializeField]private TMP_Text textHealth;

    private void Start()
    {
        screen = new Vector2(Screen.width, Screen.height);
        screen = Camera.main.ScreenToWorldPoint(screen);
        textHealth.text = health.ToString();
    }

    private void Update()
    {
        moveDir = MoveDirection();
        RotateHead();
        if (Input.GetMouseButton(0) && !onCD)
        {
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    private Vector2 MoveDirection()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        return new Vector2(xDir, yDir).normalized;
    }

    private void RotateHead()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)head.transform.position).normalized;
        head.transform.right = direction * -1;
    }

    private IEnumerator Shoot()
    {
        onCD = true;
        nozzleAnimator.SetTrigger("Shot");
        src.Play();
        Instantiate(bullet, nozzleTip.transform.position, nozzleTip.transform.rotation);
        yield return new WaitForSeconds(shootCD);
        onCD = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TankDamaged();
        }
    }

    private void TankDamaged()
    {
        health--;
        textHealth.text = health.ToString();
        if (health <= 0)
        {
            textHealth.text = 0.ToString();
            Debug.Log("Die");
            GameManager.instance.gameState = false;
            enabled = false;
        }
    }

    public void DecreaseShootCD()
    {
        if(GameManager.instance.level % 2 == 0)
        {
            shootCD -= 0.1f;
        }
    }

    public void IncreaseMoveSpeed()
    {
        if(GameManager.instance.level % 2 == 0)
        {
            moveSpeed++;
        }
    }

    public void IncreaseHealth()
    {
        health++;
        textHealth.text = health.ToString();
    }
}
