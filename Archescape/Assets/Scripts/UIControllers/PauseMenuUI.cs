using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public IUIHandler uiHandler;

    [SerializeField]
    private GameObject pauseMenu;

    private void Start()
    {
        pauseMenu = GameObject.Find(UITagRepository.pauseMenu);
        pauseMenu.SetActive(false);
    }

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
