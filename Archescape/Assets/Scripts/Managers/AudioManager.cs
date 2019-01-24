using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of audio manager found");
            GameObject.Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public AudioMixer masterMixer;

    public void SetEnvironmentalAudioVolume(float newVolume)
    {
        masterMixer.SetFloat("Environmental", Mathf.Log(newVolume) * 20);
    }

    public void SetSoundtrackAudioVolume(float newVolume)
    {
        masterMixer.SetFloat("Soundtrack", Mathf.Log(newVolume) * 20);
    }
}
