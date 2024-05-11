using System;
using UnityEngine;

public class UIManager : CustomMonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject _ui,
        _gameOver,
        _pause;

    public GameObject UI
    {
        get => _ui;
    }
    public GameObject GameOver
    {
        get => _gameOver;
    }
    public GameObject Pause
    {
        get => _pause;
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than two instance of UIManger!", gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        _gameOver.SetActive(state == GameState.GameOver);
        _pause.SetActive(state == GameState.Paused);
    }
}
