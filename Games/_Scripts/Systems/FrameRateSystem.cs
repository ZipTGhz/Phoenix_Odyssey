using System.Collections.Generic;
using UnityEngine;

public class FrameRateSystem : MonoBehaviour
{
    public static FrameRateSystem Instance { get; private set; }

    [SerializeField]
    FrameRateValue _targetFrameRate;

    readonly Dictionary<FrameRateValue, int> _frameRateHash =
        new()
        {
            { FrameRateValue._24, 24 },
            { FrameRateValue._30, 30 },
            { FrameRateValue._60, 60 },
            { FrameRateValue._90, 90 },
            { FrameRateValue._120, 120 },
            { FrameRateValue.Unlimited, 0 },
        };

    void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
        LoadDefaultSetting();
        ChangeFrameRate((int)_targetFrameRate);
    }

    void Reset()
    {
        LoadDefaultSetting();
    }

    void LoadDefaultSetting()
    {
        _targetFrameRate = FrameRateValue._60;
        ChangeFrameRate((int)_targetFrameRate);
    }

    public void ChangeFrameRate(int key)
    {
        FrameRateValue curValue = (FrameRateValue)key;
        if (_frameRateHash.ContainsKey(curValue))
        {
            int value = _frameRateHash[curValue];
            Debug.Log("FPS : " + value);
            Application.targetFrameRate = value;
        }
        else
        {
            Debug.LogWarning("Not exist FPS ");
        }
    }
}

public enum FrameRateValue
{
    _24,
    _30,
    _60,
    _90,
    _120,
    Unlimited,
}
