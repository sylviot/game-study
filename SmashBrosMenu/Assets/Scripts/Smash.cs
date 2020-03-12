using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Smash : MonoBehaviour
{
    public static Smash instance;

    protected GridLayoutGroup gridLayoutGroup;

    public GameObject characterPrefab;
    public Transform characterSlots;
    public int charactersPerColumn = 5;
    public int charactersPerRow = 2;
    [Header("Characters List")]
    public List<Character> characters = new List<Character>();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        this.gridLayoutGroup = GetComponent<GridLayoutGroup>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(this.gridLayoutGroup.cellSize.x * this.charactersPerColumn, this.gridLayoutGroup.cellSize.y * this.charactersPerRow);


        foreach (var character in this.characters)
        {
            this.SpawnCharacterCell(character);
        }
    }

    private void SpawnCharacterCell(Character character)
    {
        var characterGameObject = Instantiate(this.characterPrefab, this.transform);

        var artwork = characterGameObject.transform.Find("atwork").GetComponent<Image>();
        var name = characterGameObject.transform.Find("name").GetComponent<TextMeshProUGUI>();

        artwork.sprite = character.Sprite;
        name.text = character.Name;
    }

    public void ShowCharacterInSlot(Character character)
    {
        var characterNone = character == null;

        Color color =  characterNone? Color.clear : Color.white;
        Sprite atwork = characterNone ? null : character.Sprite;
        Sprite icon = characterNone ? null : character.Icon;
        string name = characterNone ? "" : character.Name;

        var slot = this.characterSlots.GetChild(0);
        var slotAtwork = slot.Find("atwork");
        var slotIcon = slot.Find("icon");
        var slotName = slot.Find("name");

        var sequence = DOTween.Sequence();
        sequence.Append(slotAtwork.DOLocalMoveX(-300, .05f).SetEase(Ease.OutCubic))
            .AppendCallback(() => slotAtwork.GetComponent<Image>().sprite = atwork)
            .AppendCallback(() => slotAtwork.GetComponent<Image>().color = color)
            .Append(slotAtwork.DOLocalMoveX(300, 0))
            .Append(slotAtwork.DOLocalMoveX(0, .05f).SetEase(Ease.OutCubic));

        slotAtwork.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        slotAtwork.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);

        slotIcon.GetComponent<Image>().sprite = icon;
        slotIcon.GetComponent<Image>().DOFade(.3f, 0);
        slotName.GetComponent<TextMeshProUGUI>().text = name;
    }
    void Update()
    {
        
    }
}
