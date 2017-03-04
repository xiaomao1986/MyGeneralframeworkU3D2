using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData
{
    public IEnumerator LoadDatawww(string _config)
    {
        string path = AppConfig.AppHttp+AppConfig.AppUpdataPath+ _config;
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            string s1 = AppConfig.GetpersistentDataPath() + _config;
            string filename = Path.GetFileName(s1);
            string tSt = s1.Replace(filename, "");
            while (Directory.Exists(tSt) == false)
            {
                Directory.CreateDirectory(tSt);
            }
            Stream outStream = File.Create(AppConfig.GetpersistentDataPath() + _config);
            byte[] buffer = www.bytes;
            outStream.Write(buffer, 0, buffer.Length);
            outStream.Close();
            AppUpdata.Instance.LoaSum++;
        }
        else
            Debug.Log("LoadData ------www.error--------" + www.error.ToString()+"---"+ path);

    }
    public IEnumerator LoadDatawww2(string _config,Callback _callback)
    {
        // string path = @"file://E:/StreamingAssets/" + _config;
        Debug.Log("_config----" + _config);
        My_debugNet.SendDebugLog(" load._config--" + _config);
        string path = AppConfig.GetstreamingAssetsPath()+ _config;
        Debug.Log("path----" + path);
        My_debugNet.SendDebugLog(" load.path--" + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            string s1 = Application.persistentDataPath + "/" + _config;
            string filename = Path.GetFileName(s1);
            string tSt = s1.Replace(filename, "");
            while (Directory.Exists(tSt) == false)
            {
                Directory.CreateDirectory(tSt);
            }
            Debug.Log("Application.persistentDataPath--------" + Application.persistentDataPath);
            My_debugNet.SendDebugLog(" Application.persistentDataPath----" + Application.persistentDataPath + "/" + _config);
            Stream outStream = File.Create(Application.persistentDataPath + "/" + _config);
            My_debugNet.SendDebugLog(" Applicatio--222222222222-");
            byte[] buffer = www.bytes;
            outStream.Write(buffer, 0, buffer.Length);
            outStream.Close();
            _callback();
        }
        else
        {
            Debug.Log("LoadData ------www.error--------" + www.error.ToString() + "---" + path);
            My_debugNet.SendDebugLog(" LoadData ------www.error------" + www.error.ToString());
        }

    }
}
