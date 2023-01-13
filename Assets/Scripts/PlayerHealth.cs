using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public SpriteRenderer graphics;

    public float invincibilityTimeAfterHit = 3f;
    public bool isInvincible = false;
    public float invincibilityFlashDelay = 0.15f;

    public static PlayerHealth instance;

    // Ce système permet de créer une seule instance de playerHealth et de le rendre accessible partout (depuis toutes les autres classes)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(40);
        }
    }

    public void TakeHealth(int health)
    {
        if((currentHealth + health) > maxHealth)
        {
            currentHealth = maxHealth;
        } 
        else
        {
            currentHealth += health;  
        }

        healthBar.SetHealth(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // vérification si le joueur est toujours vivant
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());

        }
    }

    void Die()
    {
        Debug.Log("Le joueur est éliminé");
        // Bloquer les mouvements du personnage
        PlayerMovement.instance.enabled = false;

        // Jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("die");

        // Empecher les interaction physique avec les autres éléments de la scène
        PlayerMovement.instance.body.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.body.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;

        // Appel du menu game over
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.body.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }


    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
