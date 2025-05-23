using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject cinematicaPanel, mundosPanel;

    public void StartGame()
    {
        MenuPausa.vueltaPausa = 1;
        if (Cinematica.siCinematica == false)
        {
            mainMenuPanel.SetActive(false);
            cinematicaPanel.SetActive(true);
        }
        else
        {
            mainMenuPanel.SetActive(false);
            mundosPanel.SetActive(true);
        }
        
    }
    public void ShowOptions()
    {
        BackButton.vueltaAjustes = 1;
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego se cerró.");
    }
    
}