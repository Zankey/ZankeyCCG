using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG.GameElements
{
    [CreateAssetMenu(menuName = "Game Elements/Player Hand Card")]
    public class PlayerHandCard : GE_Logic
    {
        public SO.GameEvent onCurrentCardSelected;
        public ZCCG.GameStates.State holdingCard;

        public override void OnClick(CardInstance inst)
        {
            Settings.gameManager.currentSelectedHolder.ResetSelectedPlayer();
            Settings.gameManager.currentSelectedHolder.SetSelectedCard(inst);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
        }

        public override void OnHighlight(CardInstance inst)
        {
            Debug.Log("Enlarge Preview");
        }
    }
}