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
    static readonly string ipAddress = "3.36.49.5";
    static readonly string dbId = "root";
    static readonly string dbPassword = "qogmlwls123!";
    static readonly string dbName = "bhjDB";
    static readonly string playerTable = "playerInfo";
    static readonly string playingTable = "playingInfo";

    readonly string strConnection = string.Format(
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

    /************************************ 시스템 호출할 5개 메소드 ************************************/
    /**************************************************************************************************/

    /*** 닉네임 입력 시 동작할 메소드 ***/
    public bool InputNickname(string nickname)
    {
        //닉네임 존재하는지 확인
        if (ExistNickname(nickname))
        {
            //전역변수 닉네임 세팅
            NicknameManager.Nickname = nickname;

            return true;
        }
        else
        {
            //새로운 닉네임 저장
            if(InsertNickname(nickname))
            {
                //전역변수 닉네임 세팅
                NicknameManager.Nickname = nickname;

                return true;
            }
        }
        return false;
    }

    /*** 게임 시작 시 동작할 메소드 (첫번째 맵 시작 시에 호출) ***/
    public bool StartGame(int roundNum, string playerName)
    {
        //플레잉 기록 초기화
        if (InitPlayingInfo(roundNum, playerName)) return true;
        return false;
    }

    /*** 장면 전환 시 동작할 메소드 (SAVE 모드 지원 위해 맵 종료 시마다 호출, 마지막 맵도 필요!) ***/
    public bool ChangeScene(int roundNum, string playerName, int score, int playTime)
    {
        //플레잉 기록 업데이트
        if (UpdatePlayingInfo(roundNum, playerName, score, playTime)) return true;
        return false;
    }

    /*** 게임 종료 시 동작할 메소드 (마지막 맵 종료 시에 호출) ***/
    public bool EndGame(int roundNum, string playerName, int score, int playTime)
    {
        //플레이어 기록 업데이트
        if(UpdatePlayerInfo(playerName, score, playTime))
        {
            //플레잉 기록 삭제
            if (DeletePlayingInfo(roundNum, playerName)) return true;
        }
        return false;
    }

    /*** 랭킹 조회 시 동작할 메소드 ***/
    public List<PlayerClass> ShowRanking()
    {
        return Select5Players();
    }

    /************************************ 클래스 내부에서 호출할 메소드 ************************************/
    /*******************************************************************************************************/

    /* 닉네임 조회 메소드 */
    public bool ExistNickname(string nickname)
    {
        string query = "select nickname from "+playerTable+" where nickname = \""+nickname+"\"";
        Debug.Log("닉네임 조회 쿼리 :: " + query);

        if (SelectRequest(query) != null) return true;
        return false;
    }

    /* 닉네임 저장 메소드 */
    public bool InsertNickname(string nickname)
    {
        string query = "insert into " + playerTable + "(nickname) value(\"" + nickname + "\")";
        Debug.Log("닉네임 저장 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 초기화 메소드 */
    public bool InitPlayingInfo(int roundNum, string playerName)
    {
        string query = "insert into " + playingTable + "(roundNum, playerName) value(" + roundNum + ",\"" + playerName + "\")";
        Debug.Log("플레잉 기록 초기화 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 업데이트 메소드 */
    public bool UpdatePlayingInfo(int roundNum, string playerName, int score, int playTime)
    {
        string query = "update " + playingTable + " set score=" + score + ", playTime=" + playTime + " where roundNum="+roundNum+" and playerName=\"" + playerName + "\"";
        Debug.Log("플레잉 기록 업데이트 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레이어 기록 업데이트 메소드 */
    public bool UpdatePlayerInfo(string nickname, int score, int playTime)
    {
        string query = "update " + playerTable + " set score=" + score + ", playTime=" + playTime + " where nickname=\"" + nickname + "\"";
        Debug.Log("플레이어 기록 업데이트 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        return false;
    }

    /* 플레잉 기록 삭제 메소드 */
    public bool DeletePlayingInfo(int roundNum, string playerName)
    {
        string query="delete from "+playingTable+" where roundNum="+roundNum+" and playerName=\""+playerName+"\"";
        Debug.Log("플레잉 기록 삭제 쿼리 :: " + query);

        if(InsertOrUpdateRequest(query)) return true;
        return false;
    }


    /* 플레이어 상위 5개 랭킹 조회 메소드 */
    public List<PlayerClass> Select5Players()
    {
        string query = "select rank() over (order by score desc, playTime asc) as 'rank',nickname,score,playTime,saveTime from playerInfo limit 5";
        Debug.Log("플레이어 랭킹 조회 쿼리 :: " + query);

        DataSet dataSet = SelectRequest(query);
        if (dataSet != null)
        {
            List<PlayerClass> players = new List<PlayerClass>();

            int length = dataSet.Tables[playerTable].Rows.Count;
            for (int i = 0; i < length; i++)
            {
                DataRow row = dataSet.Tables[playerTable].Rows[i];
                Debug.Log(row[0] + "" + row[1] + row[2] + row[3] + row[4]);

                long rank = row.Field<long>("rank");
                string nickname = row.Field<string>("nickname");
                uint score = row.Field<uint>("score");
                uint playTime = row.Field<uint>("playTime");
                DateTime saveTime = row.Field<DateTime>("saveTime");

                PlayerClass player = new PlayerClass(rank, nickname, score, playTime, saveTime);

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
    public bool InsertOrUpdateRequest(string pQuery)
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
    public DataSet SelectRequest(string pQuery)
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