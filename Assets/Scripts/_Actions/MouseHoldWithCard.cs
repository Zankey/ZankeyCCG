using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZCCG.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseHoldWithCard")]
    public class MouseHoldWithCard : Action
    {
        public CardInstance inst;
        public State playerControlState;
        public State targetSelectionState;

        public SO.GameEvent onPlayerControlState;
        public SO.GameEvent onTargetSelectionState;
        
        //public ZCCG.GameStates.State abilityTargetSelection;

        public override void Execute(float d)
        {
            inst = Settings.gameManager.currentSelectedHolder.GetSelectedCard();

            bool mouseIsDown = Input.GetMouseButton(0);

            Card c = inst.viz.card;
            PlayerHolder p = Settings.gameManager.currentPlayer;
            bool canUse = p.CanUseCard(c);

            if (!mouseIsDown)
            {
                
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {
                    GameElements.Area a = r.gameObject.GetComponentInParent<GameElements.Area>();
                    if (a != null)
                    {
                        a.OnDrop();
                        break;
                    }
                }
                
                // will use this for spells & Battlecries
                // bypass normal drop case and go straight to targeting, checking for mana requirement first
                if (inst.viz.card.hasTargeting && canUse)
                {
                    Debug.Log("Setting spell target");
                    if (c.cardType is Spell)
                    {
                        inst.gameObject.SetActive(false);
                    }
                    
                    Settings.gameManager.SetState(targetSelectionState);
                    Debug.Log("Setstate: spellTargetSelection");
                    p.handcards.Remove(inst);
                    onTargetSelectionState.Raise();
                    return;
                }
                else
                {
                    // any normal drop
                    inst.gameObject.SetActive(true);
                    Settings.gameManager.currentSelectedHolder.ResetSelectedCard();
                    Settings.gameManager.SetState(playerControlState);
                    onPlayerControlState.Raise();
                    Debug.Log("Card Dropped, now in Player Control State");
                    return;
                }
            }

        }

    }
}