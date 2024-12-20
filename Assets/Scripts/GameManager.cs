using ButchersGames;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public event Action OnLevelStarted;
    public event Action OnLevelLoaded;

    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelManager _levelManager;
    public LevelManager LevelManager => _levelManager;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    private PlayerController _player;

    private void Awake()
    {
        if (GameManager.Instance != null) Destroy(this);
        Instance = this;
        _levelManager.Init();
        _nextLevelButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
        FindPlayer();
    }

    private bool FindPlayer()
    {
        bool result = true;
        _player = FindFirstObjectByType<PlayerController>();
        if (_player == null) result = false;
        return result;
    }

    private void OnEnable()
    {
        _levelManager.OnLevelStarted += LevelStarted;
        _startButton.onClick.AddListener(StartLevel);
        _nextLevelButton.onClick.AddListener(NextLevel);
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _levelManager.OnLevelStarted -= LevelStarted;
        _startButton.onClick.RemoveListener(StartLevel);
        _nextLevelButton.onClick.RemoveListener(NextLevel);
        _restartButton.onClick.RemoveListener(RestartLevel);
    }

    private void Start()
    {
        RestartLevel();
    }

    public void LevelStarted()
    {
        OnLevelStarted?.Invoke();
    }

    public void StartLevel()
    {
        _startButton.gameObject.SetActive(false);
        _levelManager.StartLevel();
        _player.PlayerAnimator.PlayWalk();
    }

    public void RestartLevel()
    {
        _restartButton.gameObject.SetActive(false);
        _levelManager.RestartLevel();
        OnLevelLoaded?.Invoke();
        MovePlayerToSpawn();

        _startButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        _nextLevelButton.gameObject.SetActive(false);
        _levelManager.NextLevel();
        OnLevelLoaded?.Invoke();
        MovePlayerToSpawn();
        _startButton.gameObject.SetActive(true);
    }

    public void MovePlayerToSpawn()
    {
        if (!FindPlayer())
        {
            Debug.LogWarning("Player was not found and can't be moved to spawn point");
            return;
        }
        var spawnPoint = _levelManager.Levels[_levelManager.CurrentLevelIndex].PlayerSpawnPoint;
        spawnPoint.GetPositionAndRotation(out var pos, out var rot);
        _player.Rigidbody.Move(pos, rot);
        _player.PlayerAnimator.PlayIdle();
    }

    public void WinGame()
    {
        _nextLevelButton.gameObject.SetActive(true);
        //todo;
        _player.StopWalking();
        _player.PlayerAnimator.PlayDance();
    }

    public void LoseGame()
    {
        _restartButton.gameObject.SetActive(true);
        //Todo restart button;
        _player.StopWalking();
        _player.PlayerAnimator.PlayAngry();
    }
}
