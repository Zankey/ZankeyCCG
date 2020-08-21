using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Cards/Weapon")]
    public class Weapon : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);

            viz.statsHolder.SetActive(true);
            viz.clanTag.SetActive(true);
            viz.shield.enabled = true;
            viz.heart.enabled = false;
        }
    }
}
