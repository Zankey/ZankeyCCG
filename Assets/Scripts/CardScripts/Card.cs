using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{

    [CreateAssetMenu(menuName = "Card")]      //Might need to protect these values, but the CardInstance should be ok;
    public class Card : ScriptableObject
    {
        public CardType cardType;
        // [SerializeField] private int baseCost;
        // [SerializeField] private int baseAttack;
        // [SerializeField] private int baseHealth;

        // [System.NonSerialized]
        public int cost;
        // [System.NonSerialized]
        public int attack;
        // [System.NonSerialized]
        public int health;

        public CardProperties[] properties;

        // private void OnEnable() 
        // {
        //     cost = baseCost;
        //     attack = baseAttack;
        //     health = baseHealth;
        // }
    }

}
