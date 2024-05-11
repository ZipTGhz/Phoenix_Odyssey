using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        LoadDefaultValue();
    }

    protected virtual void LoadComponents() { }

    protected virtual void LoadDefaultValue() { }
}
