using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDetection : MonoBehaviour
{
    public AudioClip selectClip;
    protected GraphicRaycaster graphicRaycaster;
    protected PointerEventData pointerEventData = new PointerEventData(null);
    public Transform currentCharacter;
    protected AudioSource audioSource;

    void Start()
    {
        this.graphicRaycaster = GetComponentInParent<GraphicRaycaster>();    
        this.audioSource = GetComponentInParent<AudioSource>();
        this.audioSource.clip = selectClip;
        this.audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        var raycastResult = new List<RaycastResult>();
        this.pointerEventData.position = Camera.main.WorldToScreenPoint(this.transform.position);
        this.graphicRaycaster.Raycast(this.pointerEventData, raycastResult);

        if(raycastResult !=null && raycastResult.Any())
        {
            var raycastResultCharacter = raycastResult.FirstOrDefault().gameObject.transform;

            if(this.currentCharacter != raycastResultCharacter)
            {
                if(this.currentCharacter != null)
                {
                    currentCharacter.Find("borderSelected").GetComponent<Image>().DOKill();
                    currentCharacter.Find("borderSelected").GetComponent<Image>().color = Color.clear;
                }
            
                this.SetCurrentCharacter(raycastResultCharacter);
            }
            
        }
    }

    private void SetCurrentCharacter(Transform currentCharacterTransform)
    {
        Character character = null;
        this.currentCharacter = null;

        if (currentCharacterTransform != null && currentCharacterTransform.Find("borderSelected") != null)
        {
            this.audioSource.PlayOneShot(this.selectClip);
            this.currentCharacter = currentCharacterTransform;
            this.currentCharacter.Find("borderSelected").GetComponent<Image>().DOColor(Color.red, .4f).SetLoops(-1);
            this.currentCharacter.Find("borderSelected").GetComponent<Image>().color = Color.white;
        
            int index = this.currentCharacter.GetSiblingIndex();
            character = Smash.instance.characters[index];
        }

        Smash.instance.ShowCharacterInSlot(character);
    }
}
