using System.Collections;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterTests
{
    private GameObject player;
    private GameObject enemy;
    // Singleton classes, instantiate them globally
    GameObject textManager = (GameObject) MonoBehaviour.Instantiate(Resources.Load("Managers/TextManager"));
    GameObject mainStateManager =  (GameObject)MonoBehaviour.Instantiate(Resources.Load("Managers/MainStateManager"));
    GameObject inventoryManager = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Managers/InventoryAndSkillController"));


    [SetUp]
    public void Setup()
    {
        this.player = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Axeman"));
        this.enemy = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Skeleton"));
                    // Initialize game state before first frame
        MainStateManager.Instance.GameState = new GameState(null, null, null, true, true, new Vector3());
        MainStateManager.Instance.GameState.Inventory = new Inventory(10);
    }

    [TearDown]
    public void Teardown()
    {
        MonoBehaviour.DestroyImmediate(player);
        MonoBehaviour.DestroyImmediate(enemy);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestCharacterHeal()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.Health = 0;
        playerCharacter.Heal(20);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(20, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterHealWithMoreThanMaximum()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.Health = 0;
        playerCharacter.Heal(200);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(100, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterHit()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var enemyCharacter = enemy.GetComponent<EnemyController>();

        playerCharacter.Hit(20,enemyCharacter);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(80, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterHitMoreThanMaximum()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var enemyCharacter = enemy.GetComponent<EnemyController>();

        playerCharacter.Hit(200, enemyCharacter);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(0, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterHitNegative()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var enemyCharacter = enemy.GetComponent<EnemyController>();
        playerCharacter.Health = 70;
        playerCharacter.Hit(-30, enemyCharacter);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(70, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterHitPlaysAnimation()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var enemyCharacter = enemy.GetComponent<EnemyController>();
        playerCharacter.Hit(40, enemyCharacter);

        yield return new WaitForSeconds(0.1f);

        var animator = playerCharacter.GetComponent<Animator>();
       Assert.IsFalse(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"));
    }

    [UnityTest]
    public IEnumerator TestCharacterHealNegative()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Heal(-40);
        Assert.NotNull(playerCharacter);
        Assert.AreEqual(100, playerCharacter.Health);
    }

    [UnityTest]
    public IEnumerator TestCharacterSelect()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        Assert.False(playerCharacter.IsSelected);        
        playerCharacter.Select();
        Assert.True(playerCharacter.IsSelected);
    }

    [UnityTest]
    public IEnumerator TestCharacterUnselect()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        
        playerCharacter.Select();
        Assert.True(playerCharacter.IsSelected);
        playerCharacter.UnSelect();
        Assert.False(playerCharacter.IsSelected);
    }



    [UnityTest]
    public IEnumerator TestCharacterIsNext()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        Assert.False(playerCharacter.IsNext);
        playerCharacter.SetNext();
        Assert.True(playerCharacter.IsNext);
    }

    [UnityTest]
    public IEnumerator TestCharacterUnsetNext()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();


        playerCharacter.SetNext();
        Assert.True(playerCharacter.IsNext);
        playerCharacter.UnsetNext();
        Assert.False(playerCharacter.IsNext);
    }


    [UnityTest]
    public IEnumerator TestPLayerCharacterHasSkills()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        Assert.AreEqual(4, playerCharacter.GetSkills().Count);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSkillsNotNull()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        foreach(var skill in playerCharacter.GetSkills())
        {
            Assert.NotNull(skill);
        }
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterWeaponEquipWhenNoWeapon()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        var weapon = new Weapon("Weapon", 10, WeaponType.LongSword, 100);
        playerCharacter.EquipWeapon(weapon);
        Assert.True(playerCharacter.Weapon.Equals(weapon));
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterWeaponEquipWhenAlreadyHasWeapon()
    {
        yield return null;
        // Start runs in first frame

        var playerCharacter = player.GetComponent<HeroController>();

        var weapon = new Weapon("Weapon", 10, WeaponType.LongSword, 100);
        playerCharacter.EquipWeapon(weapon);
        Assert.True(playerCharacter.Weapon.Equals(weapon));

        var weapon2 = new Weapon("Weapon2", 100, WeaponType.LongSword, 100);
        playerCharacter.EquipWeapon(weapon2);
        Assert.True(playerCharacter.Weapon.Equals(weapon2));
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterWeaponUnequipWhenAlreadyHasWeapon()
    {
        yield return null;
        // Start runs in first frame

        var playerCharacter = player.GetComponent<HeroController>();

        var weapon = new Weapon("Weapon", 10, WeaponType.LongSword, 100);
        playerCharacter.EquipWeapon(weapon);
        Assert.True(playerCharacter.Weapon.Equals(weapon));
        playerCharacter.UnequipWeapon();
        Assert.IsNull(playerCharacter.Weapon);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterGetDamage()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Level = 3;
        playerCharacter.BaseDamage = 10;

        Assert.AreEqual(30, playerCharacter.GetCurrentDamage());
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterGetArmor()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Level = 3;
        playerCharacter.BaseArmor = 10;

        Assert.AreEqual(30, playerCharacter.GetCurrentArmor());
    }

    [UnityTest]
    public IEnumerator TestPlayerAddExperience()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Experience += 100;

        Assert.AreEqual(100, playerCharacter.Experience);
    }


    [UnityTest]
    public IEnumerator TestPlayerAddExperienceMaximum()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Experience += 2000;

        Assert.AreEqual(0, playerCharacter.Experience);
    }

    [UnityTest]
    public IEnumerator TestPlayerLevelUp()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();

        playerCharacter.Level = 1;
        playerCharacter.Experience += 1500;

        Assert.AreEqual(0, playerCharacter.Experience);
        Assert.AreEqual(2, playerCharacter.Level);

    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterEnableSkills()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.EnableSkills();

        foreach (var skill in playerCharacter.GetSkills())
        {
            Assert.False(skill.Disabled);
        }

    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterDisableSkills()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.EnableSkills();

        foreach (var skill in playerCharacter.GetSkills())
        {
            Assert.False(skill.Disabled);
        }

        playerCharacter.DisableSkills();

        foreach (var skill in playerCharacter.GetSkills())
        {
            Assert.True(skill.Disabled);
        }
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSelectSkillFromValidSkillsAndNext()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var skill = playerCharacter.GetSkills()[0];
        playerCharacter.SetNext();
        Assert.NotNull(skill);

        playerCharacter.SelectSkill(skill);

        Assert.AreEqual(skill, playerCharacter.SelectedSkill);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSelectSkillFromValidSkillsNotNext()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var skill = playerCharacter.GetSkills()[0];

        Assert.NotNull(skill);

        playerCharacter.SelectSkill(skill);

        Assert.AreEqual(null, playerCharacter.SelectedSkill);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSelectSkillFromOtherSkills()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.SetNext();
        var skillObject = (GameObject) MonoBehaviour.Instantiate(Resources.Load("Skills/SpearBuff"));
        var skill = skillObject.GetComponent<SkillBase>();
        Assert.NotNull(skill);

        playerCharacter.SelectSkill(skill);

        Assert.IsNull(playerCharacter.SelectedSkill);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSetSkill()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.SetNext();
        var skillObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Skills/SpearBuff"));
        var skill = skillObject.GetComponent<SkillBase>();
        Assert.NotNull(skill);

        playerCharacter.setSkill(skill, 1);

        Assert.AreEqual(skill, playerCharacter.GetSkills()[1]);
    }

    [UnityTest]
    public IEnumerator TestPlayerCharacterSetSkillWrongPosition()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        playerCharacter.SetNext();
        var skillObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Skills/SpearBuff"));
        var skill = skillObject.GetComponent<SkillBase>();

        Assert.Throws<System.Exception>(() => playerCharacter.setSkill(skill, 6), "Wrong position given!");
    }


    [UnityTest]
    public IEnumerator TestPlayerCharacterSetSkillWithNull()
    {
        yield return null;

        var playerCharacter = player.GetComponent<HeroController>();
        var skill = playerCharacter.GetSkills()[0];

        playerCharacter.setSkill(null, 0);

        Assert.AreEqual(skill, playerCharacter.GetSkills()[0]);
    }
}
