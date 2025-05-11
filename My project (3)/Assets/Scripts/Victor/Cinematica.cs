using UnityEngine;
using UnityEngine.SceneManagement;
public class Cinematica : MonoBehaviour
{

    public void Continue()
    {
        SceneManager.LoadScene("SeleccionMundo");
    }

}