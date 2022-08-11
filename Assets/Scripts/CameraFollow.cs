using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    //Permet de d�caler la r�caction de la cam�ra par rapport au mouvement du perso.
    public float timeOffset;
    // position de la camera en -10 pour �tre derri�re le d�cor
    public Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        // transform correspond � mon objet
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        
    }
}
