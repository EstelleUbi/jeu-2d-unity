using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public int coinsPickedUpInThisScene;

    public static CurrentSceneManager instance;

    // Ce syst�me permet de cr�er une seule instance de CurrentSceneManager et de le rendre accessible partout (depuis toutes les autres classes)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la sc�ne");
            return;
        }

        instance = this;
    }
}
