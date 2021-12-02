using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ItemDictionary
    {
        public static Dictionary<int, Item> weapons = new Dictionary<int, Item>()
        {
            { 1, new Weapon("Small practice sword", 5, WeaponType.LongSword, 100) },
            { 2, new Weapon("Medium practice sword", 8, WeaponType.LongSword, 200) },
            { 3, new Weapon("Large practice sword", 10, WeaponType.LongSword, 300) },
            { 4, new Weapon("Light saber", 7, WeaponType.Saber, 400) },
            { 5, new Weapon("John Saber", 5, WeaponType.Saber, 500) },
            { 6, new Weapon("Kis gyakorlókard", 5, WeaponType.LongSword, 600) },
            { 7, new Weapon("Kis gyakorlókard", 5, WeaponType.LongSword, 700) },
            { 8, new Weapon("Kis gyakorlókard", 5, WeaponType.LongSword, 800) },
            { 9, new Weapon("Kis gyakorlókard", 5, WeaponType.CurvedSword, 900) },
            { 10, new Weapon("Kis gyakorlókard", 5, WeaponType.CurvedSword, 1000) },
            { 11, new Weapon("Kis gyakorlókard", 5, WeaponType.CurvedSword, 1100) },
            { 12, new Weapon("Kis gyakorlókard", 5, WeaponType.CurvedSword, 1200) },
            { 13, new Weapon("Medium Knight Sword", 40, WeaponType.KnightSword, 1300) },
            { 14, new Weapon("Large Knight sword", 50, WeaponType.KnightSword, 1400) },
            { 15, new Weapon("God killer", 100, WeaponType.KnightSword, 10000) },
        };

        public static Dictionary<int, Item> armors = new Dictionary<int, Item>()
        {
            { 1, new Armor("Leather armor", 5, ArmorType.BasicArmor, 100) },
            { 2, new Armor("Hardened leather armor", 8, ArmorType.BasicArmor, 100) },
            { 3, new Armor("Leather armor plates", 10, ArmorType.BasicArmor, 100) },
            { 4, new Armor("", 7, ArmorType.BasicArmor, 100) },
            { 5, new Armor("", 5, ArmorType.BasicArmor, 100) },
            { 6, new Armor("", 5, ArmorType.KnightArmor, 100) },
            { 7, new Armor("", 5, ArmorType.KnightArmor, 100) },
            { 8, new Armor("", 5, ArmorType.KnightArmor, 100) },
            { 9, new Armor("Leather armor", 5, ArmorType.KnightArmor, 100) },
            { 10, new Armor("Leather armor", 5, ArmorType.KnightArmor, 100) },
            { 11, new Armor("Leather armor", 5, ArmorType.KnightArmor2, 100) },
            { 12, new Armor("Leather armor", 5, ArmorType.KnightArmor2, 100) },
            { 13, new Armor("Leather armor", 40, ArmorType.KnightArmor2, 100) },
            { 14, new Armor("Leather armor", 50, ArmorType.KnightArmor2, 100) },
            { 15, new Armor("Leather armor", 100, ArmorType.KnightArmor2, 100) },
        };

        public static Dictionary<int, Item> artifacts = new Dictionary<int, Item>()
        {
            { 1, new Artifact("Latern", 1, 50, ArtifactType.Latern ) },
            { 2, new Artifact("Necklace", 1, 150, ArtifactType.Necklace ) },
            { 3, new Artifact("Key", 1, 200, ArtifactType.Key ) },
            { 4, new Artifact("Skull", 1, 350, ArtifactType.Skull ) },
            { 5, new Artifact("Mirror", 1, 150, ArtifactType.Mirror ) },
            { 6, new Artifact("Piramid", 1, 150, ArtifactType.Piramid ) },
            { 7, new Artifact("Ruby", 1, 1500, ArtifactType.Ruby ) },
            { 8, new Artifact("Trophy", 1, 1000, ArtifactType.Trophy ) },
            { 9, new Artifact("Crystal", 1, 500, ArtifactType.Crystal ) },
            { 10,new Artifact("Diamond", 1, 2000, ArtifactType.Diamond ) },
        };

        //public static Dictionary<int, Item> potions = new Dictionary<int, Item>()
        //{
        //    { 1, new Weapon("Small practice sword", 5, ArmorType.LongSword, 100) },
        //    { 2, new Weapon("Medium practice sword", 8, ArmorType.LongSword, 100) },
        //    { 3, new Weapon("Large practice sword", 10, ArmorType.LongSword, 100) },
        //    { 4, new Weapon("Light saber", 7, ArmorType.Saber, 100) },
        //    { 5, new Weapon("John Saber", 5, ArmorType.Saber, 100) },
        //    { 6, new Weapon("Kis gyakorlókard", 5, ArmorType.LongSword, 100) },
        //    { 7, new Weapon("Kis gyakorlókard", 5, ArmorType.LongSword, 100) },
        //    { 8, new Weapon("Kis gyakorlókard", 5, ArmorType.LongSword, 100) },
        //    { 9, new Weapon("Kis gyakorlókard", 5, ArmorType.CurvedSword, 100) },
        //    { 10, new Weapon("Kis gyakorlókard", 5, ArmorType.CurvedSword, 100) },
        //    { 11, new Weapon("Kis gyakorlókard", 5, ArmorType.CurvedSword, 100) },
        //    { 12, new Weapon("Kis gyakorlókard", 5, ArmorType.CurvedSword, 100) },
        //    { 13, new Weapon("Medium Knight Sword", 40, ArmorType.KnightSword, 100) },
        //    { 14, new Weapon("Large Knight sword", 50, ArmorType.KnightSword, 100) },
        //    { 15, new Weapon("God killer", 100, ArmorType.KnightSword, 100) },
        //};
    }
}
