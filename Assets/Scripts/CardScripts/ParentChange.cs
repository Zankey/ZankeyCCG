using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZCCG
{
    [System.Serializable]
    public class ParentChange : MonoBehaviour
    {
        // Activate card template based on its parent object 

        public SpriteRenderer obSR;
        public SpriteRenderer weaponSR;
        public TMP_Text obAttack;
        public TMP_Text obHealth;
        public TMP_Text weaponAttack;
        public TMP_Text weaponHealth;

        public Image img;
        public TMP_Text attack;
        public TMP_Text health;
        public GameObject handTemplate;
        public GameObject boardTemplate;
        public GameObject weaponTemplate;
        public GameObject cardBack;

        private string tempAttack;
        private string tempHealth;

        private void OnEnable() 
        {
            obSR.sprite = img.sprite;
            weaponSR.sprite = img.sprite;

            tempAttack = attack.GetComponent<TMP_Text>().text;
            tempHealth = health.GetComponent<TMP_Text>().text;

            obAttack.SetText(tempAttack);
            obHealth.SetText(tempHealth);
            weaponAttack.SetText(tempAttack);
            weaponHealth.SetText(tempHealth);

            this.transform.localScale = Vector3.one;
        }

        private void OnTransformParentChanged() 
        {
            if (transform.parent != null)
            {
                this.OnEnable();
                if (transform.parent.tag == "Board" || transform.parent.tag == "EnemyBoard")
                {
                    handTemplate.SetActive(false);
                    boardTemplate.SetActive(true);
                    weaponTemplate.SetActive(false);
                    cardBack.SetActive(false);
                }
                if (transform.parent.tag == "Hand" || transform.parent.tag == "Holding")
                {
                    handTemplate.SetActive(true);
                    boardTemplate.SetActive(false);
                    weaponTemplate.SetActive(false);
                }
                if (transform.parent.tag == "EnemyHand" || transform.parent.tag == "EnemyHolding")
                {
                    handTemplate.SetActive(false);
                    boardTemplate.SetActive(false);
                    weaponTemplate.SetActive(false);
                    cardBack.SetActive(true);

                }
                if (transform.parent.tag == "Weapon" || transform.parent.tag == "EnemyWeapon")
                {
                    handTemplate.SetActive(false);
                    boardTemplate.SetActive(false);
                    weaponTemplate.SetActive(true);
                    cardBack.SetActive(false);
                }
                if (transform.parent.tag == "Graveyard" || transform.parent.tag == "EnemyGraveyard")
                {
                    handTemplate.SetActive(false);
                    boardTemplate.SetActive(false);
                    weaponTemplate.SetActive(false);
                }
                if (transform.parent.tag == "Deck" || transform.parent.tag == "EnemyDeck")
                {
                    handTemplate.SetActive(false);
                    boardTemplate.SetActive(false);
                    weaponTemplate.SetActive(false);
                }
            }
        }

    }

}