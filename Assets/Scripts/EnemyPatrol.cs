using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer spriteRenderer;
    public int damageOnCollision = 20;

    private Transform target; //�Point que l'ennemi doit atteindre
    private int destPoint = 0;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        // je prend la position de la cible moins la position de l'ennemi pour savoir combien il me reste � parcourir
        Vector3 direction = target.position - transform.position;
        // normalized => mettre le vecteur � 1, magnitude toujours � la m�me taille
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length; //despoint correspond � l'�l�ment vers lequel ont se dirige.
            target = waypoints[destPoint]; // destpoint = index du waypoint vers lequel on se dirige
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // M�thode propre � la collision en 2D , m�thode fournie par Unity
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
