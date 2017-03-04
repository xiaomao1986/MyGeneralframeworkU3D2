using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundle01
{
    private BundleManager m_BundleManager=null;
    private AssetBundleManifest manifest = null;

    //获取依赖关系 名字
    public string[] GetAllDependencies(string _abName)
    {
        if (manifest==null)
        {
            Debug.LogError("  AssetBundleManifest   null");
            return null;
        }
        return manifest.GetAllDependencies(_abName);
    }
    public LoadAssetBundle01(BundleManager _BundleManager)
    {
        m_BundleManager = _BundleManager;
    }
    public void Init()
    {
        Setmanifest();
    }
    private void Setmanifest()
    {
        //string path = AppConfig.GetpersistentDataPath() + "Android/Android.myab";
        string path = AppConfig.GetpersistentDataPath() + AppConfig.path + AppConfig.pathmyab;
        AssetBundle bundle = LoadBundle(path);
        manifest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
        bundle.Unload(false);
    }
    private AssetBundle LoadBundle(string path)
    {
        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
        long lengh = fs.Length;
        byte[] bytes = new byte[lengh];
        fs.Read(bytes, 0, (int)lengh);
        fs.Close();
        fs = null;
        AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
        return bundle;
    }

    public AssetBundle GetAssetBundle(string _abName)
    {
        return LoadDependsAssetbundle(_abName);
    }
    private AssetBundle LoadDependsAssetbundle(string _abName)
    {
        string[] cubedepends = manifest.GetAllDependencies(_abName);
        Debug.Log(cubedepends.Length);
        AssetBundle[] dependsAssetbundle = new AssetBundle[cubedepends.Length];
        for (int index = 0; index < cubedepends.Length; index++)
        {
            string pathName = AppConfig.GetpersistentDataPath() + AppConfig.path + cubedepends[index];
            dependsAssetbundle[index] = LoadBundle(pathName);// AssetBundle.LoadFromFile("file:///" + AppConfig.GetpersistentDataPath() + "Android/" + cubedepends[index]);
            if (m_BundleManager!=null)
            {
                string ABname = cubedepends[index].Replace(".myab", "");
                Debug.Log("ABname---"+ ABname);
                m_BundleManager.AddDependentDic(ABname, dependsAssetbundle[index]);
            }
        }
        string bundlepath= AppConfig.GetpersistentDataPath() + AppConfig.path + _abName;
        AssetBundle bundle = LoadBundle(bundlepath);
        return bundle;
    }
}
