using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SequenceGame : MonoBehaviour
{
    public Button[] buttons; // Los 9 botones
    public TMP_Text resultText;

    // Los índices correctos en orden (ajústalo como quieras)
    private List<int> correctSequence = new List<int> { 3, 5, 4, 1 };

    private List<int> playerSequence = new List<int>();

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
        }

        resultText.text = "";
    }

    void OnButtonPressed(int index)
    {
        if (playerSequence.Count >= 4)
            return;

        Debug.Log("Botón pulsado: " + index); // Verifica qué índice se está registrando

        playerSequence.Add(index);
        buttons[index].interactable = false;

        if (playerSequence.Count == 4)
        {
            Debug.Log("Secuencia del jugador: " + string.Join(",", playerSequence));
            CheckSequence();
        }
    }
    void CheckSequence()
    {
        if (correctSequence.SequenceEqual(playerSequence))
        {
            resultText.text = "¡Ganaste! Secuencia correcta.";
        }
        else
        {
            resultText.text = "Incorrecto. Intenta de nuevo.";
            Invoke("ResetGame", 2f);
        }
    }

    void ResetGame()
    {
        playerSequence.Clear();
        resultText.text = "";

        foreach (var button in buttons)
            button.interactable = true;
    }
}