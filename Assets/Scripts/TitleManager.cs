using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Image[] panels;

    public int currentPanelIndex = 0;
    public string rememberScenceName = "RememberScene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.ResultBGM);

        bgmSlider.onValueChanged.AddListener((value) =>
        {
            value = Mathf.Clamp01(value);

            float decibel = 20f * Mathf.Log10(value);
            decibel = Mathf.Clamp(decibel, -80f, 0f); // Clamp to avoid too low volume
            audioMixer.SetFloat("BGM", decibel);
        });
        seSlider.onValueChanged.AddListener((value) =>
        {
            value = Mathf.Clamp01(value);

            float decibel = 20f * Mathf.Log10(value);
            decibel = Mathf.Clamp(decibel, -80f, 0f); // Clamp to avoid too low volume
            audioMixer.SetFloat("SE", decibel);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNextPanel()
    {
        panels[currentPanelIndex].gameObject.SetActive(false);
        currentPanelIndex++;
        currentPanelIndex %= panels.Length; // Wrap around if we exceed the number of panels
        panels[currentPanelIndex].gameObject.SetActive(true);
        SoundManager.Instance.PlaySE(SESoundData.SE.Decide);
    }

    public void ShowlaterPanel()
    {
        panels[currentPanelIndex].gameObject.SetActive(false);

        currentPanelIndex--;
        currentPanelIndex = (currentPanelIndex + panels.Length) % panels.Length; // Wrap around if we go below 0
        panels[currentPanelIndex].gameObject.SetActive(true);
        SoundManager.Instance.PlaySE(SESoundData.SE.Decide);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(rememberScenceName);
        SoundManager.Instance.PlaySE(SESoundData.SE.Decide);
    }
}
