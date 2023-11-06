using System;

/* 랭킹 출력할 플레이어 클래스 */
public class PlayerClass
{
    private long rank;
    private string nickname;
    private int score;
    private int playTime;
    private DateTime saveTime;

    public PlayerClass() { }

    public PlayerClass(long rank, string nickname, int score, int playTime, DateTime saveTime)
    {
        this.rank = rank;
        this.nickname = nickname;
        this.score = score;
        this.playTime = playTime;
        this.saveTime = saveTime;
    }

    public long getRank() { return rank; }
    public string getNickname() { return nickname; }
    public int getScore() { return score; }
    public int getPlayTime() { return playTime; }
    public DateTime getSaveTime() { return saveTime; }
}