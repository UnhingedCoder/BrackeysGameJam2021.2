using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField] private float[] heights;

    [SerializeField] private List<Transform> allChilds = new List<Transform>();

    [Space(5)]
    [SerializeField] private Material[] colorMaterials;

    [Header("FLOAT")]
    [SerializeField] private bool shouldFloat;

    [SerializeField] private float[] floatDurations;
    [SerializeField] private float[] floatDeltaHeights;

    [SerializeField] private Ease floatEase;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        if (shouldFloat)
            FloatingHeights();
    }

    private void FloatingHeights()
    {
        for (int i = 0; i < allChilds.Count; i++)
        {
            Sequence s = DOTween.Sequence();

            float targetHeight = allChilds[i].position.y - floatDeltaHeights[Random.Range(0, floatDeltaHeights.Length)];
            float targetDuration = floatDurations[Random.Range(0, floatDurations.Length)];
            s.Append(allChilds[i].DOMoveY(targetHeight, targetDuration).SetRelative().SetEase(floatEase));

            s.SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void SetInitialState()
    {
        FetchChilds();
        AdjustHeight();
        AssignColor();
    }

    [ContextMenu("Fetch Childs")]
    private void FetchChilds()
    {
        allChilds.Clear();

        foreach (Transform child in this.transform)
        {
            allChilds.Add(child);
        }
    }

    [ContextMenu("Adjust Height")]
    private void AdjustHeight()
    {
        for (int i = 0; i < allChilds.Count; i++)
        {
            allChilds[i].position = new Vector3(allChilds[i].position.x, heights[Random.Range(0, heights.Length)], allChilds[i].position.z);
        }
    }

    [ContextMenu("Assign Color")]
    private void AssignColor()
    {
        for (int i = 0; i < allChilds.Count; i++)
        {
            allChilds[i].GetComponent<Renderer>().material = colorMaterials[Random.Range(0, colorMaterials.Length)];
        }
    }
}
