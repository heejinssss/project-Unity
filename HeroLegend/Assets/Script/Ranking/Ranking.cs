using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public Text[] rank = new Text[5];
    public Text[] name=new Text[5];
    public Text[] score = new Text[5];
    public Text[] playTime= new Text[5];


    public void Start()
    {
        List<PlayerRankingClass> players = DBManager.Instance.ShowRanking();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == null) break;//5인 이하일 경우 종료

            rank[i].text = Convert.ToString(i+1);
            name[i].text = players[i].getNickname();
            score[i].text = Convert.ToString(players[i].getScore());
            playTime[i].text = Convert.ToString(players[i].getPlayTime());

        }
    }

}