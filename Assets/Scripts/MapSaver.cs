using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System.IO;

public class MapSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var list = new List<string>()
        {
            "N", "N","G", "G", "G", "G", "HW", "HW",
            "G", "G", "G", "G", "HW", "HW", "CW", "HW"
        };
        var map = new Map("TestMap", "TEST", list, new Vector2(0, 0), width: 8, height: 2);
        string json = JsonUtility.ToJson(map);
        Debug.Log(json);
        WriteData(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void WriteData(string jsonData)
    {
        FileStream fs = new FileStream(Application.persistentDataPath +"/test.json", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);

        await sw.WriteAsync(jsonData);
        sw.Flush();
        sw.Close();
        fs.Close();
    }
}
