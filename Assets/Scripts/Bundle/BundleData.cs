using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleData
{

    public string assetBundleName = "";
    public AssetBundle assetBundle;

    public int ReferenceCount = 0;

    public BundleData(string _assetBundleName,AssetBundle _assetBundle)
    {
        ReferenceCount = 1;
        assetBundleName = _assetBundleName;
        assetBundle = _assetBundle;
    }

    public void SetReferenceCount()
    {
        ReferenceCount++;
    }
    public int GetReferenceCount()
    {
        return ReferenceCount;
    }
    public AssetBundle GetBundle()
    {
        ReferenceCount++;
        return assetBundle;
    }
    public bool DeleteBundle()
    {
        if (ReferenceCount>0)
        {
            ReferenceCount--;
            if (ReferenceCount==0)
            {
                assetBundle.Unload(true);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("引用 等于 0");
            return false;
        }

    }
	
}
