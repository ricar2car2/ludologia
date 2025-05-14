using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ButtonOrderGame : MonoBehaviour
{
    public Button[] bottomButtons; // Los 4 botones en la parte inferior
    public Transform centerPanel; // Panel en el centro donde se mostrarán los botones seleccionados
    public TMP_Text resultText;

    private List<int> correctOrder = new List<int> { 2, 0, 3, 1 }; // Índice de orden correcto
    private List<int> selectedOrder = new List<int>();

    void Start()
    {
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i;
            bottomButtons[i].onClick.AddListener(() => OnButtonPressed(index));
        }

        resultText.text = "";
    }

    void OnButtonPressed(int index)
    {
        if (selectedOrder.Count >= 4)
            return;

        selectedOrder.Add(index);

        // Mover el botón al panel central
        bottomButtons[index].transform.SetParent(centerPanel);
        bottomButtons[index].interactable = false;

        if (selectedOrder.Count == 4)
            CheckResult();
    }

    void CheckResult()
    {
        if (correctOrder.SequenceEqual(selectedOrder))
        {
            resultText.text = "¡Ganaste! Orden correcto.";
        }
        else
        {
            resultText.text = "Orden incorrecto. Intenta de nuevo.";
            Invoke("ResetGame", 2f);
        }
    }

    void ResetGame()
    {
        selectedOrder.Clear();
        resultText.text = "";

        // Borrar los botones en el panel central
        foreach (Transform child in centerPanel)
        {
            Destroy(child.gameObject);
        }
    }
}