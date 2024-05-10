using UnityEngine;

public class InputManager : MonoBehaviour
{
	//SINGLETON
	public static InputManager Instance { get; private set; }

	//SYSTEM
	public PlayerInputs Input { get; private set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Input = new PlayerInputs();
		}
		else
		{
			Debug.LogError("More than one InputManger!");
		}
	}
}
