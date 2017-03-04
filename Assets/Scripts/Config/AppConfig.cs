using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConfig
{


    public enum Platform
    {
        Android = 0,
        IOS = 1,
        Windows = 2
    }
   
    //服务器地址
    public static string AppHttp = "http://192.168.1.125";

    public static string AppUpdataPath = "/StreamingAssets/";
    public static string UpdataConfigName = "UpdataConfig.txt";

    //项目目录
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    public static  string AppUpdataConfig = "/StreamingAssets/Android/UpdataConfig.txt";
    private static Platform m_platform = Platform.Windows;
    public static string path = "Android/";
    public static string pathmyab = "Android.myab";
#elif UNITY_ANDROID
    public static string AppUpdataConfig = "/StreamingAssets/Android/UpdataConfig.txt";
    private static Platform m_platform = Platform.Android;
    public static string path = "Android/";
     public static string pathmyab = "Android.myab";
#elif UNITY_IPHONE
    public static string AppUpdataConfig = "/StreamingAssets/IOS/UpdataConfig.txt";
    private static Platform m_platform = Platform.IOS;
    public static string path = "IOS/";
     public static string pathmyab = "IOS.myab";
#endif
    public static string GetstreamingAssetsPath()
    {
        if (Platform.Android == m_platform)
        {
            return Application.streamingAssetsPath+"/";
        }
        if (Platform.IOS == m_platform)
        {
            return Application.streamingAssetsPath + "/";
        }
        if (Platform.Windows == m_platform)
        {
            return "file://" + Application.streamingAssetsPath + "/";
        }
        return "";
    }
    public static string GetpersistentDataPath()
    {
        if (Platform.Android == m_platform)
        {
            return Application.persistentDataPath + "/";
        }
        if (Platform.IOS == m_platform)
        {
            return Application.persistentDataPath + "/";
        }
        if (Platform.Windows == m_platform)
        {
            return Application.persistentDataPath + "/";
        }
        return "";
    }



}
