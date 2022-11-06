using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    Vector2 dir = Vector2.zero;
    public float speed = 5;
    Input inputs;
    public int points = 0;
    public int hp = 3;
    public Text pointsTxt;
    public Text lifesTxt;

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Awake()
    {
        inputs = new Input();
        inputs.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += ctx => dir = Vector2.zero ;
    }

    void Start()
    {
        pointsTxt.text = "Points " + points.ToString();
        lifesTxt.text = "Lifes " + hp.ToString();
    }

    void Update()
    {
        rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.y * speed);
    }

    void TakeDamage()
    {
        hp--;
        lifesTxt.text = "Lifes " + hp.ToString();
        if(hp <= 0)
        {
            Debug.Log("gameover");
        }
    }

    void AddPoints()
    {
        points++;
        pointsTxt.text = "Points " + points.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            TakeDamage();
        }else if (other.gameObject.CompareTag("Fruit"))
        {
            AddPoints();
        }
    }
}
