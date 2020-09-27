using UnityEngine;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
    public GameObject pauseUI;
    public AudioMixer audioMixer;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void Back()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }
}