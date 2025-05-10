using UnityEngine;

public class NameTag : MonoBehaviour
{
    // Almacena la resoluci�n actual de pantalla y su equivalencia en unidades del mundo
    Vector2 resolution, resolutionInWorldUnits = new Vector2(15.96f, 10);

    // Start se llama antes de la primera actualizaci�n del frame
    void Start()
    {
        // Guarda la resoluci�n de la pantalla (en p�xeles)
        resolution = new Vector2(Screen.width, Screen.height);
    }

    // LateUpdate se llama despu�s de Update, ideal para seguir a la c�mara u objetos
    void LateUpdate()
    {
        FollowMouse(); // Llama a la funci�n que hace que el objeto siga al mouse
    }

    private void FollowMouse()
    {
        // Convierte la posici�n del mouse a coordenadas del mundo 2D, proporcional a la resoluci�n
        transform.position = Input.mousePosition / resolution * resolutionInWorldUnits;
    }
}
