using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public string titleSceneName = "TitleScene";
    public string rememberSceneName = "RememberScene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickTitleButton()
    {
        GameManager.Instance.Reset();
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
        SoundManager.Instance.PlaySE(SESoundData.SE.Decide);
        StopBGM();
    }
    public void OnClickRememberButton()
    {
        GameManager.Instance.Reset();
        UnityEngine.SceneManagement.SceneManager.LoadScene(rememberSceneName);
        SoundManager.Instance.PlaySE(SESoundData.SE.Decide);
        StopBGM();
    }

    public void StopBGM()
    {
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.ResultBGM);
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.MissBGM);
    
    }
}
