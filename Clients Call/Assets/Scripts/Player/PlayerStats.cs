using UnityEngine;

public class PlayerStats {
    // score
    private int _score;
    private int _itemsPickedUp;
    // boost
    private int _totalAmountOfTimesBoosted;
    private int _amountOfTimesBoostedByOneWay;
    private int _amountOfTimesBoostedByMulti;
    // air time 
    private  float _airTimeInSeconds;
    // hitting opponent
    private  int _amountOfTimeHitYourOpponent;

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
    // air time
    public float AirTimeInSeconds {
        get { return _airTimeInSeconds; }
        set { _airTimeInSeconds = value; }
    }
    // hiting opponent
    public int AmountOfTimeHit {
        get { return _amountOfTimeHitYourOpponent; }
        set { _amountOfTimeHitYourOpponent = value; }
    }
}