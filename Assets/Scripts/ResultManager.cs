using UnityEngine;
using UnityEngine.UI;
using TMPro;
using unityroom.Api;
public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Sprite[] resultImages;
    [SerializeField] private Image resultImage;

    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float escapeTime = GameManager.Instance.escapeTime;
        float ballTime = GameManager.Instance.ballTime;
        int typingCount = GameManager.Instance.typingCount;

        score = typingCount * 100;
        Debug.Log(score);
        if (GameManager.Instance.selectBall) score += Mathf.Max(0, 3000 - (int)((escapeTime + ballTime) * 100));
        resultText.text = score.ToString();

        // C#スクリプトの冒頭に `using unityroom.Api;` を追加してください。

// ボードNo1にスコア123.45fを送信する。
        UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.Always);
        ShowResultImage();
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.ResultBGM);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowResultImage()
    {
        if (GameManager.Instance.selectBall == false)
        {
            resultImage.sprite = resultImages[0]; // Miss
            return;
        }
        else if (score >= 30000)
        {
            resultImage.sprite = resultImages[1]; // Good
        }
        else if (score >= 20000)
        {
            resultImage.sprite = resultImages[2]; // Normal
        }
        else if (score < 10000)
        {
            resultImage.sprite = resultImages[3]; // Miss
        }
        else
        {
            resultImage.sprite = resultImages[4]; // Bad
        }
    }
}
