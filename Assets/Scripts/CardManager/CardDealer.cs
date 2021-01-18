using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    [System.Serializable]
    public struct CardFace
    {
        public Card_Suits suit;
        public Card_Numbers numbers;

        public CardFace(Card_Suits suit, Card_Numbers numbers)
        {
            this.suit = suit;
            this.numbers = numbers;
        }
    }

    [System.Serializable]
    public class CardFaceCoords
    {
        public CardFace cardFace;
        [SerializeField] private Vector2 _coord;
        public Vector2 coord
        {
            get { return new Vector2(_coord.y / 9, -(_coord.x / 6)); }
            set { _coord = value; }
        }
    }

    public class CardDealer : MonoBehaviour
    {
  
    public List<Card> cards;
    public List<Button> buttons;
    public int i = 1;
    public int a = 2;
    int b = -2;
    int z = 0;
    [SerializeField] Transform cardDispenserSpawn;
        [SerializeField] Animator cardDispenserAnimator;
        [SerializeField] GameObject cardPrefab;

        [SerializeField] List<CardFaceCoords> cardFaceCoords = new List<CardFaceCoords>();

     //   [SerializeField] Player dealer;
        [SerializeField] List<Player> players = new List<Player>();

        [SerializeField] List<CardFace> cardDeck = new List<CardFace>();
        [SerializeField] List<CardFace> cardDeckUsed = new List<CardFace>();

        bool cardSpawned = false;
        Card lastCard;
    int numbers = 0;
        [Header("Testing")]
       
        [SerializeField] Button testHit;
       

    

        void Awake()
        {
          //  dealer.SetAsDealer();
            ShuffleDeck();
            DealBegin();
        }

        void ShuffleDeck()
        {
            cardDeckUsed.Clear();
            // for (int i = 0; i < 6; i++) { //Shuffle in 6 decks
            foreach (Card_Suits suit in Enum.GetValues(typeof(Card_Suits)))
            {
                foreach (Card_Numbers number in Enum.GetValues(typeof(Card_Numbers)))
                {
                    cardDeck.Add(new CardFace(suit, number));
                }
            }
            // }
           
        }

        void DealCard(Player player,int j, bool left = true)
        {
  
        Debug.Log("Dealing Card to " + player.playerName);
            StartCoroutine(DealingCard(player, j, left));
        }

        public void SpawnCard()
        {
            //Triggered by animation event to ensure correct timing
            StartCoroutine(EndOfFrame());
        }

        CardFaceCoords GetCardFaceCoord(CardFace cardFace)
        {
            foreach (CardFaceCoords card in cardFaceCoords)
            {
                if (card.cardFace.suit == cardFace.suit && card.cardFace.numbers == cardFace.numbers)
                {
                Debug.Log(card);
                    return card;
                }
            }
            return null;
        }

        IEnumerator EndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            lastCard.transform.position = new Vector3(cardDispenserSpawn.position.x, cardDispenserSpawn.position.y, 0);
            lastCard.transform.rotation = cardDispenserSpawn.rotation;
            lastCard.transform.localScale = Vector3.one;

            cardSpawned = true;
        }

        IEnumerator DealingCard(Player player, int j, bool left = true)
        {
           // if ((left && !player.leftBust) || (!left && !player.rightBust))
            { //Check for bust

                lastCard = Instantiate(cardPrefab).GetComponent<Card>();
              //  Debug.Log($"1 DealingCard -- > last card | {lastCard} |?? ");
                lastCard.transform.localScale = Vector3.zero;
           
            //if (cardDeck.Count < 10)
              //  {
               //    ShuffleDeck();
               // }
          
           // CardFace randomCard = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count - 13)];
            CardFace randomCard = cardDeck[1];

            cardDeckUsed.Add(randomCard);
          
            lastCard.GetComponent<Card>().SetCard(randomCard, GetCardFaceCoord(randomCard));
         
           // cardDeck.Remove(randomCard);
   
            cardSpawned = false;
           
            cardDispenserAnimator.SetBool("Dealing", true);
            //Play dealing animation first
           
            cardDispenserAnimator.SetTrigger("Deal");
            //Card slides out to a point in front of the dispenser
            //  while (!cardSpawned)
            
            yield return null;
            // }
          
            cardDispenserAnimator.SetBool("Dealing", false);

            //Player takes card
            // Debug.Log($"2 DealingCard -- > Try Player add card | {lastCard} | ");
        
            player.AddCard(lastCard, left);
            numbers++;
            if (numbers >= 3)
            {
                numbers = 3;
            }
            if (j == 12345)
            {
                //numbers = 10000;
            }
            lastCard.setbuttonposition(numbers);
            }
        }

       /* void CalculateHands()
        {
            int dealerTotal = dealer.CalculateHands(out bool dealerBlackjack)[0]; //Dealer cannot split hands
            if (dealerTotal <= 16 && !dealerBlackjack)
            {
                Debug.Log($"Dealer {dealerTotal}, dealing another card");
                StartCoroutine(DealCards(1, 0f, 0.5f, dealer, true, true));
                return;
            }

            foreach (Player player in players)
            {
                List<int> handTotals = player.CalculateHands(out bool playerBlackjack);
                if (playerBlackjack || dealerBlackjack)
                {
                    if (dealerBlackjack && playerBlackjack)
                    {
                        Debug.Log($"{player.playerName} draw | Dealer hand {dealerTotal}");
                        //Return bet
                        player.Transfer(player.pot[0], 0);
                    }
                    else if (playerBlackjack)
                    {
                        Debug.Log($"{player.playerName} hand wins by Blackjack | Dealer hand {dealerTotal}");
                        //1.5 to 1 win
                        player.Transfer((int)(player.pot[0] + (player.pot[0] * 1.5f)), 0);
                    }
                    else
                    {
                        Debug.Log($"Dealer Blackjack! Players lose.");
                        //Lose bet
                    }
                }
                else
                {
                    int index = 0;
                    foreach (int handTotal in handTotals)
                    {
                        if ((handTotal > dealerTotal || dealerTotal > 21) && handTotal <= 21)
                        {
                            Debug.Log($"{player.playerName} hand wins {handTotal} | Dealer hand {dealerTotal}");
                            player.Transfer(player.pot[index] + player.pot[index], index);
                            //1 to 1 win
                        }
                        else
                        {
                            Debug.Log($"{player.playerName} loses {handTotal} | Dealer hand wins {dealerTotal}");
                            //Lose bet
                        }
                        index++;
                    }
                }

                player.pot[0] = 0;
                player.pot[1] = 0;
            }
        }*/
       public void mergeCard(Card card, Button button)
     {

        
       
        cards.Add(card);
        buttons.Add(button);
       
        foreach (Card Carda in cards)
        {
            
            //Debug.Log(z);
           // Debug.Log(cards.Count);
            b++;
          
            if(b>= 0)
            {
                if (cards[cards.Count -2] == null) { return; }
                Card cardbefore = cards[cards.Count - 2];
                
                while ((int)cardbefore.cardFace.numbers > 13)
                {
                cardbefore.cardFace.numbers -= 13;
                }
              
          
                 if (cards[cards.Count - 1] == null) { return; }
                Card cardnow = cards[cards.Count-1];
               
                while ((int)cardnow.cardFace.numbers > 13)
                {
                    cardnow.cardFace.numbers -= 13;
                }
                if (cardbefore.cardFace.numbers == cardnow.cardFace.numbers && buttons[buttons.Count - 2].GetComponent<Image>().color != Color.gray)
                {
                    Debug.Log($"try merge");

                    if (cardnow == null) { return; }
                    cardnow.GetComponent<Card>().destroy();
                    buttons[buttons.Count - 2].GetComponent<Image>().color = Color.gray;
                    buttons[buttons.Count - 2].tag = "Card(clone Gray)";
                    if (z == 1)
                    {
                        
                        buttons[buttons.Count - 2]
                        .transform.localPosition =
                        new Vector3(buttons[buttons.Count - 2]
                        .transform.localPosition.x - 100, -200, 0);
                    }
                    z++;
                    
                    bigMerge(cardbefore, buttons[buttons.Count - 2]);
                    //Debug.Log(b); Debug.Log(n);

                }

            }
        }
     }
    public void bigMerge(Card cardbefore, Button button)
    {
        Debug.Log(3);
        if (b >= 2)
        {
            Debug.Log(cards[cards.IndexOf(cardbefore) - 1]);
            if (cards[cards.IndexOf(cardbefore)-1] != null)
            {
                Card cardBeforeMore = cards[cards.IndexOf(cardbefore) - 1];
                Debug.Log(2);
                while ((int)cardBeforeMore.cardFace.numbers > 13)
                {
                    cardBeforeMore.cardFace.numbers -= 13;


                }
                if (cardbefore.cardFace.numbers == cardBeforeMore.cardFace.numbers && buttons[buttons.IndexOf(button) - 1].tag == "Card(clone Gray)")
                {
                    if (cardBeforeMore == null) { return; }
                    cardbefore.GetComponent<Card>().destroy();
                    buttons[buttons.Count - 1].GetComponent<Image>().color = Color.yellow;
                    Debug.Log($"try big merge");
                    Hitmerge();
                }
            }
            
           
        }
        Hitmerge();
    }
        IEnumerator DealCards(int numberOfCards, float delay, float waitTime, int j,Player player, bool left = true, bool reveal = false)
        {
            float currentWait = waitTime;

            while (delay > 0)
            {
                delay -= Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < numberOfCards; i++)
            {
                if (players.Count > 0)
            {
               
                DealCard(player,j, left);
                }

                currentWait = waitTime;
                while (currentWait > 0)
                {
                    currentWait -= Time.deltaTime;
                    yield return null;
                }
            }

            if (reveal)
            {
                //Reveal();
            }
            if (player.dealer)
            {
               
            }
            else
            {
                if (player.CanSplit())
                {
                   
                }
                else
                {
                    
                }
                
            }
        }

        /*
        ================
            TESTING
        ================
        */

        [ContextMenu("Play Blackjack")]
        public void DealBegin()
        {
       
            //Deal two cards to player
            StartCoroutine(DealCards(3, 0f, 0.5f, 1,players[0]));
          //  StartCoroutine(DealCards(2, 1f, 0.5f, dealer));

            players[0].Transfer(-2, 0);

            //testHit.gameObject.SetActive(false);

           
        }

        [ContextMenu("Split Player Hand")]
        public void Split()
        {
            if (players.Count > 0)
            {
                players[0].SplitHand();
                StartCoroutine(DealCards(7, 0f, 0.5f, 1,players[0], true));
                StartCoroutine(DealCards(7, 0.5f, 0.5f,1, players[0], false));

              //  testHit.gameObject.SetActive(false);
               // testHitLeft.gameObject.SetActive(true);
               // testHitRight.gameObject.SetActive(true);
                //testSplit.gameObject.SetActive(false);
            }
        }

        [ContextMenu("Hit")]
        public void Hit()
    {
        float Timer = 1;
        while (Timer > 0) { Timer -= Time.deltaTime; }
        
        
        //testSplit.gameObject.SetActive(false);
        
        StartCoroutine(DealCards(1, 0f, 0.5f, 1,players[0]));
        }
    public void Hitmerge()
    {
       
        //testSplit.gameObject.SetActive(false);

        StartCoroutine(DealCards(1, 0f, 0.5f, 12345, players[0]));
    }
    /*  [ContextMenu("Hit")]
      public void HitLeft()
      {
          StartCoroutine(DealCards(1, 0f, 0.5f, players[0], true));
      }

      [ContextMenu("Hit")]
      public void HitRight()
      {
          StartCoroutine(DealCards(1, 0f, 0.5f, players[0], false));
      }

      [ContextMenu("Reveal")]
      public void Reveal()
      {
          players.ForEach(player => player.Reveal());
          dealer.Reveal();
          CalculateHands();

      }

      [ContextMenu("Reset")]
      public void Reset()
      {
          players.ForEach(player => player.Reset());
          dealer.Reset();

      }*/

}
