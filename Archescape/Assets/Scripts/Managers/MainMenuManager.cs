using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void StartNewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartNewGame(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
