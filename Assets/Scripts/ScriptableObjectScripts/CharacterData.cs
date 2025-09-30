using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Data", order = 0)]
public class CharacterData : ScriptableObject
{
    public Sprite characterImage;
    public string hiragana;
    public string romaji;
    public bool isGet;
    [TextArea]
    public string description;
}
