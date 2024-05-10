using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundAdjust : MonoBehaviour
{
    //Requires set pixels per unit is 1
    public RectTransform targetCanvas;
    Vector2 size;

    IEnumerator updateAdjustRoutine = null;

    void Start()
    {
        Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        size = sprite.bounds.size;
        updateAdjustRoutine = UpdateAdjustAsync();
        StartCoroutine(updateAdjustRoutine);
    }

    IEnumerator UpdateAdjustAsync()
    {
        Adjust();
        updateAdjustRoutine = null;
        yield break;
    }

    void Adjust()
    {
        float scaleX = targetCanvas.rect.width / size.x;
        float scaleY = targetCanvas.rect.height / size.y;
        transform.localScale = new Vector3(scaleX, scaleY);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (updateAdjustRoutine == null)
        {
            updateAdjustRoutine = UpdateAdjustAsync();
            StartCoroutine(updateAdjustRoutine);
        }
    }
#endif
}
