using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public GameObject pausePanel, inicioPanel, mundosPanel, ajustesPanel, mFantasiaPanel, mFuturistaPanel, mSkatePanel;
    public GameObject panelBotones;
    public static int vueltaPausa; //Valor que indica a donde volver despues de salir de la pausa: 1 = MenuMundos, 2 = MFantasía, 3 = MFuturista, 4 = MSkate

    public void ShowPause()
    {
    
        panelBotones.SetActive(false);
        pausePanel.SetActive(true);

        if (vueltaPausa == 1)
        {
            mundosPanel.SetActive(false);           
        }
        else if (vueltaPausa == 2)
        {
            mFantasiaPanel.SetActive(false);        
        }
        else if (vueltaPausa == 3)
        {
            mFuturistaPanel.SetActive(false);   
        }
        else
        {
            mSkatePanel.SetActive(false); 
        }
    }
    public void Continue()
    {
        
        pausePanel.SetActive(false);
        

        if (vueltaPausa == 1)
        {
            mundosPanel.SetActive(true);
        }
        else if (vueltaPausa == 2)
        {
            mFantasiaPanel.SetActive(true);
            panelBotones.SetActive(true);
        }
        else if (vueltaPausa == 3)
        {
            mFuturistaPanel.SetActive(true);
            panelBotones.SetActive(true);
        }
        else
        {
            mSkatePanel.SetActive(true);
            panelBotones.SetActive(true);
        }
    }
    public void SeleccionMundo()
    {
        vueltaPausa = 1;
        pausePanel.SetActive(false);
        mundosPanel.SetActive(true);
    }
    public void Settings()
    {
        BackButton.vueltaAjustes = 2;
        pausePanel.SetActive(false);
        ajustesPanel.SetActive(true);

    }
    public void MenuInicio()
    {
        pausePanel.SetActive(false);
        inicioPanel.SetActive(true);
    }

    

}