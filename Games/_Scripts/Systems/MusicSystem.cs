using UnityEngine;

public class MusicSystem : MonoBehaviour
{
	public static MusicSystem Instance;

	[SerializeField]
	AudioClip[] audioClips;
	AudioSource musicAudioSource;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			musicAudioSource = GetComponent<AudioSource>();
			SceneController.OnSceneChanged += OnSceneChanged;
		}
	}

	void OnDestroy()
	{
		SceneController.OnSceneChanged -= OnSceneChanged;
	}

	void OnSceneChanged(int index)
	{
		PlayMusicByScene(index);
	}

	void PlayMusicByScene(int index)
	{
		if (index >= audioClips.Length)
		{
			Debug.LogError("The index of scene is not valid!");
			return;
		}
		musicAudioSource.clip = audioClips[index];
		musicAudioSource.Play();
	}
}
