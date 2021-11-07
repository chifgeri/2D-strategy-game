using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Model;
using System.Threading.Tasks;

public static class MapLoader
{
    // Start is called before the first frame update
    public static async Task<Map> LoadData(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        string json = await sr.ReadToEndAsync();

        sr.Close();
        fs.Close();

        return JsonUtility.FromJson<Map>(json);
    }
}
