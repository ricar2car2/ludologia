using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public enum PuzzleCondition
    {
        PuzzleBotones,
        PuzzleNumeros,

        PuzzleColores
        // Agrega más aquí si añades nuevos bools
    }

    [Header("Configuración")]
    public PuzzleCondition conditionToCheck;

    [Header("Reemplazo de objetos")]
    public GameObject originalObject;
    public GameObject replacementObject;

    void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        bool conditionMet = false;

        switch (conditionToCheck)
        {
            case PuzzleCondition.PuzzleBotones:
                conditionMet = GameManager.Instance.puzzleBotonesResuelto;
                break;
            case PuzzleCondition.PuzzleNumeros:
                conditionMet = GameManager.Instance.puzzleNumerosResuelto;
                break;
            case PuzzleCondition.PuzzleColores:
                conditionMet = GameManager.Instance.puzzleColoresResuelto;
                break;
        }
        

        if (conditionMet)
        {
            if (originalObject != null)
                originalObject.SetActive(false);

            if (replacementObject != null)
                replacementObject.SetActive(true);
        }
    }
}
