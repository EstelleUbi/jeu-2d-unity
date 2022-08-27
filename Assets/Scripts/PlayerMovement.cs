using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float climbSpeed;
    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;
    public float jumpForce;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    public CapsuleCollider2D playerCollider;

    public Rigidbody2D body;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovement instance;

    // Ce système permet de créer une seule instance de playerHealth et de le rendre accessible partout (depuis toutes les autres classes)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        // Calcul du mouvement (direction, temps, vitesse)
        horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetKeyDown("space") && isGrounded)
        {
            Debug.Log("jump");
            isJumping = true;
        }

        Flip(body.velocity.x);

        // Envoyer la vitesse à l'animator. Problème : le déplacement à gauche génère un float négatif.
        float characterVelocity = Mathf.Abs(body.velocity.x); // renvoie toujour une valeur positive 
        animator.SetFloat("speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    private void FixedUpdate()
    {
        // Création d'une zone, d'une boite de collision entre les deux éléments et si la boite est en contact avec quelque chose alors cela renvoie true.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        // Déplacement
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, body.velocity.y);
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, .05f);
            if (isJumping)
            {
                body.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        } 
        else
        {
            // déplacement vertical
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, .05f);
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

    // Création d'une zone de détection des objets/sols
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
