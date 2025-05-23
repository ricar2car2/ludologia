using UnityEngine;
using UnityEngine.SceneManagement;
public class SeleccionMundo : MonoBehaviour
{
    public GameObject fantasiaPanel, futuristaPanel, skatePanel;
    public GameObject seleccionMundoPanel, inicioPanel, pausePanel, botonesPanel;
    
    public void MFantasia()
    {
        MenuPausa.vueltaPausa = 2;
        fantasiaPanel.SetActive(true);
        botonesPanel.SetActive(true);
        seleccionMundoPanel.SetActive(false);
    }

    public void MFuturista()
    {
        MenuPausa.vueltaPausa = 3;
        futuristaPanel.SetActive(true);
        botonesPanel.SetActive(true);
        seleccionMundoPanel.SetActive(false);
    }
    public void MSkate()
    {
        MenuPausa.vueltaPausa = 4;
        skatePanel.SetActive(true);
        botonesPanel.SetActive(true);
        seleccionMundoPanel.SetActive(false); ;
    }
    public void Pausa()
    {
        pausePanel.SetActive(true);
        seleccionMundoPanel.SetActive(false); ;
    }
    public void volverInicio()
    {
        inicioPanel.SetActive(true);
        seleccionMundoPanel.SetActive(false); ;
    }


}