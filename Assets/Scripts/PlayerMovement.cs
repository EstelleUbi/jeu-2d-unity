using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private bool isJumping;
    private bool isGrounded;
    public float jumpForce;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public Rigidbody2D body;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;


    private void Update()
    {
        // Calcul du mouvement (direction, temps, vitesse)
        horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

        if (Input.GetKeyDown("space") && isGrounded)
        {
            Debug.Log("jump");
            isJumping = true;
        }

        Flip(body.velocity.x);

        // Envoyer la vitesse � l'animator. Probl�me : le d�placement � gauche g�n�re un float n�gatif.
        float characterVelocity = Mathf.Abs(body.velocity.x); // renvoie toujour une valeur positive 
        animator.SetFloat("speed", characterVelocity);
    }

    private void FixedUpdate()
    {
        // Cr�ation d'une zone, d'une boite de collision entre les deux �l�ments et si la boite est en contact avec quelque chose alors cela renvoie true.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        // D�placement
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, body.velocity.y);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping == true)
        {
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }

    }

    // Cr�ation d'une zone de d�tection des objets/sols
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
