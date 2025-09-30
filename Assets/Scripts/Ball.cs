using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public int ballID;
    public string resultSceneName = "ResultScene"; // Unique identifier for the ball
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (GameManager.Instance.ballID != ballID)
        {
            Debug.Log("Wrong");
            SoundManager.Instance.PlaySE(SESoundData.SE.Wrong);
        }
        else
        {
            GameManager.Instance.selectBall = true;
            Debug.Log("Correct");
            SoundManager.Instance.PlaySE(SESoundData.SE.Correct);
        }
        SceneManager.LoadScene(resultSceneName);

    }
}
