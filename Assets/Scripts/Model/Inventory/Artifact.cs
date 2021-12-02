using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{

    public enum ArtifactType
    {
        Crystal,
        Diamond,
        Ruby,
        Necklace,
        Latern,
        Key,
        Mirror,
        Piramid,
        Trophy,
        Skull
    }
    public class Artifact: Item
    {
        [SerializeField]
        private ArtifactType type;


        public Artifact(string _name, int _amount, int _price, ArtifactType _type) : base(_name, _price, _amount, true)
        {
            type = _type;
        }

        public override Item Clone(int amount)
        {
            return new Artifact(this.Name, amount, this.Price, this.type);
        }

        public override ItemAttribute GetItemAttributes()
        {
            return null;
        }

        public override Enum GetItemType()
        {
            return type;
        }

        public override Sprite GetSprite() {

            switch (type)
            {
                case ArtifactType.Crystal:
                    return ArtifactSprites.Instance.crystal;
                case ArtifactType.Diamond:
                    return ArtifactSprites.Instance.diamond;
                case ArtifactType.Ruby:
                    return ArtifactSprites.Instance.ruby;
                case ArtifactType.Necklace:
                    return ArtifactSprites.Instance.necklace;
                case ArtifactType.Latern:
                    return ArtifactSprites.Instance.latern; 
                case ArtifactType.Key:
                    return ArtifactSprites.Instance.key;
                case ArtifactType.Mirror:
                    return ArtifactSprites.Instance.mirror;
                case ArtifactType.Piramid:
                    return ArtifactSprites.Instance.piramid;
                case ArtifactType.Trophy:
                    return ArtifactSprites.Instance.trophy;
                case ArtifactType.Skull:
                    return ArtifactSprites.Instance.skull;
                default:
                    return null;
            }
        }

        public override void Use(PlayerCharacter target)
        {
            return;
        }
    }
}
