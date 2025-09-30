using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{
    [SerializeField] private BallDataBase ballDataBase;
    [SerializeField] public Image[] images;

    [SerializeField] public Ball[] balls;
    private int id;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BallSetup();
    }


    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.ballTime += Time.deltaTime;
    }

    public void BallSetup()
    {
        int ballCount = 0;
        while (ballCount < images.Length)
        {
            id = Random.Range(0, ballDataBase.ballDataList.Count);
            if (id == GameManager.Instance.ballID)
            {
                continue; // Ensure we don't select the current ball
            }
            Debug.Log($"Setting up ball with ID: {ballCount}");
            images[ballCount].sprite = ballDataBase.ballDataList[id].ballImage;
            images[ballCount].SetNativeSize();
            balls[ballCount].ballID = id;
            ballCount++;
        }
        id = Random.Range(0, images.Length);
        images[id].sprite = ballDataBase.ballDataList[GameManager.Instance.ballID].ballImage;
        images[id].SetNativeSize();
        balls[id].ballID = GameManager.Instance.ballID;
        Debug.Log($"Ball setup complete with current ball ID: {id}");
    }

    


    
}
