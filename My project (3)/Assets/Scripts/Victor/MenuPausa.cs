using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject settingsPanel;

    void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ShowPause()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);

    }
    public void Continue()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

    }
    public void SeleccionMundo()
    {
        SceneManager.LoadScene("SeleccionMundo");
    }
    public void Settings()
    {
       
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);

    }
    public void MenuInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    

}