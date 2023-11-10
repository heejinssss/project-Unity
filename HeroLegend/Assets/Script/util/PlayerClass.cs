using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
    private int score;
    private int playTime;

    public PlayerClass() { }

    public PlayerClass(int score, int playTime)
    {
        this.score = score;
        this.playTime = playTime;
    }

    public int getScore() { return score; }
    public int getPlayTime() {  return playTime; }
}
