using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl  1"); // exact scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // works in build, not editor
    }
}
