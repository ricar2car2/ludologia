using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Datos del di logo")]
    public string characterName;  // Nombre del personaje que habla
    public List<string> lines;    // Lista de l neas de di logo

    // Puedes agregar m s variables aqu  si necesitas m s informaci n
}