using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private void Start()
    {
        if(!PlayerPrefs.HasKey(SettingValues.language))
        {
            PlayerPrefs.SetString(SettingValues.language, "ENG");
        }
    }

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
