using UnityEngine;

public class InputManager : MonoBehaviour
{
    //SINGLETON
    public static InputManager Instance { get; private set; }

    //SYSTEM
    public PlayerInputs Input;

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Yeh sure!");
            Instance = this;
            Input = new PlayerInputs();
        }
        else
        {
            Debug.LogError("More than one InputManger!");
        }
    }
}
