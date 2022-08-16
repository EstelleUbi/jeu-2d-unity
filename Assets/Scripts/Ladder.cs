using UnityEngine;
using TMPro;

public class Ladder : MonoBehaviour
{

    public bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D topCollider;
    public TextMeshProUGUI interactUI;


    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }

        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerMovement.isClimbing = true;
                topCollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
        }
    }
}
