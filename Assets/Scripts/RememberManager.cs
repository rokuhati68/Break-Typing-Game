using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RememberManager : MonoBehaviour
{
    public BallDataBase ballDataBase;
    public Image ballImage;
    public float waitTime = 0.0f;
    public float waitTimeMax;
    public string typingSceneName = "TypingScene";
    public string gameOverSceneName = "GameOverScene";
    private bool isBell = true;

    void Start()
    {
        waitTimeMax = Random.Range(5.0f, 10.0f);
        GameManager.Instance.ballID = Random.Range(0, ballDataBase.ballDataList.Count);

        BallData data = ballDataBase.GetBall(GameManager.Instance.ballID);
        if (data == null)
        {
            Debug.LogError("BallDataが見つかりません");
            return;
        }

        ballImage.sprite = data.ballImage;
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Writepencil);
        SoundManager.Instance.randomSource.Play();
    }
    void Update()
    {
        Debug.Log($"Current wait time: {waitTime}, Max wait time: {waitTimeMax}");
        waitTime += Time.deltaTime;
        if (waitTime >= waitTimeMax && isBell)
        {
            //音を鳴らす
            SoundManager.Instance.PlaySE(SESoundData.SE.Bell);
            isBell = false;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            Escape();
        }

    }
    public void Escape()
    {
        if (waitTime >= waitTimeMax)
        {
            GameManager.Instance.escapeTime = waitTime - waitTimeMax;
            Debug.Log("Escape pressed, resetting GameManager.");
            SceneManager.LoadScene(typingSceneName);
        }
        else
        {
            GameManager.Instance.Reset();
            SceneManager.LoadScene(gameOverSceneName);
            Debug.Log("Escape pressed, but wait time not reached.");
        }
        SoundManager.Instance.StopSE(SESoundData.SE.Bell);
        SoundManager.Instance.randomSource.Stop();
        SoundManager.Instance.StopBGM(BGMSoundData.BGM.Writepencil);

    }
}
