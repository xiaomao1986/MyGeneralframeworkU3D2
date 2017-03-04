using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestLoadNewAB : MonoBehaviour
{
    private GameObject cube=null;
    private AssetBundleManifest manifest;
    public Text t;
    public TestLoadNewAB Instance;
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    public void Start()
    {
      
      ///  StartCoroutine(LoadAssetBundle("cube.myab"));
    }
     public void OnCilck()
    {
        //if (m_AssetBundle==null)
        //{
        //    string Path3 = "file://" + Application.streamingAssetsPath + "/Android/cube.myab
        //    StartCoroutine(LoadAssetBundle1(Path3, "cube"));
        //    return;
        //}
        Debug.Log("OnCilck！！！");
        StartCoroutine(LoadAssetBundle001());
        My_debugNet.SendDebugLog("cop");
       // AssetBundle  ab =  GetAssetBundle("cube01.myab");
        //GameObject o = Instantiate(ab.LoadAsset("cube01")) as GameObject;
      //  o.transform.position = new Vector3(0, 0, 5);


    }
    public void OnCilck1()
    {
        //if (abDic.ContainsKey("cube"))
        //{
        //    cube = Instantiate(abDic["cube"].LoadAsset("cube")) as GameObject;
        //    cube.transform.position = new Vector3(0, 0, 5);
        //}
        string path =  AppConfig.GetpersistentDataPath() + "Android/Android.myab";
        AssetBundle bundle = getb(path);
        if (bundle==null)
        {
           
        }else
        {

            manifest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
            bundle.Unload(false);
            string[] cubedepends = manifest.GetAllDependencies("cube.MyAB");
            Debug.Log(cubedepends.Length);
            AssetBundle[] dependsAssetbundle = new AssetBundle[cubedepends.Length];
            for (int index = 0; index < cubedepends.Length; index++)
            {
                dependsAssetbundle[index] = getb(AppConfig.GetpersistentDataPath() + "Android/" + cubedepends[index]);// AssetBundle.LoadFromFile("file:///" + AppConfig.GetpersistentDataPath() + "Android/" + cubedepends[index]);
            }
            AssetBundle cubeBundle = getb(AppConfig.GetpersistentDataPath() + "Android/" + "cube.MyAB");// AssetBundle.LoadFromFile("file:///" + AppConfig.GetpersistentDataPath() + "Android/" + pathData);
            GameObject g = Instantiate(cubeBundle.LoadAsset("cube")) as GameObject;
            g.transform.position = new Vector3(0, 0, 5);
        }


    }
    public void OnCilck2()
    {
        if (abDic.ContainsKey("cube"))
        {
            foreach (var item in abDic)
            {
                if (item.Key!= "cube"&&item.Key!="m1")
                {
                    item.Value.Unload(true);
                    Debug.Log("释放成功！！！"+item.Key);
                }
            }
         
          
        }else
        {
            Debug.Log("cube！被删除！！");
        }

    }
    void OnGUI()
    {
        //if (GUILayout.Button("LoadAssetbundle"))
        //{
        //   AssetBundle cubeBundle= GetAssetBundle("cube03.MyAB");
        //    cube= Instantiate(cubeBundle.LoadAsset("cube03")) as GameObject;
        //    cube.transform.position = new Vector3(0, 0, 5);
        //}
        //if (GUILayout.Button("LoadAssetbundle2"))
        //{
        //    AssetBundle cubeBundle1 = GetAssetBundle("cube01.MyAB");
        //    GameObject g= cubeBundle1.LoadAsset("cube01") as GameObject;
        //    GameObject oj = new GameObject();
        //    oj.name = "oj";

        //   GameObject obj= Instantiate(cubeBundle1.LoadAsset("cube01"), oj.transform) as GameObject;
        //    obj.transform.localPosition = Vector3.zero;
        //}
        //if (GUILayout.Button("LoadAssetbundle3"))
        //{
        //    Debug.Log("cube.transform.position--"+ cube.transform.position);
        //    cube.transform.localPosition = Vector3.zero;
        //}

    }

    public AssetBundle getb(string path)
    {
        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
        Debug.Log("fs.Length" + fs.Length);
        long lengh = fs.Length;
        byte[] bytes = new byte[lengh];
        fs.Read(bytes, 0, (int)lengh);
        fs.Close();
        fs = null;
        AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
        if (bundle == null)
        {
            Debug.Log("222222222222222");
            return null;
        }
        else
        {
            Debug.Log("333333");
            return bundle;
        }
    }


    public void Update()
    {
        if (m_AssetBundle!=null)
        {

        }
    }
    IEnumerator LoadAssetBundle001()
    {
        string path = "file://" + AppConfig.GetpersistentDataPath() + "Android/Android";
        WWW www = new WWW(path);
        yield return www;
        if (www.error!=null)
        {
            Debug.Log("11111111111");
        }else
            Debug.Log("222222222222222");
    }
    public AssetBundle GetAssetBundle(string pathData)
    {
        string path = "file://" + AppConfig.GetpersistentDataPath() + "Android/Android";
        Debug.Log("=======path=====" + path);
        AssetBundle manifestBundle = AssetBundle.LoadFromFile(path);
        manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
        manifestBundle.Unload(false);
        AssetBundle cubeBundle=null;
            string[] cubedepends = manifest.GetAllDependencies(pathData);
            Debug.Log(cubedepends.Length);
            AssetBundle[] dependsAssetbundle = new AssetBundle[cubedepends.Length];
            for (int index = 0; index < cubedepends.Length; index++)
            {
                dependsAssetbundle[index] = AssetBundle.LoadFromFile("file:///" + AppConfig.GetpersistentDataPath() + "Android/" + cubedepends[index]);
            }

            cubeBundle = AssetBundle.LoadFromFile("file:///" + AppConfig.GetpersistentDataPath() + "Android/" + pathData);
            return cubeBundle;
      }

    private AssetBundle m_AssetBundle = null;
    IEnumerator LoadAssetBundle(string name)
    {
        //AssetBundle manifestBundle = AssetBundle.LoadFromFile("file://" + Application.streamingAssetsPath + "/Android/Android");
        string Path  = AssetPath.AssetBundleManifestPath();
        WWW www = new WWW(Path);
        yield return www;
        if (www.error==null)
        {
            AssetBundle manifestBundle = www.assetBundle;
            manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
            manifestBundle.Unload(false);

            string[] depNames = manifest.GetAllDependencies(name);
            Debug.Log("depNames length = " + depNames.Length.ToString());
            AssetBundle[] dependsAssetbundle = new AssetBundle[depNames.Length];
            string Path2 = "";
            for (int index = 0; index < depNames.Length; index++)
            {

                Debug.Log("depNames---"+ depNames[index]);
                string ABname = depNames[index].Replace(".myab", "");
                Debug.Log("ABname---" + ABname);
                Path2 = AssetPath.AssetBundlePath() + depNames[index];
                StartCoroutine(LoadAssetBundle(Path2,dependsAssetbundle[index],ABname));
            }
            //string Path3 = AssetPath.AssetBundlePath() + "cube03.myab";
          //  StartCoroutine(LoadAssetBundle1(Path3));
        }
        else
        {
            Debug.Log("www====" + www.error.ToString());
            t.text = "wwwh =" + www.error.ToString();
        }
    }
    IEnumerator LoadAssetBundle(string path2, AssetBundle ab,string ABname)
    {
        Debug.Log("path2====" + path2);
        t.text = "path2 =" + path2;
        WWW www1 = new WWW(path2);
        yield return www1;
        if (www1.error == null)
        {
            if (!abDic.ContainsKey(ABname))
            {
                ab = www1.assetBundle;
                abDic.Add(ABname, ab);
            }
        }
        else
        {
            Debug.Log("www1====" + www1.error.ToString());
            t.text = "www1 =" + www1.error.ToString();
        }
    }
    IEnumerator LoadAssetBundle1(string path2, string ABname)
    {
        Debug.Log("path2====" + path2);
        t.text = "path2 =" + path2;
        WWW www1 = new WWW(path2);
        yield return www1;
        if (www1.error == null)
        {
            abDic.Add(ABname, www1.assetBundle);
            //cube = Instantiate(www1.assetBundle.LoadAsset("cube")) as GameObject;
            //cube.transform.position = new Vector3(0, 0, 5);
        }
        else
        {
            Debug.Log("www1====" + www1.error.ToString());
            t.text = "www2 =" + www1.error.ToString();
        }
    }
}
