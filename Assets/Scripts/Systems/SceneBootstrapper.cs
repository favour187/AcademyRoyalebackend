using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void EnsureFirstScene()
    {
        BootstrapScene(SceneManager.GetActiveScene().name);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BootstrapScene(scene.name);
    }

    private static void BootstrapScene(string sceneName)
    {
        Debug.Log("Bootstrapping Scene: " + sceneName);

        if (sceneName == "SplashScreen" && Object.FindObjectOfType<SplashController>() == null)
        {
            GameObject go = new GameObject("[SplashController]");
            go.AddComponent<SplashController>();
        }

        if (sceneName == "MainMenu" && Object.FindObjectOfType<MainMenuController>() == null)
        {
            GameObject go = new GameObject("[MainMenuController]");
            go.AddComponent<MainMenuController>();
        }

        if (sceneName == "CharacterSelect" && Object.FindObjectOfType<CharacterSelectController>() == null)
        {
            GameObject go = new GameObject("[CharacterSelectController]");
            go.AddComponent<CharacterSelectController>();
        }

        if (sceneName == "LobbyScene" && Object.FindObjectOfType<LobbyController>() == null)
        {
            GameObject go = new GameObject("[LobbyController]");
            go.AddComponent<LobbyController>();
        }

        if (sceneName == "GameScene" && Object.FindObjectOfType<GameSceneController>() == null)
        {
            GameObject go = new GameObject("[GameSceneController]");
            go.AddComponent<GameSceneController>();
        }

        if (sceneName == "GameOver" && Object.FindObjectOfType<GameOverController>() == null)
        {
            GameObject go = new GameObject("[GameOverController]");
            go.AddComponent<GameOverController>();
        }
    }
}


