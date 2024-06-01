using UnityEngine;

public class AudioSystemManager : MonoBehaviour
{
    public static AudioSystemManager Instance { get; private set; }

    [SerializeField]
    AudioSource _musicSource;

    [SerializeField]
    AudioSource _soundFXSource;

    void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    public void MuteMusicSource(bool value)
    {
        _musicSource.mute = !value;
    }

    public void MuteSoundFXSource(bool value)
    {
        _soundFXSource.mute = !value;
    }
}
