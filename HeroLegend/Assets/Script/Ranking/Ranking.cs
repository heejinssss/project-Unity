using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public Text[] rank = new Text[5];


    public void Start()
    {
        List<PlayerRankingClass> players = DBManager.Instance.ShowRanking();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == null) break;//5인 이하일 경우 종료

            StringBuilder str = new StringBuilder();
            str.Append(i+1).Append(" ")
                .Append(players[i].getNickname()).Append(" ")
                .Append(players[i].getScore()).Append(" ")
                .Append(players[i].getPlayTime()).Append(" ");

            rank[i].text = str.ToString();
        }
    }

}