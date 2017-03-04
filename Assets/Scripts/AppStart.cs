using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
public class AppStart : MonoBehaviour
{

    [CSharpCallLua]
    private delegate void LuaInit();
    private LuaInit AppStartluaInit;
    public static AppStart Instance;

    public Text tt;
    public my_SceneManager SceneManager;
    public BundleManager assetBundleManager = null;
    public void Awake()
    {
        Instance = this;
        SceneManager = new my_SceneManager();
        gameObject.AddComponent<AppUpdata>();
        Debug.Log("AppStart    Awake");
    }

    public void Start()
    {
        Debug.Log("AppStart    Start");
        AppUpdata.Instance.Init();
    }
    Action ac;
    public int yy = 10;
    public void Init()
    {
        My_debugNet.SendDebugLog("------AppStart----Init---");
        assetBundleManager = new BundleManager();
        assetBundleManager.loadAssetBundle01.Init();
        //  TextAsset luaAppstart = assetBundleManager.GetAssetBundle("AppStart.myab").LoadAsset("AppStart") as TextAsset;
        TextAsset luaAppstart = Resources.Load("AppStart")as TextAsset;
        Debug.Log("lua------------" + luaAppstart.text);
        LuaMag.luaEnv.DoString(luaAppstart.text);
        LuaMag.luaEnv.Global.Set("CSAppstart", this);
        LuaMag.luaEnv.Global.Get("Init", out AppStartluaInit);
        ac = LuaMag.luaEnv.Global.Get<Action>("start");
        ac();
    
    
    }

    public void OpenScene(string abName, string sceneName)
    {
        AssetBundle sab = assetBundleManager.GetAssetBundle(abName);
        SceneManager.OpenScene(sceneName);
    }
}
