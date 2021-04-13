using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZCCG.GameStates;

namespace ZCCG
{

    public class GameManager : MonoBehaviour
    {
        [System.NonSerialized]
        public PlayerHolder[] all_players;
        public PlayerHolder currentPlayer;
        public PlayerHolder otherPlayer;

        public CardHolders playerCardHolder;
        public CardHolders otherCardHolder;

        public State currentState;
        public GameObject cardPrefab;
        public GameObject turnButton;

        public LineRenderer targetingLine;

        public CurrentSelectedHolder currentSelectedHolder;  //reference to current card or hero 

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public SO.StringVariable turnText;

        public HeroManager[] heroManagers;
        public HeroPowerHolder[] heroPowerHolders;

        public static GameManager singleton;

        private void  Awake()
        {
            singleton = this;

            all_players = new PlayerHolder[turns.Length];
            for (int i = 0; i < turns.Length; i++)
            {
                all_players[i] = turns[i].player;
            }

            currentPlayer = turns[0].player;
            otherPlayer = turns[1].player;

        }

        private void Start()
        {
            Settings.gameManager = this;

            SetupPlayers();

            CreateDeck();

            // SetOriginalTags(currentPlayer);
            // SetOriginalTags(otherPlayer);

            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }


        void SetupPlayers()
        {
            foreach (PlayerHolder p in all_players)
            {
                if (p.isHumanPlayer)
                {
                    p.currentHolder = playerCardHolder;
                }
                else
                {
                    p.currentHolder = otherCardHolder;
                }
            }  
        }

        //DeckLoader
        void CreateDeck()
        {
            ResourcesManager rm = Settings.GetResourcesManager();

            for (int p = 0; p < all_players.Length; p++)
            {
                for (int i = 0; i < all_players[p].startingDeck.Length; i++)
                {
                    GameObject go = Instantiate(cardPrefab) as GameObject;
                    CardViz v = go.GetComponent<CardViz>();
                    v.LoadCard(rm.GetCardInstance(all_players[p].startingDeck[i]));
                    CardInstance inst = go.GetComponent<CardInstance>();
                    inst.SetOwner(all_players[p].username);
                    inst.currentLogic = all_players[p].handLogic;
                    Settings.SetParentForCard(go.transform, all_players[p].currentHolder.deckHolder.value);
                    all_players[p].deck.Add(inst);
                }
                Debug.Log("Created " + all_players[p].deck.Count + " cards for " + all_players[p].username);
            }

            
        }

        //This is temporary for player switching
        // public bool switchPlayer;

        private void Update() 
        {
            // if(switchPlayer)
            // {
            //     switchPlayer = false;

            //     playerCardHolder.LoadPlayer(all_players[0], heroManagers[0]);
            //     enemyCardHolder.LoadPlayer(all_players[1], heroManagers [1]);
            // }

            // Debug.Log("current player: "+ currentPlayer.username);
            // Debug.Log("current player's CardHolder: " + currentPlayer.currentHolder.name);
            // Debug.Log("current player's CardsDown List: " + currentPlayer.cardsDown.Count);

            // Debug.Log("current player has this many board cards : " + currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>().Length);

            bool isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                turnIndex++;
                if(turnIndex > turns.Length -1)
                {
                    turnIndex = 0;
                }

                // the current player has changed here
                otherPlayer = currentPlayer;
                currentPlayer = turns[turnIndex].player;
                

                turns[turnIndex].OnTurnStart();
                turnText.value = turns[turnIndex].player.username;
                onTurnChanged.Raise();
            }

            if(currentState != null)
            {
                currentState.Tick(Time.deltaTime);
                // Debug.Log(currentState);
            }


        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void EndCurrentPhase()
        {
            Settings.RegisterEvent(turns[turnIndex].name + " Finished", currentPlayer.playerColor);
            turns[turnIndex].EndCurrentPhase();
        }

        public void DrawTargetingLine(Vector3 selectedPosition)
        {
            targetingLine.gameObject.SetActive(true);

            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            screenPoint.z = 1; //distance of the plane from the camera
            Vector3 endPos = Camera.main.ScreenToWorldPoint(screenPoint);

            Vector3 startPos = new Vector3(selectedPosition.x, selectedPosition.y,selectedPosition.z - 1);

            targetingLine.SetPosition(0, startPos);
            targetingLine.SetPosition(1, endPos);

        }

        //removes all cards with zero Health, This should be handled in its own intermediate phase between actions. 
        // public void BoardCleanup()
        // {
        //     // Send enemies cards to GY first
        //     foreach (CardInstance inst in Settings.gameManager.otherPlayer.cardsDown)
        //     {
        //         inst.RemoveDeadCard();
        //     }
        //     // Send player's cards to GY second
        //     foreach (CardInstance inst in Settings.gameManager.currentPlayer.cardsDown)
        //     {
        //         inst.RemoveDeadCard();
        //     }
        // }

        public void DrawCard()
        {
            int randomIndex = Random.Range(0, currentPlayer.deck.Count);
            CardInstance randomCard = currentPlayer.deck[randomIndex];
            if(currentPlayer.handcards.Count <=9)
            {
                Settings.SetParentForCard(randomCard.transform, currentPlayer.currentHolder.handGrid.value);
                currentPlayer.handcards.Add(randomCard);
            }
            else
            {
                Settings.SetParentForCard(randomCard.transform, currentPlayer.currentHolder.graveyardHolder.value);
                Debug.Log(currentPlayer.username +"'s Hand is too full!! sending card to graveyard!");
            }
            currentPlayer.deck.Remove(randomCard);         
        }

        //TODO: similar to draw card, but could add graveyard/enemy hand, enemy deck, ect...

    }
        
}
