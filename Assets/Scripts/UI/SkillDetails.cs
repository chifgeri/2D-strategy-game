using Model;
using TMPro;
using UnityEngine;

public class SkillDetails : MonoBehaviour
{
    [SerializeField]
    private TMP_Text skillName;
    [SerializeField]
    private TMP_Text damageValue;
    [SerializeField]
    private TMP_Text healValue;
    [SerializeField]
    private TMP_Text damageModifierValue;
    [SerializeField]
    private TMP_Text description;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5), Quaternion.identity);
        }
    }

    public void SetValues(SkillBase skill)
    {
        skillName.text = skill.Name;
        damageValue.text = skill.Damage.ToString();
        damageModifierValue.text = skill.DamageModifier.ToString();
        healValue.text = skill.Heal.ToString();
        description.text = skill.Description.ToString();
    }
}
