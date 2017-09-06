using UnityEngine;

public class PlayerStats : MonoBehaviour {
    // score
    private int _score;
    // boost
    private int _totalAmountOfTimesBoosted;
    private int _amountOfTimesBoostedByOneWay;
    private int _amountOfTimesBoostedByMulti;
    // air time 
    private int _airTimeInSeconds;
    // hitting opponent
    private int _amountOfTimeHitYourOpponent;
    // single round time
    private int _shortestRoundInSec;
    private int _longestRoundInSec;

    // score
    public int Score {
        get { return _score; }
        set { _score = value; }
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
    // air time
    public int AirTimeInSeconds {
        get { return _airTimeInSeconds; }
        set { _airTimeInSeconds = value; }
    }
    // hiting opponent
    public int AmountOfTimeHit {
        get { return _amountOfTimeHitYourOpponent; }
        set { _amountOfTimeHitYourOpponent = value; }
    }
    // single round time
    public int ShortestRoundInSec {
        get { return _shortestRoundInSec; }
        set { _shortestRoundInSec = value; }
    }
    public int LongestRoundInSec {
        get { return _longestRoundInSec; }
        set { _longestRoundInSec = value; }
    }
}