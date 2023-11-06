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
        List<PlayerClass> players = DBManager.Instance.SelectPlayers();

        for (int i = 0; i < 5; i++)
        {
            if (players[i] == null) break;//5인 이하일 경우 종료

            StringBuilder str = new StringBuilder();
            str.Append(players[i].getRank()).Append(" ")
                .Append(players[i].getNickname()).Append(" ")
                .Append(players[i].getScore()).Append(" ")
                .Append(players[i].getPlayTime()).Append(" ")
                .Append(players[i].getSaveTime()).Append(" ");

            rank[i].text = str.ToString();
        }
    }

}