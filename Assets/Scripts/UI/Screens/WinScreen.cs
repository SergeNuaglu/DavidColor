using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : Screen
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _giftButton;
    [SerializeField] private HorizontalLayoutGroup _buttonsLayoutGroup;
    [SerializeField] private RectTransform _nextButtonTransform;
    [SerializeField] private Gift _gift;
    [SerializeField] private TMP_Text _winSign;
    [SerializeField] private float _delayBeforeOpening = 1.5f;

    private RectTransform _nextButtonInitialTransform;

    public event Action NextLevelButtonClicked;

    private void OnValidate()
    {
        _buttonsLayoutGroup.enabled = false;
    }

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(() => OnButtonClicked(NextLevelButtonClicked));
    }

    private void Start()
    {
        _nextButtonInitialTransform = _nextButtonTransform;
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
    }

    public override void Open()
    {
        SetRandomWinSign();

        if (_gift.IsFull)
        {
            _giftButton.enabled = true;
            _buttonsLayoutGroup.enabled = true;
        }
        else
        {
            _gift.enabled = false;
            _buttonsLayoutGroup.enabled = false;
            _nextButtonTransform = _nextButtonInitialTransform;
        }

        StartCoroutine(ShowScreen());
    }

    public override void Close()
    {
        base.Close();
    }

    private void SetRandomWinSign()
    {
        string[] signs = { "Great", "Nicely Done", "Awesome", "You've done well", "Bravo" };
        float minValue = -1f;
        float maxValue = signs.Length - 1;
        float random = UnityEngine.Random.Range(minValue, maxValue);

        for (int i = 0; i < signs.Length; i++)
        {
            if (random <= i)
            {
                _winSign.text = signs[i];
                return;
            }
        }
    }

    private IEnumerator ShowScreen()
    {
        yield return new WaitForSeconds(_delayBeforeOpening);

        base.Open();
    }
}
