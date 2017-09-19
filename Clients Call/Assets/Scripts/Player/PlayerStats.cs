using System;
using UnityEngine;

public class PlayerStats {
    // score
    private int _score;
    private int _itemsPickedUp; // Single-player stat
    // boost
    private int _totalAmountOfTimesBoosted;
    private int _amountOfTimesBoostedByOneWay;
    private int _amountOfTimesBoostedByMulti;
    // air time 
    private  float _airTimeInSeconds;
    // hitting opponent
    private  int _amountOfTimeHitYourOpponent;
    // Amount of meters traveled
    private float _totalAmountOfMetersTravelled;
    // high speed
    private float _highestVelocity;

    // score
    public int Score {
        get { return _score; }
        set {
            if (_score < 9) {
                _score = value;
            }
        }
    }
    public int ItemsPickedUp {
        get { return _itemsPickedUp; }
        set { _itemsPickedUp = value; }
    }
    //boost
    public int TotalAmountBoosted {
        get { return _totalAmountOfTimesBoosted; }
        set { _totalAmountOfTimesBoosted = value; }
    }
    public int AmountBoostedOneWay {
        get { return _amountOfTimesBoostedByOneWay; }
        set { _amountOfTimesBoostedByOneWay = value; }
    }
    public int AmountBoostedMulti {
        get { return _amountOfTimesBoostedByMulti; }
        set { _amountOfTimesBoostedByMulti = value; }
    }
    // hitting opponent
    public int AmountOfTimeHitOpponent {
        get { return _amountOfTimeHitYourOpponent; }
        set { _amountOfTimeHitYourOpponent = value; }
    }
    // air time
    public float AirTimeInSeconds {
        get {
            return (float) Math.Round(_airTimeInSeconds, 2);
        }
        set { _airTimeInSeconds = value; }
    }
    // max speed
    public float HighestVelocity {
        get { return (float) Math.Round(_highestVelocity, 2); }
        set { _highestVelocity = value; }
    }
    
    // Amount of meters traveled
    public float TotalAmountOfMetersTravelled {
        get {
            return (float) Math.Round(_totalAmountOfMetersTravelled, 2);
        }
        set { _totalAmountOfMetersTravelled = value; }
    }
}