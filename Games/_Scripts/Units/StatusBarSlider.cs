using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarSlider : MonoBehaviour
{
    //STATUS INFO
    Slider slider;
    TextMeshProUGUI statusInfo;

    void Awake()
    {
        slider = GetComponent<Slider>();
        statusInfo = GetComponentInChildren<TextMeshProUGUI>();
    }

    void setStatusText(float first, float second)
    {
        statusInfo.text = first.ToString() + " / " + second.ToString();
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
        setStatusText(slider.value, slider.maxValue);
    }

    public void SetNewValue(float newValue)
    {
        slider.value = newValue;
        setStatusText(slider.value, slider.maxValue);
    }
}
