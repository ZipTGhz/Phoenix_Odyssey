using UnityEngine;

public class LifeTimeDisabler : MonoBehaviour
{
    public float time = 0f;

    public void DelayedDisable()
    {
        Invoke("DisableObject", time);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
