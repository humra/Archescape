using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioMixer masterMixer;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetEnvironmentalAudioVolume(float newVolume)
    {
        masterMixer.SetFloat("Environmental", Mathf.Log(newVolume) * 20);
    }

    public void SetSoundtrackAudioVolume(float newVolume)
    {
        masterMixer.SetFloat("Soundtrack", Mathf.Log(newVolume) * 20);
    }
}
