using System.Collections;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ModelTest
{

    // A Test behaves as an ordinary method
    [Test]
    public void TestInventoryCreate()
    {
        var inventory = new Inventory(10);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.IsNull(inventory.GetItems()[0]);
    }

    [Test]
    public void TestInventoryAddItem()
    {
        var inventory = new Inventory(10);
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        inventory.AddItem(weapon);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(weapon, inventory.GetItems()[0]);
    }

    [Test]
    public void TestInventoryIsFull()
    {
        var inventory = new Inventory(0);
        Assert.NotNull(inventory);
        Assert.True(inventory.isFull());
    }
    [Test]
    public void TestInventoryItemRemove()
    {
        var inventory = new Inventory(10);
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        inventory.AddItem(weapon);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(weapon, inventory.GetItems()[0]);

        inventory.RemoveItem(weapon, false);

        Assert.IsNull(inventory.GetItems()[0]);
    }
    [Test]
    public void TestInventoryItemMove()
    {
        var inventory = new Inventory(10);
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        inventory.AddItem(weapon);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(weapon, inventory.GetItems()[0]);

        inventory.MoveItem(weapon, 0, 5, false);

        Assert.IsNull(inventory.GetItems()[0]);
        Assert.AreEqual(weapon, inventory.GetItems()[5]);
    }

    [Test]
    public void TestInventoryItemMoveWithWrongParams()
    {
        var inventory = new Inventory(10);
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        inventory.AddItem(weapon);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(weapon, inventory.GetItems()[0]);

        inventory.MoveItem(weapon, -3, 6, false);

        // It should be not moved
        Assert.AreEqual(weapon, inventory.GetItems()[0]);
        Assert.IsNull(inventory.GetItems()[5]);
    }

    [Test]
    public void TestInventoryRemoveWrongItem()
    {
        var inventory = new Inventory(10);
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        inventory.AddItem(weapon);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(weapon, inventory.GetItems()[0]);

        inventory.RemoveItem(new Armor("Armor", 10, ArmorType.BasicArmor, 100), false);

        // It should be not removed
        Assert.AreEqual(weapon, inventory.GetItems()[0]);
    }

    [Test]
    public void TestInventoryStackableRemoveAll()
    {
        var inventory = new Inventory(10);
        var artifacts = new Artifact("123", 10, 100, ArtifactType.Mirror);
        inventory.AddItem(artifacts);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(artifacts, inventory.GetItems()[0]);
        Assert.AreEqual(10, inventory.GetItems()[0].Amount);

        inventory.RemoveItem(artifacts, true);

        Assert.IsNull(inventory.GetItems()[0]);
    }

    [Test]
    public void TestInventoryStackableRemoveOne()
    {
        var inventory = new Inventory(10);
        var artifacts = new Artifact("123", 10, 100, ArtifactType.Mirror);
        inventory.AddItem(artifacts);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(artifacts, inventory.GetItems()[0]);
        Assert.AreEqual(10, inventory.GetItems()[0].Amount);

        inventory.RemoveItem(artifacts, false);

        Assert.IsNotNull(inventory.GetItems()[0]);
        Assert.AreEqual(9, inventory.GetItems()[0].Amount);
    }

    [Test]
    public void TestInventoryStackableMoveOne()
    {
        var inventory = new Inventory(10);
        var artifacts = new Artifact("123", 10, 100, ArtifactType.Mirror);
        inventory.AddItem(artifacts);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(artifacts, inventory.GetItems()[0]);
        Assert.AreEqual(10, inventory.GetItems()[0].Amount);

        inventory.MoveItem(artifacts, 0, 3, false);

        Assert.IsNotNull(inventory.GetItems()[0]);
        Assert.AreEqual(9, inventory.GetItems()[0].Amount);

        Assert.IsNotNull(inventory.GetItems()[3]);
        Assert.AreEqual(1, inventory.GetItems()[3].Amount);
    }

    [Test]
    public void TestInventoryStackableMoveAll()
    {
        var inventory = new Inventory(10);
        var artifacts = new Artifact("123", 10, 100, ArtifactType.Mirror);
        inventory.AddItem(artifacts);

        Assert.NotNull(inventory);
        Assert.AreEqual(10, inventory.GetItems().Count);
        Assert.AreEqual(artifacts, inventory.GetItems()[0]);
        Assert.AreEqual(10, inventory.GetItems()[0].Amount);

        inventory.MoveItem(artifacts, 0, 3, true);

        Assert.IsNull(inventory.GetItems()[0]);

        Assert.IsNotNull(inventory.GetItems()[3]);
        Assert.AreEqual(10, inventory.GetItems()[3].Amount);
    }

    [Test]
    public void TestWeaponCreation()
    {
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        Assert.AreEqual("123", weapon.Name);
        Assert.AreEqual(1, weapon.Amount);
        Assert.False(weapon.Stackable);
        Assert.AreEqual(WeaponType.LongSword, weapon.GetItemType());
        Assert.AreEqual(10, weapon.Damage);
        Assert.AreEqual(100, weapon.Price);
    }

    [Test]
    public void TestWeaponClone()
    {
        var weapon = new Weapon("123", 10, WeaponType.LongSword, 100);
        var clone = (Weapon)weapon.Clone();
        Assert.AreEqual("123", clone.Name);
        Assert.AreEqual(1, clone.Amount);
        Assert.AreEqual(WeaponType.LongSword, clone.GetItemType());
        Assert.False(clone.Stackable);
        Assert.AreEqual(10, clone.Damage);
        Assert.AreEqual(100, clone.Price);
    }

    [Test]
    public void TestArmorCreation()
    {
        var armor = new Armor("123", 10, ArmorType.KnightArmor, 100);
        Assert.AreEqual("123", armor.Name);
        Assert.AreEqual(1, armor.Amount);
        Assert.False(armor.Stackable);
        Assert.AreEqual(ArmorType.KnightArmor, armor.GetItemType());
        Assert.AreEqual(10, armor.ArmorValue);
        Assert.AreEqual(100, armor.Price);
    }

    [Test]
    public void TestArmorClone()
    {
        var armor = new Armor("123", 10, ArmorType.KnightArmor, 100);
        var clone = (Armor)armor.Clone(1);
        Assert.AreEqual("123", clone.Name);
        Assert.AreEqual(1, clone.Amount);
        Assert.False(clone.Stackable);
        Assert.AreEqual(ArmorType.KnightArmor, clone.GetItemType());
        Assert.AreEqual(10, clone.ArmorValue);
        Assert.AreEqual(100, clone.Price);
    }

    [Test]
    public void TestConsumablesCreation()
    {
        var potion = new Consumable("123", 10, 100, ConsumableType.HealthPotion);
        Assert.AreEqual("123", potion.Name);
        Assert.AreEqual(10, potion.Amount);
        Assert.True(potion.Stackable);
        Assert.AreEqual(ConsumableType.HealthPotion, potion.GetItemType());
        Assert.AreEqual(100, potion.Price);
    }

    [Test]
    public void TestConsumablesClone()
    {
        var potion = new Consumable("123", 10, 100, ConsumableType.HealthPotion);
        var clone = potion.Clone(10);
        Assert.AreEqual("123", clone.Name);
        Assert.AreEqual(10, clone.Amount);
        Assert.True(clone.Stackable);
        Assert.AreEqual(ConsumableType.HealthPotion, clone.GetItemType());
        Assert.AreEqual(100, clone.Price);
    }

    [Test]
    public void TestPlayableDataCreation()
    {
        var data = new PlayableData("TESZT", "ID", PlayableTypes.Paladin, 1, 100, 100, null, null, 100);
        Assert.AreEqual("TESZT", data.Name);
        Assert.AreEqual("ID", data.Id);
        Assert.AreEqual(PlayableTypes.Paladin, data.PlayableType);
        Assert.AreEqual(1, data.Level);
        Assert.AreEqual(100, data.Health);
        Assert.AreEqual(100, data.Experience);
        Assert.AreEqual(null, data.Armor);
        Assert.AreEqual(null, data.Weapon);
        Assert.AreEqual(100, data.Price);
    }

    [Test]
    public void TestEnemyDataCreation()
    {
        var data = new EnemyData("TESZT", "ID", EnemyTypes.Orc, 1, 100);
        Assert.AreEqual("TESZT", data.Name);
        Assert.AreEqual("ID", data.Id);
        Assert.AreEqual(EnemyTypes.Orc, data.EnemyType);
        Assert.AreEqual(1, data.Level);
        Assert.AreEqual(100, data.Health);
    }

    [Test]
    public void TestRoomCreation()
    {
        var data = new Room(1, null, null, 100, new Vector2Int(0, 0), false);
        Assert.AreEqual(1, data.RoomId);
        Assert.AreEqual(null, data.Enemies);
        Assert.AreEqual(null, data.LootItems);
        Assert.AreEqual(100, data.LootMoney);
        Assert.AreEqual(false, data.Cleared);
        Assert.AreEqual(new Vector2Int(0, 0), data.DoorPosition);

    }

}