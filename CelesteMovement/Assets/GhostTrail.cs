using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    private Movement movement;
    public Transform ghostParent;
    public Color trailColor;
    public float ghostInterval;
    public Color fadeColor;
    public float fadeTimeout;

    void Start()
    {
        this.movement = FindObjectOfType<Movement>();
    }

    public void Show()
    {
        var sequence = DOTween.Sequence();

        for (int i = 0; i < ghostParent.childCount; i++)
        {
            var currentGhost = ghostParent.GetChild(i);
            sequence.AppendCallback(() => currentGhost.gameObject.SetActive(true));
            sequence.AppendCallback(() => currentGhost.position = this.movement.transform.position);
            sequence.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = this.movement.transform.localScale.x<0);
            sequence.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(this.trailColor, 0));
            sequence.AppendInterval(this.ghostInterval);
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(this.fadeColor, this.fadeTimeout);
    }
}
