using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;

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
            if(instance == null)
            {
                instance = new DBManager();
            }
            return instance;
        }
    }

    /* 클래스 생성자 */
    public DBManager() {
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

    /* SelectAll Test 메소드 */
    public void SelectAllTest()
    {
        string query = "select * from playerInfo";
        DataSet dataSet = SelectRequest(query);

        if (dataSet != null)
        {
            Debug.Log("데이터베이스 전체 조회 테스트");
            foreach (DataRow row in dataSet.Tables[tableName].Rows)
            {
                foreach (DataColumn col in dataSet.Tables[tableName].Columns)
                {
                    Debug.Log(col.ColumnName + ": " + row[col]);
                }
            }
        }
    }

    /* 닉네임 저장 메소드 */
    public bool InsertNameRequest(string name)
    {
        try
        {
            string query = "insert into " + tableName + "(nickname) value(\"" + name + "\")";

            Debug.Log("닉네임 저장 쿼리 :: " + query);

            MySqlCommand command = new MySqlCommand();
            command.Connection = SqlConnection;
            command.CommandText = query;

            if ((SqlConnection == null) || (SqlConnection.State != ConnectionState.Open))
            {
                SqlConnection.Open();
            }

            command.ExecuteNonQuery();

            SqlConnection.Close();

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log("닉네임 저장 실패 :: " + e.ToString());
            return false;
        }
    }

    /* 조회 메소드 (커스텀 필요) */
    public DataSet SelectRequest(string pQuery)
    {
        try
        {
            if ((SqlConnection == null) || (SqlConnection.State != ConnectionState.Open))
            {
                SqlConnection.Open();
            }

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConnection;
            cmd.CommandText = pQuery;

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

    // Disconnect
    public void OnApplicationQuit()
    {
        if (SqlConnection != null && SqlConnection.State != ConnectionState.Closed)
        {
            SqlConnection.Close();
        }
    }
}
