using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Cards/Minion")]
    public class Minion : CardType
    {

        public override void OnSetType(CardViz viz)
        {           
            base.OnSetType(viz);

            viz.statsHolder.SetActive(true);
            viz.clanTag.SetActive(true);
            viz.shield.enabled = false;
            viz.heart.enabled = true;
        }

    }
}
