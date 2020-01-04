using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator animator;

    private bool animationStarted;
    private bool animationFinished;

    private int jumpedTimes;
    private bool jumpLeft = true;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(nameof(FrogJump));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(this.animationStarted && this.animationFinished)
        {
            this.animationStarted = false;

            // Swap frog jump (end) to parent jump (start)
            this.transform.parent.position = this.transform.position * (this.jumpLeft?1:-1);
            this.transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        this.animationStarted = true;
        this.animationFinished = false;

        this.animator.Play("FrogJump");

        StartCoroutine(nameof(FrogJump));
    }

    void AnimationFinished()
    {
        this.animationFinished = true;
        this.animator.Play("FrogIdle");
    }
}
