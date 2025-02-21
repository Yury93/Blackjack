﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dealerScoreText;
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
    public int totalBet;
   
    private int playerBet;
    private int maxBet;
    public bool bet;
    private float timeActive_mainText;
    //Animations button
   public  ButtonAnimator betBtnAnimator;
    public Action OnDeal, OnHit, OnStend, OnWin, OnLose, OnBet, OnDraw, OnEmptyBalance;
    [SerializeField] private CardScript cardDealer1;
    public static GameTimeLine instance;
    private void Start()
    {
        instance = this;
        //timelineSettings
        totalBet = timeLineSetting.totalBet;
        playerBet = timeLineSetting.playerBet;
        newBetChip.text = "$" + playerBet.ToString();
        maxBet = timeLineSetting.maxBet;
        timeActive_mainText = timeLineSetting.timeActive_mainText;
        cashText.text = "$" + playerScript.GetMoney().ToString();
        // Add on click listeners to the buttons
        betBtn.onClick.AddListener(() => BetClicked());
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtnAnimator = new ButtonAnimator(betBtn);

        ButtonBlocker.ButtonSetActive(bet, standBtn, hitBtn, dealBtn);
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
            scoreText.text = /*"PLAYER'S PAW: " +*/ playerScript.handValue.ToString();
            dealerScoreText.text = /*"Paw: "*/  dealerScript.handValue.ToString();

            if (hideCard.gameObject.activeSelf)
            {
                dealerScoreText.text =/* "DEALER'S PAW: " +*/ (dealerScript.handValue - cardDealer1.value).ToString();
            }
            else
            {
                dealerScoreText.text = /*"DEALER'S PAW: " +*/ dealerScript.handValue.ToString();
            }
            // Place card back on dealer card, hide card
            SetActiveHideCard(true);
            // Adjust buttons visibility
            dealBtn.gameObject.SetActive(false);
            hitBtn.gameObject.SetActive(true);
            standBtn.gameObject.SetActive(true);
            standBtnText.text = "Stand";
            PlaceBet();
        }
        //dealerScoreText.text = "Paw: " + dealerScript.handValue.ToString();
    }
    public bool SetActiveHideCard( bool enabl)
    {
        if (enabl)
        {
            dealerScript.hand[0].gameObject.SetActive( false);
            hideCard.gameObject.SetActive(true);
            return true;
        }
        else
        {
            dealerScript.hand[0].gameObject.SetActive(true);
            hideCard.gameObject.SetActive(false);
            return false;
        }
    }
    public void PlaceBet()
    {
        if (playerBet <= playerScript.GetMoney() && 0 < playerScript.GetMoney() && (totalBet / 2) <= maxBet)
        {
            //totalBet = totalBet * 2;
            betsText.text = " $" + (totalBet / 2).ToString();//bet txt
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
            scoreText.text = /*"PLAYER'S PAW: " +*/ playerScript.handValue.ToString();
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
            while (dealerScript.handValue < 17 && dealerScript.cardIndex < 10)
            {
                if (hideCard.activeSelf)
                {
                    yield return new WaitForSeconds(0.5f);
                    SetActiveHideCard(false);
                }
                yield return new WaitForSeconds(0.5f);
                dealerScript.GetCard();
                
                dealerScoreText.text = /*"DEALER'S PAW: " +*/ dealerScript.handValue.ToString();
                if (dealerScript.handValue > 20)
                {
                    RoundOver();
                }
            }
           
            standBtn.interactable = true;
            if (standClicks == 1)
            {
                StandClicked();
            }
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
        dealerScoreText.text = /*"DEALER'S PAW: " + */dealerScript.handValue.ToString();
        if (roundOver)
        {

            hitBtn.gameObject.SetActive(true);
            standBtn.gameObject.SetActive(true);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            SetActiveHideCard(false);
            standClicks = 0;
            playerBet = 0;
            totalBet = 0;
            bet = false;
            print("Конец раунда");
            cashText.text = "$" + playerScript.GetMoney().ToString();
            betsText.text = " $" + (totalBet / 2).ToString();//bet txt
            cashText.text = "$" + playerScript.GetMoney().ToString();
            ButtonBlocker.ButtonSetActive(bet, standBtn, hitBtn, dealBtn);
            

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
       
        if (playerBet <= playerScript.GetMoney() 
            &&  0 < playerScript.GetMoney() 
            && (totalBet/2) <= maxBet)
        {
            playerScript.AdjustMoney(-playerBet);
           
            cashText.text = "$" + playerScript.GetMoney().ToString();
            totalBet += (playerBet * 2);
            betsText.text = " $" + (totalBet/2).ToString();//bet txt
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
        //yield return new WaitForSeconds(timeActive_mainText);
        //mainText.text = " ";
        yield return new WaitForSeconds(timeActive_mainText);
        mainText.gameObject.SetActive(false);


        playerScript.ResetHand();
        dealerScript.ResetHand();
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();

        for (int i = 0; i < playerScript.hand.Length; i++)
        {
            playerScript.hand[i].GetComponent<CardScript>().SetBackSpriteCard();
            if (i <= 1)
                playerScript.hand[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < dealerScript.hand.Length; i++)
        {
            dealerScript.hand[i].GetComponent<CardScript>().SetBackSpriteCard();
            if(i<=1)
            dealerScript.hand[i].gameObject.SetActive(true);
        }
        SetActiveHideCard(true);
        scoreText.text = /*"PLAYER'S PAW: " +*/ 0.ToString();
        dealerScoreText.text = /*"PLAYER'S PAW: " +*/ 0.ToString();
        yield return new WaitForSecondsRealtime(1f);
        var selctor = FindObjectOfType<UrlSelector>();
        if(selctor.activeSelf == false)
        betBtnAnimator.ButtonEnable();
       
    }
}
