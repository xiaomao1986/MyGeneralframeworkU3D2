using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleManager
{
     public LoadAssetBundle01 loadAssetBundle01 = null;
    //Bundle依赖关系 字典
    private Dictionary<string, BundleData> DependentDic = new Dictionary<string, BundleData>();
    private Dictionary<string, BundleData> AssetBundleDic = new Dictionary<string, BundleData>();
    public BundleManager()
    {
        loadAssetBundle01 = new LoadAssetBundle01(this);
    }
    /// <summary>
    /// 添加 依赖关系到 字典
    /// </summary>
    /// <param name="_bundleName">资源名字</param>
    /// <param name="_assetBundle">AssetBundle</param>
    public void AddDependentDic(string _bundleName, AssetBundle _assetBundle)
    {
        if (DependentDic.ContainsKey(_bundleName))
            DependentDic[_bundleName].SetReferenceCount();
        else
        {
            BundleData tmpBundleData = new BundleData(_bundleName,_assetBundle);
            DependentDic.Add(_bundleName,tmpBundleData);
        }
    }
    public void DeleteDependent(string _depName)
    {
        if (DependentDic.ContainsKey(_depName))
        {
            DependentDic[_depName].DeleteBundle();
        }
    }
    /// <summary>
    /// 获得AssetBundle  
    /// </summary>
    /// <param name="_abName"></param>
    /// <returns></returns>
    public AssetBundle GetAssetBundle(string _abName)
    {
        if (!AssetBundleDic.ContainsKey(_abName))
        {
            //TODO 如果没有 重新加载进来
            AssetBundle tmpBundle = loadAssetBundle01.GetAssetBundle(_abName);
            if (tmpBundle==null)
            {
                Debug.Log(" 加载AB 没成功   tmpBundle  null");
                return null;
            }
            AddAssetBundle(_abName, tmpBundle);
            return tmpBundle;
        }
        return AssetBundleDic[_abName].GetBundle();
    }
    public void AddAssetBundle(string _abName, AssetBundle _assetBundle)
    {
        if (AssetBundleDic.ContainsKey(_abName))
        {
            AssetBundleDic[_abName].SetReferenceCount();
        }else
        {
            BundleData tmpBundleData = new BundleData(_abName, _assetBundle);
            AssetBundleDic.Add(_abName, tmpBundleData);
        }
    }
    public void DeleteAssetBundle(string _abName)
    {
        if (AssetBundleDic.ContainsKey(_abName))
        {
            //TODO 该Bundle 依赖的资源
            string[] depNames= loadAssetBundle01.GetAllDependencies(_abName);
            for (int i = 0; i < depNames.Length; i++)
            {
                DeleteDependent(depNames[i]);
            }
            bool isDelete= AssetBundleDic[_abName].DeleteBundle();
            if (isDelete)
            {
                AssetBundleDic.Remove(_abName);
            }
        }
    }
}
