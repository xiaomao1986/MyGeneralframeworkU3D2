using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class TestScene : Scene
{
    
    protected override void OnClose(Callback<bool> callback)
    {

    }

    protected override void OnHide(Callback<bool> callback)
    {
       
    }

    protected override void OnOpen(Callback<bool> callback)
    {
        GameObject[] obj = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            AssetBundle ad = assetBundleManager.GetAssetBundle("cube.myab");
           
            obj[i] = MonoBehaviour.Instantiate(ad.LoadAsset("cube")) as GameObject;
            obj[i].transform.SetParent(this.transform);
            obj[i].transform.localPosition = new Vector3(0, i, 5);
        }
        //AssetBundle ad = assetBundleManager.GetAssetBundle("cube.myab");
        //GameObject ob = MonoBehaviour.Instantiate(ad.LoadAsset("cube")) as GameObject;
        //ob.transform.SetParent(this.transform);
        //ob.transform.localPosition = new Vector3(0, 0, 5);

        AssetBundle ad1 = assetBundleManager.GetAssetBundle("tlua.myab");
        TextAsset t = ad1.LoadAsset("tlua") as TextAsset;
       // Debug.Log("lua------------" + t.text);

        string text = System.IO.File.ReadAllText(AppConfig.GetpersistentDataPath() + AppConfig.path + "1234.lua");
      //  Debug.Log("AppStart    text===" + text);
        My_debugNet.SendDebugLog("AppStart    text==="+ text);
        LuaMag.luaEnv.DoString(t.text);
        LuaMag.luaEnv.Global.Set("CSTestScene", this);
        LuaMag.luaEnv.Global.Get("luaInit", out testobj01);
    }

    protected override void OnShow(Callback<bool> callback)
    {
        
    }
    [CSharpCallLua]
    public delegate void testGameObject01(GameObject p);
    testGameObject testobj01;
    // Use this for initialization
    void Start ()
    {

     
    }
	// Update is called once per frame
	void Update () {
		
	}
}
