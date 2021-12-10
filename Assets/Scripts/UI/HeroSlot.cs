using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public delegate void SelectHeroSlotDelegate(HeroSlot slot);

public class HeroSlot : MonoBehaviour
{
    [SerializeField]
    private Image heroImage;
    [SerializeField]
    private Image slotImage;
    [SerializeField]
    private Sprite normalSprite;
    [SerializeField]
    private Sprite highlightedSprite;

    private PlayableData playableData;
    private bool isSelected;

    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public PlayableData PlayableData { get => playableData; set => playableData = value; }

    public event SelectHeroSlotDelegate SelectHeroSlotEvent;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void SetData(PlayableData data)
    {
        if (data != null)
        {
            playableData = data;
            var character = PlayerCharacters.Instance.PlayerTypeToPrefab(data.PlayableType);
            heroImage.sprite = character.GetComponent<SpriteRenderer>().sprite;
            heroImage.color = new Color(1, 1, 1, 1);
        } else
        {
            heroImage.sprite = null;
            heroImage.color = new Color(1, 1, 1, 0);
        }


    }

    public void Select()
    {
        if (playableData != null)
        {
            isSelected = true;
            if (SelectHeroSlotEvent != null)
            {
                SelectHeroSlotEvent(this);
            }
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            slotImage.sprite = highlightedSprite;
        } else
        {
            slotImage.sprite = normalSprite;
        }
    }
}
