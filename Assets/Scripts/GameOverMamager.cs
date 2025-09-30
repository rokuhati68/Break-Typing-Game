using UnityEngine;

public class GameOverMamager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MissBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
