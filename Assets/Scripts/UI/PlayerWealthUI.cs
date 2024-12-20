using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWealthUI : MonoBehaviour
{
    [SerializeField] private PlayerWealth _playerWealth;
    [SerializeField] private List<string> _wealthTitles;
    [SerializeField] private List<Color> _wealthColors;

    [Header("World UI")]
    [SerializeField] private GameObject _worldUIHolder;
    [SerializeField] private Slider _wealthSlider;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private TextMeshProUGUI _wealthTitleText;

    [Header("Screen UI")]
    [SerializeField] private TextMeshProUGUI _currentWealthText;


    private void OnEnable()
    {
        _playerWealth.WealthChanged += UpdateWealthUI;
        
    }

    private void OnDisable()
    {
        _playerWealth.WealthChanged -= UpdateWealthUI;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _worldUIHolder.transform.LookAt(Camera.main.transform);
    }

    private void UpdateWealthUI(int wealth, int wealthLevel)
    {
        _currentWealthText.text = $"{wealth}";
        _wealthSlider.value = (float)wealth / (float)_playerWealth.MaxWealth;
        var color = _wealthColors[wealthLevel];
        _sliderFill.color = color;
        _wealthTitleText.text = _wealthTitles[wealthLevel];
        _wealthTitleText.color = color;
    }
}
