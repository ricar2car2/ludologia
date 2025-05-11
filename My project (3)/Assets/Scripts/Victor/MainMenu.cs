using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Cinematica");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego se cerró.");
    }
}