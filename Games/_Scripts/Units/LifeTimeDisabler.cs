using UnityEngine;

public class LifeTimeDisabler : MonoBehaviour
{
    [SerializeField]
    float _time = 0f;

    public void DelayedDisable()
    {
        Invoke("DisableObject", _time);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
