using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _ui,
        _gameOver,
        _pause;

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
        _ui.SetActive(state == GameState.Playing);
        _gameOver.SetActive(state == GameState.GameOver);
        _pause.SetActive(state == GameState.Paused);
    }

    // Update is called once per frame
    void Update() { }
}
