using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlowAdjuster : MonoBehaviour
{
    [SerializeField] private bool shouldPulse;

    [SerializeField] private float glowDuration;
    [SerializeField] private float glowPower;

    [SerializeField] private Ease glowEase;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        if (shouldPulse)
            SetGlow();
    }

    private void SetGlow()
    {
        Sequence s = DOTween.Sequence();

        s.Append(renderer.material.DOFloat(glowPower, "Glow_Power", glowDuration).SetEase(glowEase));

        s.SetLoops(-1, LoopType.Yoyo);
    }
}
