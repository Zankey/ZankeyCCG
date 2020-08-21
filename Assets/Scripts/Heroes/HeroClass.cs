using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    [CreateAssetMenu (menuName = "Heroes")]
    public abstract class HeroClass : ScriptableObject
    {
        public abstract void HeroPower();
        public abstract void HeroPowerDetails();
        public abstract int GetHeroPowerCost();
        public abstract void SetHeroPowerCost(int i);
        public abstract void SetHeroPowerHolder(GameObject go);
        public abstract Sprite GetHeroPowerSprite();
        public abstract Sprite GetPortrait();
        public abstract void SetHeroPowerUsed(bool b);
        public abstract bool GetHeroPowerUsed();

    }
}
