/********************************************************
 * 程序：AppUpdata 应用更新                             *
 * 作者：李明洋                                         *
 * QQ:104228011                                         *
 * 时间：2017/02/16                                     *
 * 描述：                                               *
 * 1，该类负责 对应用的 资源进行下载                    *
 * 2，本地版本号 与 服务器版本号 对比                   *
 * 3，下载服务器资源 存在本地  StreamingAssets目录      * 
 * 4，下载完毕后 调用 AppStart类 init fangfa            *   
 ********************************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AppUpdata : MonoBehaviour
{
    public static AppUpdata Instance;
    private Dictionary<string, string> m_localupdataConifDic = new Dictionary<string, string>();
    private Dictionary<string, string> m_wwwupdataConifDic = new Dictionary<string, string>();

    private Dictionary<string, string> m_updataConifDic = new Dictionary<string, string>();

    private StreamingTopersistent m_StreamingTopersistent=null;

    public int LoaSum = 0;
    public int coypSum = 0;
    public Text ttttt;

    void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
        My_debugNet.SendDebugLog(" Start");
        Debug.Log("Start");
        string p = AppConfig.GetpersistentDataPath()  + AppConfig.path + AppConfig.UpdataConfigName;
        Debug.Log("Start1111111111111");
        My_debugNet.SendDebugLog(" 124444");
        FileInfo t = new FileInfo(p);
        if (t.Exists)
        {
            InitUpdata();
            Debug.Log("2222222222");
            My_debugNet.SendDebugLog("InitUpdata");
        }
        else
        {
            Debug.Log("33333333333");
            m_StreamingTopersistent = new StreamingTopersistent(this);
            My_debugNet.SendDebugLog("  m_StreamingTopersistent = new");
        }
    }
    public void InitUpdata()
    {
        StartCoroutine(LoadlocalUpdateConif());
    }
    void Update()
    {
        if (m_updataConifDic.Count!=0)
        {
            if (LoaSum==m_updataConifDic.Count)
            {
                DicClear();
                Debug.Log("下载完成！！");
                string path = AppConfig.GetpersistentDataPath() + AppConfig.path;
                SavedText.CreateFile(path, AppConfig.UpdataConfigName, UpdataConfig);
                AppStart.Instance.Init();
                
            }
        }
    }
    private string UpdataConfig = "";
    //加载本地 UpdateConif.txt 
    IEnumerator LoadlocalUpdateConif()
    {
        // string path = AssetPath.AssetBundlePath() + "UpdataConfig.txt";
        My_debugNet.SendDebugLog(" LoadlocalUpdateConif-111111111111111");
        string path = "file:///"+AppConfig.GetpersistentDataPath()+AppConfig.path+AppConfig.UpdataConfigName;
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            string[] Arrys = www.text.Split(new char[2] { '\n', '|' });
            for (int i = 0; i < Arrys.Length - 1; i++)
            {
                string key = Arrys[i];
                i++;
                string value = Arrys[i];
                m_localupdataConifDic.Add(key, value);
            }
            StartCoroutine(LoadwwwUpdateConif());
        }
        else
        {
            My_debugNet.SendDebugLog(" LoadlocalUpdateConif-11111111ww.error-1111111"+ www.error);
            Debug.Log(" ------www.error--------" + www.error);
        }
    }
    IEnumerator LoadwwwUpdateConif()
    {
        My_debugNet.SendDebugLog("LoadwwwUpdateConif===============");
        string path = AppConfig.AppHttp+ AppConfig.AppUpdataConfig;
        My_debugNet.SendDebugLog("LoadwwwUpdateConif=======path========"+ path);
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
                m_wwwupdataConifDic.Add(key, value);
            }
            UpdataContrast();
        }
        else
        {
            My_debugNet.SendDebugLog("LoadwwwUpdateConif=======www.error========" + www.error);
            Debug.Log(" ------www.error--------" + www.error);
        }
            
    }
    private void UpdataContrast()
    {
        if (!m_wwwupdataConifDic.ContainsKey("version") || !m_localupdataConifDic.ContainsKey("version"))
        {
            My_debugNet.SendDebugLog("----更新 配置文件出错！！！未找到 version");
            Debug.LogError("更新 配置文件出错！！！未找到 version");
            return;
        }
        if (m_wwwupdataConifDic["version"] == m_localupdataConifDic["version"])
        {
            Debug.Log(" ------版本号相同-------");
            My_debugNet.SendDebugLog("------版本号相同-------");
            //版本号相同 不做更新 直接进入程序 调用 开启程序方法
            AppStart.Instance.Init();
            My_debugNet.SendDebugLog("------    AppStart.Instance.Init();-------");
            DicClear();
            return;
        }
        //查找不同
        foreach (var item in m_wwwupdataConifDic)
        {
            //把不相同的配置放进 更新字典里 
            if (item.Key == "version")
            {
                if (m_localupdataConifDic["version"] != item.Value)
                {
                  //  m_updataConifDic.Add(item.Key, item.Value);
                }
            }
            if (!m_localupdataConifDic.ContainsKey(item.Key))
            {
                m_updataConifDic.Add(item.Key, item.Value);
            }
        }
        foreach (var item in m_updataConifDic)
        {
            LoadData load = new LoadData();
            StartCoroutine(load.LoadDatawww(item.Value));
        }
    }

    private void DicClear()
    {
        m_localupdataConifDic.Clear();
        m_wwwupdataConifDic.Clear();
        m_updataConifDic.Clear();
    }


    void DeleteFile(string path)
    {
      //  string _path = @"file://E:/StreamingAssets/Android/Android.manifest";
        string path1 = @"E:\StreamingAssets\Android\UpdateConfig.txt";
        Debug.Log("path--" + path1);
        File.Delete(path1);
    }
}
