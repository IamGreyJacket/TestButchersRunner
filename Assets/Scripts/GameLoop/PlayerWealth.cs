using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWealth : MonoBehaviour
{
    public event Action<int, int> WealthChanged;

    private GameManager _gameManager => GameManager.Instance;

    [Space, SerializeField, Min(0)] private int _defaultWealth = 120;
    [SerializeField] private List<int> _wealthLevels;
    [SerializeField] private int _maxWealth = 300;
    public int MaxWealth => _maxWealth;
    [SerializeField] private List<GameObject> _wealthLevelModels;

    private int _currentWealth = 0;
    public int WealthPoints => _currentWealth;
    private int _currentWealthLevel = 0;
    public int WealthLevel => _currentWealthLevel;

    private void OnEnable()
    {
        if (_gameManager == null) return;
        _gameManager.OnLevelStarted += ResetToDefault;
        _gameManager.OnLevelLoaded += ResetToDefault;
    }

    private void OnDisable()
    {
        if (_gameManager == null) return;
        _gameManager.OnLevelStarted -= ResetToDefault;
        _gameManager.OnLevelLoaded -= ResetToDefault;
    }

    private void Start()
    {
        OnEnable();
    }

    public void ResetToDefault()
    {
        _currentWealth = 0;
        _currentWealthLevel = 0;
        ChangeWealth(_defaultWealth);
    }

    public void ChangeWealth(int amount)
    {
        _currentWealth += amount;
        
        int wealthLevel = 0;
        for (int i = 0; i < _wealthLevels.Count; i++)
        {
            if (_currentWealth >= _wealthLevels[i]) wealthLevel = i;
            else break;
        }
        WealthChanged?.Invoke(_currentWealth, wealthLevel);
        ChangeAppearance(wealthLevel);
        if (_currentWealth < 0) LoseGame();
    }

    private void LoseGame()
    {
        _gameManager.LoseGame();
    }

    private void ChangeAppearance(int wealthLevel)
    {
        if (_currentWealthLevel == wealthLevel) return;
        _currentWealthLevel = wealthLevel;
        _wealthLevelModels.ForEach(m => m.SetActive(false));
        _wealthLevelModels[wealthLevel].SetActive(true);
        //todo: activate animations through animator;
    }
}
