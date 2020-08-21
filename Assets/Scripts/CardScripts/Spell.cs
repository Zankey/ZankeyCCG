using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Cards/Spell")]
    public class Spell : CardType
    {
        public override void OnSetType(CardViz viz)
        {

            base.OnSetType(viz); //calls virtual method from base class on setting type

            viz.statsHolder.SetActive(false);
            viz.clanTag.SetActive(true);

        }

    }
}
