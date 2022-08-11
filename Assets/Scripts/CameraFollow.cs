using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    //Permet de décaler la récaction de la caméra par rapport au mouvement du perso.
    public float timeOffset;
    // position de la camera en -10 pour être derrière le décor
    public Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        // transform correspond à mon objet
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        
    }
}
