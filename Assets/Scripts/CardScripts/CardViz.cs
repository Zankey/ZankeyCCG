using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZCCG
{
    public class CardViz : MonoBehaviour
    {   
        public Card card;
        public CardVizProperties[] properties;
        public GameObject statsHolder;
        public GameObject clanTag; 
        public Image heart;
        public Image shield;
        public Image frozen;
        public Image divineShield;
        public Image asleep;
        public Image deathrattle;
        public Image specialAbility;

        //parses through all elements on the Card ScriptableObject and loads them dynamically
        public void LoadCard(Card c)
        {
            if (c == null)
                return;

            card = c;

            c.cardType.OnSetType(this);

            for (int i = 0; i < c.properties.Length; i++)
            {
                CardProperties cp = c.properties[i];

                CardVizProperties p = GetProperty(cp.element);

                if(p == null)
                    continue;

                if(cp.element is ElementInt)
                {
                    p.tmProText.text = cp.intValue.ToString(); //converts the input of the card cost/attack/health int on the ScriptableObject to TMP text on the template
                }

                if (cp.tag is ElementTag)
                {
                    p.tag = cp.tag.tag;
                    Debug.Log("Setting p.tag = " + cp.tag.tag);
                }

                if(cp.element is ElementText)
                {
                    p.tmProText.text = cp.stringValue;
                }
                else
                if(cp.element is ElementImage)
                {
                    p.img.sprite = cp.sprite;
                }

            }      
        }

        public CardVizProperties GetProperty(Element e)
        {
            CardVizProperties result = null;

            for (int i = 0; i < properties.Length; i++)
            {
                if(properties[i].element == e)
                {
                    result = properties[i];
                    break;
                }
            }

            return result;
        }


        //TagChecker
        public bool CheckTags(string t)
        {
            for (int i = 0; i < this.properties.Length; i++)
            {
                if(this.properties[i].tag != null && this.properties[i].tag == t)
                {
                    return true;
                }
            }
            return false;
        }
    
    }
}