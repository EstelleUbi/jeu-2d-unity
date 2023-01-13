using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    // Ce système permet de créer une seule instance de DontDestroyOnLoadScene et de le rendre accessible partout (depuis toutes les autres classes)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
            return;
        }

        instance = this;

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void RemoveFromDontDestroyOnLoadd()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }

}
