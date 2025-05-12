using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Datos del diálogo")]
    public string characterName;  // Nombre del personaje que habla
    public List<string> lines;    // Lista de líneas de diálogo

    // Puedes agregar más variables aquí si necesitas más información
}

