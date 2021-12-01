using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Utils;
using UnityEngine.EventSystems;
using UI;

public class HeroChooserController : MonoBehaviour
{
    // Start is called before the first frame update
    private HeroSlot selectedSlot;

    [SerializeField]
    private HeroSlot heroSlotPrefab;
    [SerializeField]
    private GameObject inputPanel;
    [SerializeField]
    private TMP_InputField input;

    private float slotWidth = 126.0f;
    private float slotHeight = 192.0f;
    private float xOffset = 24.0f;
    private float spacing = 24.0f;
    private float yOffset = 64.0f;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        var heroes = HeroRoster.heroes.Values.ToList();

        int i = 0;
        foreach (var hero in heroes)
        {
            var heroSlot = Instantiate(heroSlotPrefab, this.transform);
            heroSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * slotWidth + slotWidth / 2 + xOffset + i * spacing, -(slotHeight / 2 + yOffset));
            heroSlot.SelectHeroSlotEvent += SetSelectedSlot;
            heroSlot.SetData(hero);
            i++;
        }
    }

    void SetSelectedSlot(HeroSlot slot)
    {
        if (slot.Equals(selectedSlot))
        {
            return;
        }
        if (selectedSlot != null)
        {
            selectedSlot.IsSelected = false;
        }
        selectedSlot = slot;
        if (!inputPanel.activeInHierarchy)
        {
            inputPanel.SetActive(true);
        }
    }

    public void AddPlayableToList()
    {
        var playables = MainStateManager.Instance.GameState.PlayableCharacters;
        if (selectedSlot != null)
        {
            if (selectedSlot.PlayableData != null)
            {
                if(MainStateManager.Instance.GameState.Money < selectedSlot.PlayableData.Price)
                {
                    MessagePanel.Instance.ShowMessage("Not enough money to buy the hero");
                    return;
                }
                
                if (playables.Count >= 4)
                {
                    MessagePanel.Instance.ShowMessage("No more space in the team");
                    return;
                }
                playables.Add(selectedSlot.PlayableData);
                MainStateManager.Instance.GameState.Money -= selectedSlot.PlayableData.Price;
            }
        }
    }
    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
