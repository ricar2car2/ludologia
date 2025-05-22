using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ColorSelector : MonoBehaviour
{
    public Button[] colorButtons; // Asigna los 8 botones desde el inspector
    public TMP_Text resultText; // Asigna el TMP_Text desde el inspector

    private List<int> correctIndexes = new List<int> { 1, 2, 5, 6 }; // Ajusta estos índices a los correctos
    private List<int> selectedIndexes = new List<int>();

    void Start()
    {
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;
            colorButtons[i].onClick.AddListener(() => OnColorSelected(index));
        }

        resultText.text = "";
    }

    void OnColorSelected(int index)
    {
        if (selectedIndexes.Contains(index) || selectedIndexes.Count >= 4)
            return;

        selectedIndexes.Add(index);
        colorButtons[index].interactable = false;

        if (selectedIndexes.Count == 4)
            CheckResult();
    }

    void CheckResult()
    {
        var correct = correctIndexes.OrderBy(i => i);
        var selected = selectedIndexes.OrderBy(i => i);

        if (correct.SequenceEqual(selected))
        {
            resultText.text = "¡Que buena elección brother! Así da gusto vandalizar la propiedad pública";
        }
        else
        {
            resultText.text = "Vaya combinación de mierda hermano, da igual el mensaje, con estos colores no llegamos a ningún lado";
            Invoke("ResetGame", 2f); // Espera 2 segundos antes de reiniciar
        }
    }

    void ResetGame()
    {
        selectedIndexes.Clear();
        resultText.text = "";

        foreach (var button in colorButtons)
            button.interactable = true;
    }
}
