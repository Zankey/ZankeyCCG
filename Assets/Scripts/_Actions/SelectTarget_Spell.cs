using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZCCG.GameStates;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Actions/SelectSpellTarget")]
    public class SelectTarget_Spell : Action
    {
        
        public State playerControlState;
        public SO.GameEvent onPlayerControlState;

        [System.NonSerialized]
        private CurrentSelectedHolder currentHolder;
        [System.NonSerialized]
        private PlayerHolder currentHero;
        [System.NonSerialized]
        private Vector3 currentSelectedPosition;

        public override void Execute(float d)
        {
            Debug.Log("got here - spell targeting State");

            currentHolder = Settings.gameManager.currentSelectedHolder;

            if (currentHolder.GetSelectedPlayer() != null)
            {
                Debug.Log("Try succeeded for Hero");
                currentHero = currentHolder.GetSelectedPlayer();
                currentSelectedPosition = currentHero.heroStatsUI.transform.position;
            }
            else
            {
                currentHero = null;
            }

            Settings.gameManager.DrawTargetingLine(currentSelectedPosition);

            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {

                    CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();
                    PlayerHolder op = Settings.gameManager.otherPlayer;
                    PlayerHolder cp = Settings.gameManager.currentPlayer;
                    HeroManager hm = r.gameObject.GetComponentInParent<HeroManager>();

                    if (inst != null && validTarget(null, inst))
                    {
                        if (op.cardsDown.Contains(inst) || cp.cardsDown.Contains(inst))
                        {
                            Debug.Log("Selecting Card target for spell, currentTargetCard = inst");
                            currentHolder.SetTargetCard(inst);
                            //Settings.spellManager.CastSpell(currentHolder.currentSelectedCard.spellId);
                        }
                    }
                    if (hm != null && validTarget(hm.player, null))
                    {
                        if (hm.player.Equals(op) || hm.player.Equals(cp))
                        {

                            Debug.Log("Casting Spell on hero:" + hm.player.username);
                            currentHolder.SetTargetPlayer(hm.player);
                            //Settings.spellManager.CastSpell(currentHolder.currentSelectedCard.spellId);
                        }

                    }
                    
                    Settings.gameManager.SetState(playerControlState);
                    onPlayerControlState.Raise();
                    return;

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right Click, attack aborted");
                Settings.gameManager.targetingLine.gameObject.SetActive(false);
                currentHolder.currentSelectedCard.gameObject.SetActive(true);
                currentHolder.ResetSelectedCard();
                currentHolder.ResetSelectedPlayer();
                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }

            bool validTarget(PlayerHolder ph, CardInstance ci)
            {
                bool result = true;

                if (ci != null && ci.tags.ContainsKey("Stealth"))
                {
                    Debug.Log("Cannot target a stealthed character");
                    result = false;
                }
                if (ph != null && ph.isStealth)
                {
                    Debug.Log("Cannot target a stealthed character");
                    result = false;
                }
                if (ci != null && ci.tags.ContainsKey("SpellsAndHeroPowers"))
                {
                    Debug.Log("Cannot be targeted by spells and hero powers");
                    result = false;
                }
                if (ph != null && ph.isSpellsAndHeroPowers)
                {
                    Debug.Log("Cannot be targeted by spells and hero powers");
                    result = false;
                }


                return result;
            }
            // void ResolveAbilities()
            // {

            // }

        }
    }
}