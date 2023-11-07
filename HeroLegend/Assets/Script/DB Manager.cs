using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

/* MonoBehaviour 상속받지 않았으므로 Hierarchy에 존재할 필요 없음 */
public class DBManager
{
    public static MySqlConnection SqlConnection;

    /*
     * CONFIRM :: 추후 정보 숨김 가능한가
     */

    /* static이 아니면 커넥션 불가 */
    static readonly string ipAddress = "127.0.0.1";
    static readonly string dbId = "root";
    static readonly string dbPassword = "root";
    static readonly string dbName = "bhj";
    static readonly string tableName = "playerInfo";

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


    /************************************ NICKNAME 관리 ************************************/
    /***************************************************************************************/

    public static string nickname;

    /* 닉네임 일반 Getter */
    public string getNickname()
    {
        if (!string.IsNullOrEmpty(nickname))
        {
            return nickname;
        }
        return "OO";
    }

    public string getNickname을를()
    {
        if (!string.IsNullOrEmpty(nickname))
        {
            if (HasJongseong(nickname))
            {
                return nickname + "을 ";
            }
            else
            {
                return nickname + "를 ";
            }
        }
        return "OO를 ";//예외적으로 nickname 데이터 날아간 경우, 받침 없음 기준으로 처리
    }

    public string getNickname이가()
    {
        if (!string.IsNullOrEmpty(nickname))
        {
            if (HasJongseong(nickname))
            {
                return nickname + "이 ";
            }
            else
            {
                return nickname + "가 ";
            }
        }
        return "OO가 ";//예외적으로 nickname 데이터 날아간 경우, 받침 없음 기준으로 처리
    }

    public string getNickname은는()
    {
        if (!string.IsNullOrEmpty(nickname))
        {
            if (HasJongseong(nickname))
            {
                return nickname + "은 ";
            }
            else
            {
                return nickname + "는 ";
            }
        }
        return "OO는 ";//예외적으로 nickname 데이터 날아간 경우, 받침 없음 기준으로 처리
    }

    /* 종성 분석 메소드 */
    bool HasJongseong(string text)
    {
        string lastCharacter = text.Substring(text.Length - 1, 1);
        char c = lastCharacter[0];

        if (!(0xAC00 <= c && c <= 0xD7A3) && !(0x3131 <= c && c <= 0x318E))
        {
            //한글 외의 문자는 받침 없음으로 처리
            Debug.Log("한글 외의 문자");
            return false;
        }

        int codePoint = Char.ConvertToUtf32(c.ToString(), 0);
        int distanceFromGa = codePoint - Char.ConvertToUtf32("가", 0);

        // 한 음절은 초성(19자), 중성(21자), 종성(28자)로 구성되므로 종성이 있는지 여부를 확인하기 위해 거리를 계산
        return distanceFromGa % 28 != 0;
    }


    /************************************ DATABASE 관리 ************************************/
    /***************************************************************************************/


    /* 닉네임 저장 메소드 */
    public bool InsertNickname(string name)
    {
        string query = "insert into " + tableName + "(nickname) value(\"" + name + "\")";
        Debug.Log("닉네임 저장 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query))
        {
            nickname = name;
            return true;
        }
        else return false;
    }

    /* 게임 오버 시, 점수 및 시간 저장 메소드 */
    public bool UpdatePlayerInfo(string name, int score, int playTime)
    {
        string query = "update " + tableName + " set score=" + score + ", playTime=" + playTime + " where nickname=\"" + name + "\"";
        Debug.Log("점수 및 시간 저장 쿼리 :: " + query);

        if (InsertOrUpdateRequest(query)) return true;
        else return false;
    }

    /* 플레이어 상위 5개 랭킹 조회 메소드 */
    public List<PlayerClass> SelectPlayers()
    {
        string query = "select rank() over (order by score desc, playTime asc) as 'rank',nickname,score,playTime,saveTime from playerInfo limit 5";
        Debug.Log("플레이어 랭킹 조회 쿼리 :: " + query);

        DataSet dataSet = SelectRequest(query);
        if (dataSet != null)
        {
            List<PlayerClass> players = new List<PlayerClass>();

            int length = dataSet.Tables[tableName].Rows.Count;
            for (int i = 0; i < length; i++)
            {
                DataRow row = dataSet.Tables[tableName].Rows[i];
                Debug.Log(row[0] + "" + row[1] + row[2] + row[3] + row[4]);
                long rank = row.Field<long>("rank");
                string nickname = row.Field<string>("nickname");
                byte score = row.Field<byte>("score");
                byte playTime = row.Field<byte>("playTime");
                DateTime saveTime = row.Field<DateTime>("saveTime");

                PlayerClass player = new PlayerClass(rank, nickname, score, playTime, saveTime);

                Debug.Log("조회된 플레이어 :: " + player);

                players.Add(player);
            }
            return players;
        }
        else return null;
    }

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
            adapter.Fill(dataSet, tableName);

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