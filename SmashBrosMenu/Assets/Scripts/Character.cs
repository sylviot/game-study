using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public Sprite Icon;
    public float Zoom = 1;
}
