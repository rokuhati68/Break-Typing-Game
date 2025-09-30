using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int ballID = 0;
    public float escapeTime = 0f;
    public float ballTime = 0f;
    public int typingCount = 0;
    public int point = 0;
    public bool selectBall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Reset()
    {
        ballID = 0;
        escapeTime = 0f;
        ballTime = 0f;
        typingCount = 0;
        selectBall = false;
    }
}
