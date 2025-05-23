using UnityEngine;
using UnityEngine.SceneManagement;
public class Cinematica : MonoBehaviour
{
    public GameObject cinematicaPanel;
    public GameObject seleccionMundoPanel;
    public static bool siCinematica = false;


    public void Continue()
    {
        seleccionMundoPanel.SetActive(true);
        cinematicaPanel.SetActive(false);
        siCinematica = true;
    }

}