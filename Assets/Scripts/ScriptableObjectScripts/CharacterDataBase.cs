using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataBase", menuName = "Scriptable Objects/CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{
    public List<CharacterData> characterDataList;
}
