using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZCCG.GameStates;


namespace ZCCG
{
    [CreateAssetMenu(menuName = "Actions/BattlecryTarget")]
    public class BattlecryTarget : Action
    {
        public State playerControlState;
        public SO.GameEvent onPlayerControlState;

        private CurrentSelectedHolder currentHolder;
        [System.NonSerialized]
        private CardInstance currentCard;
        [System.NonSerialized]
        private CardType currentType;
        [System.NonSerialized]
        private Vector3 currentSelectedPosition;

        public override void Execute(float d)
        {
            currentHolder = Settings.gameManager.currentSelectedHolder;

            if (currentHolder.GetSelectedCard() != null)
            {
                Debug.Log("Try succeeded for card");
                currentCard = currentHolder.GetSelectedCard();
                currentType = currentCard.viz.card.cardType;
                currentSelectedPosition = currentCard.transform.position;
            }
            else
            {
                currentCard = null;
                currentType = null;
            }

            Settings.gameManager.DrawTargetingLine(currentSelectedPosition);

            //if (currentCardSelected.TryGetComponent(out CardInstance )

            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {

                    CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();
                    PlayerHolder op = Settings.gameManager.otherPlayer;
                    PlayerHolder cp = Settings.gameManager.currentPlayer;
                    HeroManager hm = r.gameObject.GetComponentInParent<HeroManager>();

                    if (inst != null)
                    {
                        if (op.cardsDown.Contains(inst))
                        {
                            Debug.Log("your target is a card instance on the other player's board");
                            // if (validTarget(null, inst))
                            // {
                            //     //currentCard.BattlecryTarget();
                            // }
                        }
                        if (cp.cardsDown.Contains(inst))
                        {
                            Debug.Log("your target is a card instance on the the current player's board");
                            // if (validTarget(null, inst))
                            // {
                            //     //currentCard.BattlecryTarget();
                            // }
                        }
                    }
                    if (hm != null)
                    {
                        if (hm.player.Equals(op))
                        {
                            // if (validTarget(op, null))
                            // {
                            //     //currentCard.BattlecryTarget();
                            // }
                        }

                        if (hm.player.Equals(cp))
                        {
                            // if (validTarget(op, null))
                            // {
                            //     //currentCard.BattlecryTarget();
                            // }
                        }
                    }

                    Settings.gameManager.targetingLine.gameObject.SetActive(false);
                    currentHolder.ResetSelectedCard();
                    Settings.gameManager.SetState(playerControlState);
                    onPlayerControlState.Raise();
                    return;

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right Click, attack aborted");
                Settings.gameManager.targetingLine.gameObject.SetActive(false);
                currentHolder.ResetSelectedCard();
                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }


            // bool validTarget(PlayerHolder ph, CardInstance ci)
            // {
            //     bool result = true;

            //         if (ci != null && ci.isStealth)
            //         {
            //             result = false;
            //             Debug.Log("Can not target that: Stealth");
            //         }
            //         if (ph != null && ph.isStealth)
            //         {
            //             result = false;
            //             Debug.Log("Can not target that: Stealth");
            //         }


            //     }

            //     return result;
            // }
        }
    }
}