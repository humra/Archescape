using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public IUIHandler uiHandler;
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(UIKeybindRepository.pauseMenu))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void QuitGame()
    {
        uiHandler.QuitGame();
    }
}
