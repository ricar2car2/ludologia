using UnityEngine;

public class NameTag : MonoBehaviour
{
    // Almacena la resolución actual de pantalla y su equivalencia en unidades del mundo
    Vector2 resolution, resolutionInWorldUnits = new Vector2(15.96f, 10);

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        // Guarda la resolución de la pantalla (en píxeles)
        resolution = new Vector2(Screen.width, Screen.height);
    }

    // LateUpdate se llama después de Update, ideal para seguir a la cámara u objetos
    void LateUpdate()
    {
        FollowMouse(); // Llama a la función que hace que el objeto siga al mouse
    }

    private void FollowMouse()
    {
        // Convierte la posición del mouse a coordenadas del mundo 2D, proporcional a la resolución
        transform.position = Input.mousePosition / resolution * resolutionInWorldUnits;
    }
}
