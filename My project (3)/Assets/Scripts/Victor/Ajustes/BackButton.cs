using UnityEngine;
using UnityEngine.SceneManagement;
public class BackButton : MonoBehaviour
{
    public GameObject mainMenuPanel, pausePanel;
    public GameObject optionsPanel;
    public static int vueltaAjustes; //Valor que indica a donde volver despues de salir de los ajustes: 1 = MainMenu, 2 = PausaMenu
    public void GoBackAjustes()
    {
        if (vueltaAjustes == 1)
        {
            optionsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        
    }

}



