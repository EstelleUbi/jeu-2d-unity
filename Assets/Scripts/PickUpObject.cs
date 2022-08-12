using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (transform.tag)
            {
                case "Coin":
                    Inventory.instance.AddCoins(1);
                    break;
                case "Heart":
                    PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                    playerHealth.TakeHealth(10);
                    break;

            }
            
            Destroy(gameObject);
        }
    }
}
