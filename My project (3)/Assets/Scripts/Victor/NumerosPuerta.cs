using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SequenceGame : MonoBehaviour
{
    public Button[] buttons; // Los 9 botones
    public TMP_Text resultText;

    // Los �ndices correctos en orden (aj�stalo como quieras)
    private List<int> correctSequence = new List<int> { 3, 5, 4, 1 };

    private List<int> playerSequence = new List<int>();

    GameManager gameManager;




    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
            gameManager = FindFirstObjectByType<GameManager>();   

        }

        resultText.text = "";
    }

    void OnButtonPressed(int index)
    {
        if (playerSequence.Count >= 4)
            return;

        Debug.Log("Bot�n pulsado: " + index); // Verifica qu� �ndice se est� registrando

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
            resultText.text = "�Ganaste! Secuencia correcta.";
            if (gameManager != null)
            {
                gameManager.puzzleNumerosResuelto = true;
            }

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