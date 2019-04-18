using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    protected override void Init()
    {
    }


    public int bestScore;
    public int score;
    public int scoreUndercook;
    public int scoreWelldone;
    public int scoreOvercook;
    public int scoreBurned;

    public float roundTimer;
    public bool isSpawnEnd;
    public bool isRoundEnd;

    private float _roundTimer;
    private GameObject popcornList;

    // Use this for initialization
    void Start () {
        _roundTimer = roundTimer;
        SetDefaultParameter();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isSpawnEnd == false)
        {
            StartRoundTimer();
        }
        else if (isSpawnEnd == true)
        {
            ListPopcorn();
        }
    }

    void StartRoundTimer()
    {
        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
        }
        if (roundTimer <= 0)
        {
            isSpawnEnd = true;
        }
    }

    public void SetBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }
    }

    public void SetDefaultParameter()
    {
        score = 0;
        scoreUndercook = 0;
        scoreWelldone = 0;
        scoreOvercook = 0;
        scoreBurned = 0;
        isRoundEnd = false;
        isSpawnEnd = false;
        roundTimer = _roundTimer;
    }
    public void ListPopcorn()
    {
        popcornList = GameObject.FindGameObjectWithTag("Popcorn");
        if (popcornList == null)
        {
            isRoundEnd = true;
        }
        //popcornList = GameObject.FindGameObjectsWithTag("Popcorn");
        //for (int i = 0; i < popcornList.Length; i++)
        //{
        //    //Destroy(popcornList[i]);
        //}
    }
}
