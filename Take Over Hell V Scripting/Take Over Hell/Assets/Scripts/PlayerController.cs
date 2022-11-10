using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento;

    [SerializeField] public Vector2 direccion;

    [HideInInspector] public float lastHorizontalVector, lastVerticalVector;
    
    public float lastVectorForAnim { get; private set; }
    
    private Rigidbody2D rb2D;
    private float movimientoX, movimientoY;
    private Animator animator;

    private void Awake()
    {
        lastVectorForAnim = 1;
        lastHorizontalVector = 1;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");

        if (direccion.x != 0)
        {
            lastVerticalVector = 0;
            lastHorizontalVector = direccion.x;
            lastVectorForAnim = direccion.x;
        }

        if (direccion.y != 0)
        {
            lastHorizontalVector = 0;
            lastVerticalVector = direccion.y;
        }
        
        if (movimientoX != 0 || movimientoY != 0)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Moviendose", true);
            
            if(movimientoX != 0)
            {
                animator.SetFloat("MovimientoX", movimientoX);
            }
            else if (movimientoY != 0)
            {
                animator.SetFloat("MovimientoX", lastVectorForAnim);
            }    
        }
        
        if (movimientoX == 0 && movimientoY == 0)
        {
            animator.SetBool("Idle", true);
            animator.SetFloat("MovimientoX", 0);
            animator.SetBool("Moviendose", false);
        }
        
        direccion = new Vector2(movimientoX, movimientoY).normalized;
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direccion * (velocidadMovimiento * Time.fixedDeltaTime));
    }
}

