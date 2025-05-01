using UnityEngine;

public class NameTag : MonoBehaviour
{
    public Camera uiCamera; // Asigna tu Main Camera
    public Canvas nameTagCanvas; // Asigna el NameTag Canvas (World Space)

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos;

        // Convertir la posición del mouse en pantalla a posición en el mundo del canvas
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            nameTagCanvas.transform as RectTransform,
            mousePos,
            uiCamera,
            out worldPos
        );

        rectTransform.position = worldPos;
    }
}
