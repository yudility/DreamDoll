using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// ������ ������
public class PlayerData
{
    public string name;
    public int level;
    public int coin;
    public int item;
}

public class DataManager : MonoBehaviour
{
    // �̱���
    public static DataManager instance;

    // �÷��̾� ����
    PlayerData nowPlayer = new PlayerData();

    string path;
    string filename = "save";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }



    void Start()
    {
       
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + filename, data);
        Debug.Log("���� �Ϸ�");
    }

    public void LoadData()
    {
        string data =  File.ReadAllText(path + filename);

        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}