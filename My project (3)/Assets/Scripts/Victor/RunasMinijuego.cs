using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class ButtonOrderGame : MonoBehaviour
{
    public Button[] bottomButtons; // Los 4 botones en la parte inferior
    public TMP_Text resultText;

    GameManager gameManager;

    private List<int> correctOrder = new List<int> { 0, 1, 2, 3 }; // �ndice de orden correcto
    private List<int> selectedOrder = new List<int>();

    void Start()
    {
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i;
            bottomButtons[i].onClick.AddListener(() => OnButtonPressed(index));
            gameManager = FindFirstObjectByType<GameManager>();   
        }

        resultText.text = "";
    }

    void OnButtonPressed(int index)
    {
        if (selectedOrder.Count >= 4)
            return;

        selectedOrder.Add(index);

        // Desactiva el bot�n para evitar m�ltiples clics
        bottomButtons[index].interactable = false;

        if (selectedOrder.Count == 4)
            CheckResult();
    }

    void CheckResult()
    {
        if (correctOrder.SequenceEqual(selectedOrder))
        {
            resultText.text = "�Ganaste! Orden correcto.";
            if (gameManager != null)
            {
                gameManager.puzzleBotonesResuelto = true;
                gameManager.StartCoroutine(gameManager.ChangeScene(5, 2));
            }

        }
        else
        {
            resultText.text = "Orden incorrecto. Intenta de nuevo.";
            Invoke("ResetGame", 2f); // Espera 2 segundos antes de reiniciar
        }
    }

    void ResetGame()
    {
        selectedOrder.Clear();
        resultText.text = "";

        // Reactivar todos los botones
        foreach (Button btn in bottomButtons)
        {
            btn.interactable = true;
        }
    }
}