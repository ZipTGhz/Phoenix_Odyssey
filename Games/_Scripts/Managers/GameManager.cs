using System;
using UnityEngine;

//chi quan ly trong scene
public class GameManager : MonoBehaviour
{
    //SINGLETON
    public static GameManager Instance { get; private set; }

    public static event Action<GameState> OnStateChanged;
    public GameState CurrentState { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Game Manager!");
        }
    }

    void Start() => ChangeState(GameState.Playing);

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.Playing:
                Debug.Log("Playing");
                HandlePlaying();
                break;
            case GameState.Paused:
                Debug.Log("Game paused");
                HandlePaused();
                // Hiển thị menu tạm dừng
                break;
            case GameState.Quest:
                Debug.Log("Quest manager is active");
                // Hiển thị giao diện quản lý nhiệm vụ
                break;
            case GameState.Inventory:
                Debug.Log("Inventory manager is active");
                // Hiển thị giao diện quản lý kho hàng
                break;
            case GameState.Dialogue:
                Debug.Log("Dialogue with NPC");
                // Bắt đầu cuộc trò chuyện với NPC
                break;
            case GameState.Cutscene:
                Debug.Log("Cutscene is playing");
                // Phát cắt cảnh
                break;
            case GameState.Battle:
                Debug.Log("Battle mode is active");
                // Bắt đầu trận đấu
                break;
            case GameState.LevelUp:
                Debug.Log("Level up");
                // Hiển thị thông báo lên cấp
                break;
            case GameState.GameOver:
                Debug.Log("Game over");
                // Hiển thị thông báo kết thúc trò chơi
                break;
        }
        OnStateChanged?.Invoke(newState);
    }

    private void HandlePaused()
    {
        //Do something
    }

    private void HandlePlaying()
    {
        //Do something
    }
}

[SerializeField]
public enum GameState
{
    Playing,
    Paused, // Trò chơi tạm dừng
    Quest, // Trạng thái quản lý nhiệm vụ
    Inventory, // Trạng thái quản lý kho hàng
    Dialogue, // Trạng thái trò chuyện với NPC
    Cutscene, // Trạng thái xem cắt cảnh
    Battle, // Trạng thái trong trận đấu
    LevelUp, // Trạng thái khi lên cấp
    GameOver // Trò chơi kết thúc
}
