using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingClass
{
    private int score;
    private int playTime;

    public PlayingClass() { }

    public PlayingClass(int score, int playTime)
    {
        this.score = score;
        this.playTime = playTime;
    }

    public int getScore() { return score; }
    public int getPlayTime() {  return playTime; }
}
