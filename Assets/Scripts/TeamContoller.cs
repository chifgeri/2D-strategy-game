using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamContoller : MonoBehaviour
{
    [SerializeField]
    private HeroSlot heroSlotPrefab;
    // Start is called before the first frame update
    private float slotWidth = 126.0f;
    private float slotHeight = 192.0f;
    private float xOffset = 172.0f;
    private float spacing = 24.0f;
    private float yOffset = 24.0f;

    private List<HeroSlot> slots = new List<HeroSlot>();

    private void Awake()
    {
       for(int i = 0; i < 4; i++) { 
            var heroSlot = Instantiate(heroSlotPrefab, this.transform);
            heroSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * slotWidth + slotWidth / 2 + xOffset + i * spacing, -(slotHeight / 2 + yOffset));
            slots.Add(heroSlot); 
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(MainStateManager.Instance != null)
        {
            FillSlots();
        }
    }

    void FillSlots()
    {
        var playables = MainStateManager.Instance.GameState.PlayableCharacters;
        int i = 0;
        foreach(var playable in playables)
        {
            slots[i].SetData(playable);
            i++;
        }
    }
}
