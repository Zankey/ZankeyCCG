using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



namespace ZCCG
{
    public static class Settings
    {
        public static GameManager gameManager;
        public static ManaManager manaManager;
        public static SpellManager spellManager;
        private static ResourcesManager _resourcesManager;
        private static ConsoleHook _consoleManager;

        public static void RegisterEvent(string e, Color color = default(Color))
        {
            if(_consoleManager == null)
            {
                _consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
            }

            _consoleManager.RegisterEvent(e,color);
        }

        public static ResourcesManager GetResourcesManager()
        {
            if (_resourcesManager == null)
            {
                _resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager; //make sure the Scriptable Object has the same name "ResourcesManager"
                _resourcesManager.Init();
            }

            return _resourcesManager;
        }

        //Moved from mouse over detection
        public static List<RaycastResult> GetUIObjs()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // creates a list of everything you are mousing over, and lets you know if it has any of the IClickable methods
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static void DropCreatureCard(Transform c, Transform p, CardInstance cardInst)
        {
            //Execute any special abilities here (charge -> isUsable = true // Battlecry: Do something // Spell, similar to battlecry but needs to have tag of "spell" associated with it)

            if (cardInst.isCharge)
            {
                cardInst.isAsleep = false;
            }

            if (cardInst.isAsleep)
            {
                cardInst.viz.asleep.gameObject.SetActive(true);
            }

            SetParentForCard(c,p);

            if (!cardInst.viz.card.hasTargeting) // otherwise we want to reduce mana cost after battlecry has finished
            {
                manaManager.PayManaCost(cardInst.viz.card.cost);
            }
            gameManager.currentPlayer.DropCard(cardInst);

        }

        // for board placement of minions and weapons
        public static void SetParentForCard(Transform c, Transform p)
        {
            c.SetParent(p);
            c.localPosition = Vector3.zero;
            c.localEulerAngles = Vector3.zero;
            c.localScale = Vector3.one;
        }

        // used in mana manager for vizualization
        public static void SetParentForMana(Transform c, Transform p)
        {
            c.SetParent(p);
            c.localPosition = Vector3.zero;
            c.localEulerAngles = Vector3.zero;
            c.localScale = new Vector3(15,15,0);
        }

    }
}