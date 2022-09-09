using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameTimeLine : MonoBehaviour
{
    // Game Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

    private int standClicks = 0;

    // Access the player and dealer's script
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    // public Text to access and update - hud
    public Text scoreText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    public Text mainText;
    public Text standBtnText;
    public Text newBetChip;
    // Card hiding dealer's 2nd card
    public GameObject hideCard;

    // How much is bet
    [SerializeField] private TimeLineSetting timeLineSetting;
    public TimeLineSetting GetSettings => timeLineSetting;
    private int totalBet;
    private int playerBet;
    private int maxBet;
    private bool bet;
    private float timeActive_mainText;
    //Animations button
    private ButtonAnimator betBtnAnimator;
    public Action OnDeal, OnHit, OnStend, OnWin, OnLose, OnBet, OnDraw, OnEmptyBalance;
    [SerializeField] private CardScript cardDealer1;

    private void Start()
    {
        //timelineSettings
        totalBet = timeLineSetting.totalBet;
        playerBet = timeLineSetting.playerBet;
        newBetChip.text = "$" + playerBet.ToString();
        maxBet = timeLineSetting.maxBet;
        timeActive_mainText = timeLineSetting.timeActive_mainText;
        cashText.text = "$"+playerScript.GetMoney().ToString();
        // Add on click listeners to the buttons
        betBtn.onClick.AddListener(() => BetClicked());
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtnAnimator = new ButtonAnimator(betBtn);

        ButtonBlocker.ButtonSetActive(bet, standBtn, hitBtn,dealBtn);
        betBtnAnimator.ButtonEnable();
    }


    private void DealClicked()
    {
        
        if (bet)
        {
            OnDeal?.Invoke();
            betBtnAnimator.ButtonDisabled();
            ButtonBlocker.ButtonSetActive(true, standBtn, hitBtn);
            // Reset round, hide text, prep for new hand
            playerScript.ResetHand();
            dealerScript.ResetHand();
            // Hide deal hand score at start of deal
            //dealerScoreText.gameObject.SetActive(false);
            mainText.gameObject.SetActive(false);
            //dealerScoreText.gameObject.SetActive(false);
            GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
            playerScript.StartHand();
            dealerScript.StartHand();
            // Update the scores displayed
            scoreText.text = "Paw: " + playerScript.handValue.ToString();
            dealerScoreText.text = "Paw: " + dealerScript.handValue.ToString();

            if (hideCard.GetComponent<Renderer>().enabled)
            {
                dealerScoreText.text = "Paw: " + (dealerScript.handValue - cardDealer1.value).ToString();
            }
            else
            {
                dealerScoreText.text = "Paw: " + dealerScript.handValue.ToString();
            }
            // Place card back on dealer card, hide card
            hideCard.GetComponent<Renderer>().enabled = true;
            // Adjust buttons visibility
            dealBtn.gameObject.SetActive(false);
            hitBtn.gameObject.SetActive(true);
            standBtn.gameObject.SetActive(true);
            standBtnText.text = "Stand";
            PlaceBet();
        }
        //dealerScoreText.text = "Paw: " + dealerScript.handValue.ToString();
    }
    public void PlaceBet()
    {
        if (playerBet <= playerScript.GetMoney() && 0 < playerScript.GetMoney() && (totalBet / 2) <= maxBet)
        {
            //totalBet = totalBet * 2;
            betsText.text = " $" + totalBet.ToString();//bet txt
            dealerScript.AdjustMoney(-(playerBet));
            print(playerBet);
            cashText.text = "$" + playerScript.GetMoney().ToString();
        }
    }
    

    private void HitClicked()
    {
        
        // Check that there is still room on the table
        if (playerScript.cardIndex <= 10)
        {
            OnHit?.Invoke();
            playerScript.GetCard();
            scoreText.text = "Paw: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        OnStend?.Invoke();
        standClicks++;
        if (standClicks > 1)
        {
            RoundOver();
        }
        HitDealer();
        standBtnText.text = "Call";
    }

    private void HitDealer()
    {
        StartCoroutine(CorDelayGetCardDealer());
        IEnumerator CorDelayGetCardDealer()
        {
            standBtn.interactable = false;
            while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
            {
                if (hideCard.GetComponent<Renderer>().enabled)
                {
                    yield return new WaitForSeconds(0.5f);
                    hideCard.GetComponent<Renderer>().enabled = false;
                }
                yield return new WaitForSeconds(0.5f);
                dealerScript.GetCard();
                
                dealerScoreText.text = "Paw: " + dealerScript.handValue.ToString();
                if (dealerScript.handValue > 20) RoundOver();
            }
            standBtn.interactable = true;
        }
    }

    // Check for winnner and loser, hand is over
    void RoundOver()
    {
        // Booleans (true/false) for bust and blackjack/21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        // If stand has been clicked less than twice, no 21s or busts, quit function
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;
        // All bust, bets returned
        if (playerBust && dealerBust)
        {
            OnDraw?.Invoke();
            mainText.text = "All Bust: Bets returned";
            playerScript.AdjustMoney(totalBet / 2);
        }
        // if player busts, dealer didnt, or if dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            OnLose?.Invoke();
            mainText.text = "Dealer wins!";
        }
        // if dealer busts, player didnt, or player has more points, player wins
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            OnWin?.Invoke();
            mainText.text = "You win!";
            playerScript.AdjustMoney(totalBet);
        }
        //Check for tie, return bets
        else if (playerScript.handValue == dealerScript.handValue)
        {
            OnDraw?.Invoke();
            mainText.text = "Push: Bets returned";
            playerScript.AdjustMoney(totalBet / 2);
        }
        //else
        //{
        //    roundOver = false;
        //}
        // Set ui up for next move / hand / turn
       
        if (roundOver)
        {

            hitBtn.gameObject.SetActive(true);
            standBtn.gameObject.SetActive(true);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            standClicks = 0;
            playerBet = 0;
            totalBet = 0;
            bet = false;
            print("Конец раунда");
            cashText.text = "$" + playerScript.GetMoney().ToString();
            betsText.text = " $" + totalBet.ToString();//bet txt
            cashText.text = "$" + playerScript.GetMoney().ToString();
            ButtonBlocker.ButtonSetActive(bet, standBtn, hitBtn, dealBtn);
            betBtnAnimator.ButtonEnable();

            if (playerScript.GetMoney() <= 0)
            {
                OnEmptyBalance?.Invoke();
            }
        }
        StartCoroutine(CorEmptyBetMainTxt());
    }

    // Add money to pot if bet clicked
    void BetClicked()
    {
        Text newBet = betBtn.GetComponentInChildren(typeof(Text)) as Text;
        playerBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        OnBet?.Invoke();
       
        if (playerBet <= playerScript.GetMoney() &&  0 < playerScript.GetMoney() 
            && (totalBet/2) <= maxBet)
        {
            playerScript.AdjustMoney(-playerBet);
           
            cashText.text = "$" + playerScript.GetMoney().ToString();
            totalBet += (playerBet * 2);
            betsText.text = " $" + totalBet.ToString();//bet txt
            bet = true;

            ButtonBlocker.ButtonSetActive(true,dealBtn);
            return;
        }
        if (playerScript.GetMoney() <=  0 && !bet)
        {
            betBtnAnimator.ButtonDisabled();
            OnEmptyBalance?.Invoke();
            
        }
    
    }

    IEnumerator CorEmptyBetMainTxt()
    {
        yield return new WaitForSeconds(timeActive_mainText);
        mainText.text = "We need to make" + "\n" + " a new bet!";
        yield return new WaitForSeconds(timeActive_mainText);
        mainText.gameObject.SetActive(false);



    }
}
