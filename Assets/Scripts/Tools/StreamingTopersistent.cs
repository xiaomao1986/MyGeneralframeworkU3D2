using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamingTopersistent
{
    private AppUpdata m_appUpdata=null;
    public StreamingTopersistent(AppUpdata _appUpdata)
    {
        try
        {
            m_appUpdata = _appUpdata;
            _appUpdata.StartCoroutine(LoadlocalUpdateConif());
        }
        catch (System.Exception e)
        {
            My_debugNet.SendDebugLog("  m_StreamingTopersistent--3--"+e);
            throw;
        }
     
    }
    private string UpdataConfig = "";
    private Dictionary<string, string> m_localupdataConifDic = new Dictionary<string, string>();
    IEnumerator LoadlocalUpdateConif()
    {
        My_debugNet.SendDebugLog("  LoadlocalUpdateConif----");
        string path = AppConfig.GetstreamingAssetsPath() + AppConfig.path + AppConfig.UpdataConfigName;
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            UpdataConfig = www.text;
            string[] Arrys = www.text.Split(new char[2] { '\n', '|' });
            for (int i = 0; i < Arrys.Length - 1; i++)
            {
                string key = Arrys[i];
                i++;
                string value = Arrys[i];
                m_localupdataConifDic.Add(key, value);
            }
            foreach (var item in m_localupdataConifDic)
            {
                if (item.Key == "version")
                {
                    continue;
                }
                LoadData load = new LoadData();
                My_debugNet.SendDebugLog(" load.LoadDatawww2--" + item.Value);
                AppUpdata.Instance.StartCoroutine(load.LoadDatawww2(item.Value, loadDataCallback));
            }
            if (m_localupdataConifDic.Count==1)
            {
                loadDataCallback();
            }
        }
        else
        { 
            Debug.Log(" ------www.error--------" + www.error);
            My_debugNet.SendDebugLog("  LoadlocalUpdateConif--www.erro--"+ www.error);
        }
    }
    private void loadDataCallback()
    {
        My_debugNet.SendDebugLog(" loadDataCallback--33333333333333-" + m_appUpdata.coypSum+"===="+ m_localupdataConifDic.Count);
        string path = AppConfig.GetpersistentDataPath() + AppConfig.path;
        if (m_appUpdata == null)
        {
            return;
        }
        if (m_localupdataConifDic.Count ==1)
        {
            My_debugNet.SendDebugLog("m_localupdataConifDic.Count  ===1");
            SavedText.CreateFile(path, AppConfig.UpdataConfigName, UpdataConfig);
            m_localupdataConifDic.Clear();
            m_appUpdata.InitUpdata();
            Debug.Log("复制完成");
            My_debugNet.SendDebugLog("  LoadlocalUpdateConif---复制完成-");
            return;
        }
        if (m_localupdataConifDic.Count != 0)
        {
            My_debugNet.SendDebugLog(" m_localupdataConifDic.Count != 0");
            m_appUpdata.coypSum++;
            if (m_appUpdata.coypSum == m_localupdataConifDic.Count-1)
            {
                My_debugNet.SendDebugLog("m_appUpdata.coypSum == m_localupdataConifDic.Count-1");
                SavedText.CreateFile(path, AppConfig.UpdataConfigName, UpdataConfig, loadDataCallback);
            }
            if (m_appUpdata.coypSum == m_localupdataConifDic.Count)
            {
                m_localupdataConifDic.Clear();
                m_appUpdata.InitUpdata();
                Debug.Log("复制完成");
                My_debugNet.SendDebugLog("  LoadlocalUpdateConif---复制完成-");
            }
        }
    }
}
