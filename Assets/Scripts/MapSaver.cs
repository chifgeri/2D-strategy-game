using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System.IO;

public static class MapSaver
{
    public static async void WriteData(string jsonData)
    {
        FileStream fs = new FileStream(Application.persistentDataPath +"/test.json", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);

        await sw.WriteAsync(jsonData);
        sw.Flush();
        sw.Close();
        fs.Close();
    }
}
