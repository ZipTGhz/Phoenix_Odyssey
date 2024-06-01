using UnityEngine;

public class MusicResources : MonoBehaviour
{
    public static MusicResources Instance { get; private set; }

    [SerializeField]
    AudioClip[] audioClips;

    //SETTERS & GETTERS
    public AudioClip[] AudioClips
    {
        get => audioClips;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
