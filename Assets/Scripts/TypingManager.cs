using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class TypingManager : MonoBehaviour
{
    public float typingTime = 30.00f;
    private string ballSceneName = "BallScene";
    public TextMeshProUGUI typingTimerText;
    public TextMeshProUGUI typingCountText;
    public TextMeshProUGUI hiraganaText;
    public TextMeshProUGUI romajiText;
    public Image characterImage;
    private string targetWord;

    private int currentCharIndex = 0;
    [SerializeField] private CharacterDataBase characterDataBase;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetTargetWord();
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.TypingBGM);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        typingTime -= Time.deltaTime;
        if (typingTime <= 0)
        {
            Debug.Log("Typing time is over.");
            SceneManager.LoadScene(ballSceneName);
        }
        typingTimerText.text = typingTime.ToString("F2");
        typingCountText.text = GameManager.Instance.typingCount.ToString();
    }

    public void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            // 押されたキーを取得
            string keyPressed = Input.inputString;
            SoundManager.Instance.PlaySE(SESoundData.SE.Typing);

            if (keyPressed.Length > 0)
            {
                // 入力された文字がターゲット文字と一致するかチェック
                if (keyPressed[0] == targetWord[currentCharIndex])
                {
                    // 一致した場合、入力された文字を表示
                    currentCharIndex++;
                    GameManager.Instance.typingCount++;
                    ColorChange();
                    // 全ての文字が入力された場合
                    if (currentCharIndex >= targetWord.Length)
                    {
                        Debug.Log("正解！");
                        // 次の単語を生成
                        SetTargetWord();
                    }
                }
                else
                {
                    // 間違えた場合、何か処理をする
                    Debug.Log("不正解！");
                }
            }
        }
    }
    public void SetTargetWord()
    {
        currentCharIndex = 0;
        int index = Random.Range(0, characterDataBase.characterDataList.Count);
        var randomCharacter = characterDataBase.characterDataList[index];
        romajiText.text = randomCharacter.romaji;
        hiraganaText.text = randomCharacter.hiragana;
        characterImage.sprite = randomCharacter.characterImage;
        characterImage.SetNativeSize();
        targetWord = randomCharacter.romaji; // ターゲットワードを設定

    }
    public void ColorChange()
    {
        string updatedText = "";

        // 正しく打てた文字 → 薄い色に
        for (int i = 0; i < currentCharIndex; i++)
        {
            updatedText += $"<color=#FFFFFF50>{targetWord[i]}</color>"; // 薄グレー
        }

        // 残りの文字 → 通常色
        for (int i = currentCharIndex; i < targetWord.Length; i++)
        {
            updatedText += targetWord[i];
        }
        romajiText.text = updatedText;
    }
}
