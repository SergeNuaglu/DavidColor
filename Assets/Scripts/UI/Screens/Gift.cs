using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _fullnessPercent;

    private float _onePercentValue = 0.01f;
    private int _currentFullness;
    private int _lastFullness;
    private char _percentSign = '%';
    private int _maxValue = 100;

    public bool IsFull { get; private set; }

    private void Awake()
    {
        _currentFullness = GetCurrentValue();
        _image.fillAmount = _currentFullness * _onePercentValue;
        _fullnessPercent.text = _currentFullness.ToString() + _percentSign;

        if (_currentFullness == _maxValue)
        {
            IsFull = true;
        }
        else
        {
            IsFull = false;
        }

        _lastFullness = _currentFullness;
    }

    private int GetCurrentValue()
    {
        const int MinValuePerLevel = 17;
        const int MaxValuePerLevel = 25;

        int valuePerLevel = Random.Range(MinValuePerLevel, MaxValuePerLevel);

        if (_lastFullness + valuePerLevel <= _maxValue)
        {
            return _lastFullness + valuePerLevel;
        }
        else if (_lastFullness == _maxValue)
        {
            return valuePerLevel;
        }

        return _maxValue;
    }
}
