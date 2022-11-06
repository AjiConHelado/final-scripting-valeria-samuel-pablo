using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocidadMovimiento;

    [SerializeField] public Vector2 direccion;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    

    private Rigidbody2D rb2D;

    private float movimientoX, movimientoY;

    private Animator animator;

    public int maxHealth = 5;

    int currentHealth;

    public int Health
    {
        get { return currentHealth; }
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Character is dead GAME OVER");
        }
    }

    private void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");

        if (direccion.x != 0)
        {
            lastHorizontalVector = direccion.x;
        }

        if (direccion.y != 0)
        {
            lastVerticalVector = direccion.y;
        }
        
        

        animator.SetFloat("MovimientoX", movimientoX);
        animator.SetFloat("MovimientoY",movimientoY);

        direccion = new Vector2(movimientoX, movimientoY).normalized;


    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direccion * (velocidadMovimiento * Time.fixedDeltaTime));
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}

