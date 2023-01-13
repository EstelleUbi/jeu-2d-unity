using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    // Ce système permet de créer une seule instance de GameOverManager et de le rendre accessible partout (depuis toutes les autres classes)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoadd();
        }

        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisScene);
        // Rechargement de la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        // Retour au menu principal
    }

    public void QuitButton()
    {
        // fermer le jeu
        Application.Quit();
    }
}
