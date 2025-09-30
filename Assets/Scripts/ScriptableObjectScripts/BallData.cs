using UnityEngine;


[CreateAssetMenu(fileName = "BallData", menuName = "TypingGame/Ball Data")]
public class BallData : ScriptableObject
{
    
    public int ballID;
    public Sprite ballImage;
    public bool isGet;
    [TextArea]
    public string description;
}
