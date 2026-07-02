using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DanganDialogue", menuName = "Scriptable Objects/DanganDialogue")]
[System.Serializable]
public class DanganDialogue : ScriptableObject
{

    public string text;

    public List<int> importantInfo;
}
