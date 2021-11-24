using UnityEngine;
using Model;
using System.Collections.Generic;

public class UIOverlayManager : Singleton<UIOverlayManager>
{
    [SerializeField]
    ItemContainer itemContainerPrefab;
    GameObject UIOverlay;
    public SkillSlot slotPrefab;

    public List<SkillSlot> skillSlots;


   protected override void Awake(){
        base.Awake();

        
        // Get the the reference to the UI canvas
        GameObject UIOverlayObject = GameObject.Find("UIOverlay");
         if(UIOverlayObject != null){
            UIOverlay = UIOverlayObject;
         }
        // Initialize Skill slots
        skillSlots = new List<SkillSlot>();
        CreateSkillSlots();
    }

    public void RefreshSkills(PlayerCharacter c){
        if(c.GetSkills().Count == 4 ){
            var skillList = c.GetSkills();
            for(int i = 0; i<4; i++ ){
                skillSlots[i].SetSkill(skillList[i]);
            }
        } else {
            Debug.LogError("[UIPanel]: Inconsistent Skill count on character!");
        }
    }

    public void ShowLootItems(List<Item> items)
    {
        float posOffset = 1.5f;
        for(int i = 0; i < items.Count; i++)
        {
            var itemContainer = Instantiate(itemContainerPrefab, new Vector3(i % 2 == 0 ? i* -posOffset : i*posOffset, 0, 1), Quaternion.identity, UIOverlay.transform);
            itemContainer.GetComponent<RectTransform>().anchoredPosition = new Vector3(i % 2 == 0 ? i * -posOffset : i * posOffset, 0, 1);
            itemContainer.SetItem(items[i]);
        }
    }

    public void CreateSkillSlots(){
        SkillPanel panel = UIOverlay.GetComponentInChildren<SkillPanel>();
        float skillCellSize = 130f;

        RectTransform trans = panel.GetComponent<RectTransform>();

        for(int x=0; x<4;x++){
            SkillSlot newSlot = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            RectTransform skillRectTransform = newSlot.GetComponent<RectTransform>();
            skillRectTransform.SetParent(panel.transform, false);
            newSlot.gameObject.SetActive(true);
            skillRectTransform.SetPositionAndRotation(new Vector3(-panel.transform.position.x, 0, 0), Quaternion.identity);
            skillRectTransform.anchoredPosition = new Vector2(x*skillCellSize - panel.GetComponent<RectTransform>().rect.width/2+skillCellSize/2, 0);
            skillSlots.Add(newSlot);
        }
    }

    public void ShowCharacterInfo(Character c){
        
    }

    public void RefreshInventory(Inventory inventory){
        InventoryPanel panel = UIOverlay.GetComponentInChildren<InventoryPanel>();
    
        if(panel != null){
            panel.RefreshItems(inventory);
        }
    }
}
