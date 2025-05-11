using UnityEngine;
using UnityEngine.SceneManagement;
public class SeleccionMundo : MonoBehaviour
{

    public void MFantasia()
    {
        SceneManager.LoadScene("Fantasia");
    }

    public void MFuturista()
    {
        SceneManager.LoadScene("Futurista");
    }
    public void MSkate()
    {
        SceneManager.LoadScene("Skate");
    }
}