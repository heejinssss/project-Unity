using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

/* MonoBehaviour 상속받지 않았으므로 Hierarchy에 존재할 필요 없음 */
public class DBManager
{

    /************************************ DATABASE 연결 ************************************/
    /***************************************************************************************/

    public static MySqlConnection SqlConnection;

    /* static이 아니면 커넥션 불가 */
    private static readonly string ipAddress = "3.36.49.5";
    private static readonly string dbId = "root";
    private static readonly string dbPassword = "qogmlwls123!";
    private static readonly string dbName = "bhjDB";
    private static readonly string playerTable = "playerInfo";
    private static readonly string playingTable = "playingInfo";

    private readonly string strConnection = string.Format(
        "server={0};uid={1};pwd={2};database={3};charset=utf8mb4;",
        ipAddress, dbId, dbPassword, dbName);

    /* 싱글톤 인스턴스 */
    private static DBManager instance;

    /* 인스턴스 Getter */
    public static DBManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DBManager();
            }
            return instance;
        }
    }

    /* 클래스 생성자 */
    public DBManager()
    {
        try
        {
            SqlConnection = new MySqlConnection(strConnection);
            Debug.Log("데이터베이스 연결 : " + SqlConnection);

        }
        catch (System.Exception e)
        {
            Debug.Log("데이터베이스 연결 실패 : " + e.ToString());
            
        }
    }

    /************************************ DATABASE 관리 ************************************/
    /***************************************************************************************/

    /************************************ 외부에서 호출할 메소드 (public) ************************************/
    /*********************************************************************************************************/

    /*** 닉네임 입력 시 동작할 메소드 ***/
    public bool InputNickname(string nickname)
    {
        if (ExistNickname(nickname))
        {
            NicknameManager.Nickname = nickname;
            return true;
        }
        else if(InsertNickname(nickname))
        {
            NicknameManager.Nickname = nickname;
            return true;
        }
        return false;
    }

    /*** 게임 시작 시 동작할 메소드 (첫번째 맵 시작 시에 호출) ***/
    public bool StartGame(int roundNum, string playerName)
    {
        if(GetPlayingInfo(roundNum, playerName)!=null)
        {
            if (UpdatePlayingInfo(roundNum, playerName, 0, 0)) return true;
            return false;
        }
        if (InsertPlayingInfo(roundNum, playerName)) return true;
        return false;
    }

    /*** 장면 전환 시 동작할 메소드 (첫번째 맵 제외 각 맵 시작 시마다 호출) ***/
    public PlayingClass StartScene(int roundNum, string playerName)
    {
        return GetPlayingInfo(roundNum, playerName);
    }

    /*** 장면 전환 시 동작할 메소드 (각 맵 종료 시마다 호출) ***/
    public bool ChangeScene(int roundNum, string playerName, int score, int playTime)
    {
        //PlayingClass playing=GetPlayingInfo(roundNum, playerName);
        //if (playing==null) return false;

        //score += playing.getScore();
        //playTime+= playing.getPlayTime();

        if (UpdatePlayingInfo(roundNum, playerName, score, playTime)) return true;
        return false;
    }

    /*** 게임 종료 시 동작할 메소드 (마지막 맵 종료 시에 호출) ***/
    public bool EndGame(int roundNum, string playerName, int score, int playTime)
    {
        PlayerClass player = GetPlayerInfo(playerName);
        if (player==null) return false;

        score+= player.getScore();
        playTime += player.getPlayTime();

        if (UpdatePlayerInfo(playerName, score, playTime) && DeletePlayingInfo(roundNum, playerName)) return true;
        return false;
    }

    /*** 랭킹 조회 시 동작할 메소드 ***/
    public List<PlayerRankingClass> ShowRanking()
    {
        return Select5Players();
    }

    /************************************ 클래스 내부에서 호출할 메소드 (private) ************************************/
    /*****************************************************************************************************************/

    /* 닉네임 존재 확인 메소드 */
    private bool ExistNickname(string nickname)
    {
        string query = "select nickname from "+playerTable+" where nickname = \""+nickname+"\"";
        Debug.Log("닉네임 조회 쿼리 :: " + query);

        if (SelectRequest(query).Tables[0].Rows.Count != 0) return true;
        return false;
    }

    /* 닉네임 저장 메소드 */
    private bool InsertNickname(string nickname)
    {
        string query = "insert into " + playerTable + "(nickname) value(\"" + nickname + "\")";
        Debug.Log("닉네임 저장 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 조회 메소드 */
    private PlayingClass GetPlayingInfo(int roundNum, string playerName)
    {
        string query = "select score, playTime from " + playingTable + " where roundNum=" + roundNum + " and playerName=\"" + playerName + "\"";
        Debug.Log("플레잉 기록 조회 쿼리 :: " + query);

        DataSet dataSet = SelectRequest(query);
        if (dataSet != null)
        { 
            DataRow row = dataSet.Tables[0].Rows[0];
            int score = Convert.ToInt32(row["score"]);
            int playTime = Convert.ToInt32(row["playTime"]);

            PlayingClass playing = new PlayingClass(score,playTime);
            Debug.Log("조회된 플레잉 기록 :: " + playing);

            return playing;
        }
        return null;
    }

    /* 플레잉 기록 초기 데이터 삽입 메소드 */
    private bool InsertPlayingInfo(int roundNum, string playerName)
    {
        string query = "insert into " + playingTable + "(roundNum, playerName) value(" + roundNum + ",\"" + playerName + "\")";
        Debug.Log("플레잉 기록 초기화 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 업데이트 메소드, 초기화 시에도 사용 */
    private bool UpdatePlayingInfo(int roundNum, string playerName, int score, int playTime)
    {
        string query = "update " + playingTable + " set score=" + score + ", playTime=" + playTime + " where roundNum="+roundNum+" and playerName=\"" + playerName + "\"";
        Debug.Log("플레잉 기록 업데이트 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레이어 기록 조회 메소드 */
    private PlayerClass GetPlayerInfo(string nickname)
    {
        string query = "select score, playTime from " + playerTable + " where nickname=\"" + nickname + "\"";
        Debug.Log("플레이어 기록 조회 쿼리 :: " + query);

        DataSet dataSet = SelectRequest(query);
        if (dataSet != null)
        {
            DataRow row = dataSet.Tables[playerTable].Rows[0];
            int score = row.Field<int>("score");
            int playTime = row.Field<int>("playTime");

            PlayerClass player = new PlayerClass(score, playTime);
            Debug.Log("조회된 플레이어 기록 :: " + player);

            return player;
        }
        return null;
    }

    /* 플레이어 기록 업데이트 메소드 */
    private bool UpdatePlayerInfo(string nickname, int score, int playTime)
    {
        string query = "update " + playerTable + " set score=" + score + ", playTime=" + playTime + " where nickname=\"" + nickname + "\"";
        Debug.Log("플레이어 기록 업데이트 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 삭제 메소드 */
    private bool DeletePlayingInfo(int roundNum, string playerName)
    {
        string query="delete from "+playingTable+" where roundNum="+roundNum+" and playerName=\""+playerName+"\"";
        Debug.Log("플레잉 기록 삭제 쿼리 :: " + query);

        if(InsertOrUpdateRequest(query)) return true;
        return false;
    }


    /* 플레이어 상위 5개 랭킹 조회 메소드 */
    private List<PlayerRankingClass> Select5Players()
    {
        string query = "select rank() over (order by score desc, playTime asc) as 'rank',nickname,score,playTime,saveTime from playerInfo limit 5";
        Debug.Log("플레이어 랭킹 조회 쿼리 :: " + query);

        DataSet dataSet = SelectRequest(query);
        if (dataSet != null)
        {
            List<PlayerRankingClass> players = new List<PlayerRankingClass>();

            int length = dataSet.Tables[playerTable].Rows.Count;
            for (int i = 0; i < length; i++)
            {
                DataRow row = dataSet.Tables[playerTable].Rows[i];
                Debug.Log(row[0] + "" + row[1] + row[2] + row[3] + row[4]);

                long rank = row.Field<long>("rank");
                string nickname = row.Field<string>("nickname");
                int score = row.Field<int>("score");
                int playTime = row.Field<int>("playTime");
                DateTime saveTime = row.Field<DateTime>("saveTime");

                PlayerRankingClass player = new PlayerRankingClass(rank, nickname, score, playTime, saveTime);

                Debug.Log("조회된 플레이어 :: " + player);

                players.Add(player);
            }
            return players;
        }
        else return null;
    }

    /************************************ 공용 메소드 ************************************/
    /*************************************************************************************/

    /* (공용) 저장 전용 메소드 */
    private bool InsertOrUpdateRequest(string pQuery)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConnection;
            cmd.CommandText = pQuery;

            if ((SqlConnection == null) || (SqlConnection.State != ConnectionState.Open))
            {
                SqlConnection.Open();
            }

            cmd.ExecuteNonQuery();

            SqlConnection.Close();

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log("저장 실패 :: " + e.ToString());
            return false;
        }
    }

    /* (공용) 조회 전용 메소드 */
    private DataSet SelectRequest(string pQuery)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConnection;
            cmd.CommandText = pQuery;

            if ((SqlConnection == null) || (SqlConnection.State != ConnectionState.Open))
            {
                SqlConnection.Open();
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, playerTable);

            SqlConnection.Close();

            return dataSet;
        }
        catch (System.Exception e)
        {
            Debug.Log("조회 실패 :: " + e.ToString());
            return null;
        }
    }

    /* 연결 해제 메소드 */
    public void OnApplicationQuit()
    {
        if (SqlConnection != null && SqlConnection.State != ConnectionState.Closed)
        {
            SqlConnection.Close();
        }
    }
}