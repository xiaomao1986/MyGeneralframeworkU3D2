using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
       // string testStr = "parent/child/child2|mylua|LuaMag";


      string ss=  TestPrefabName<mylua>(gameObject, "123", "456");

        Debug.Log("sss=="+ss);
        ss = TestPrefabName(gameObject, "123", "456");
        Debug.Log("sss=1=" + ss);

        mylua t1 = GetComponent1<mylua>(gameObject, "123", "456");
        Debug.Log("sss=2=" + t1.gameObject.name);


     GameObject g =  GetChild(gameObject,"123");

        Debug.Log("gggg=" + g.name);
    }


    public static string TestPrefabName<T>(GameObject g, params string[] testStr) where T : MonoBehaviour
    {
        GameObject tmpObj = g;
        if (tmpObj == null) return " 传入 GameObject 空";
        else
        {
            for (int i = 0; i < testStr.Length; i++)
            {
                if (tmpObj.transform.FindChild(testStr[i]) == null)
                    return "没有找到 " + testStr[i] + "物体";
                else
                    tmpObj = tmpObj.transform.FindChild(testStr[i]).gameObject;
            }
            if (tmpObj.GetComponent<T>() != null)
                return null;
            else
                return tmpObj.name + "   物体上没有找到  相应 脚本";
        }
    }
    public static string TestPrefabName(GameObject g, params string[] testStr)
    {
        GameObject tmpobj = g;
        if (tmpobj == null)
            return " 传入 GameObject 空";
        else
        {
            for (int i = 0; i < testStr.Length; i++)
            {
                if (tmpobj.transform.FindChild(testStr[i]) == null)
                    return "没有找到 " + g.name + " 子  " + testStr[i] + "   物体";
                else
                    tmpobj = tmpobj.transform.FindChild(testStr[i]).gameObject;
            }
        }
        return null;
    }
    public static GameObject GetChild(GameObject g, params string[] testStr)
    {
        string Error = "";
        GameObject tmpobj = g;
        if (tmpobj == null)
        {
            Error = "传入 GameObject 空";
            return null;
        }
        else
        {
            for (int i = 0; i < testStr.Length; i++)
            {
                if (tmpobj.transform.FindChild(testStr[i]) == null)
                {
                    Error = "没有找到 " + testStr[i] + "物体";
                    return null;
                }
                else
                    return tmpobj.transform.FindChild(testStr[i]).gameObject;
            }
        }
        throw new System.Exception(Error);
    }
    public static T GetComponent1<T>(GameObject g, params string[] testStr)
    {
        string Error = "";
        T t1 = default(T);
        GameObject tmpobj = g;
        if (tmpobj == null)
        {
            Error = "传入 GameObject 空";
            return t1;
        }
        else
        {
            for (int i = 0; i < testStr.Length; i++)
            {
                if (tmpobj.transform.FindChild(testStr[i]) == null)
                {
                    Error = "没有找到 " + testStr[i] + "物体";
                    return t1;
                }
                else
                {
                    tmpobj = tmpobj.transform.FindChild(testStr[i]).gameObject;
                }
            }
            if (tmpobj.GetComponent<T>() == null)
            {
                return t1;
            } else
                 t1 = tmpobj.GetComponent<T>();
            return t1;
        }
        throw new System.Exception(Error);
    }

    public  string TestPrefabsName(GameObject g, string testStr)
    {
        //检查 testStr是否空
        if (testStr == string.Empty || testStr == "")
        {
            return "传入 空 字符串 错误！！！";
        }
        //检查 testStr 格式 是否 带有 /分隔符
        int StrIndex = testStr.IndexOf("/");
        if (StrIndex < 1)
        {
   
           // return "传入 字符串 格式错误！！！";
        }
        int StrIndex1 = testStr.IndexOf("|");
        string str = "";
        string str2 = ""; 
        string[] childArrys;
        string[] ComponentArrys;
        if (StrIndex1 > 0)
        {
            str = testStr.Substring(0, StrIndex1);
            Debug.Log("str===" + str);
            childArrys = str.Split('/');
            str2 = testStr.Substring(StrIndex1);
            Debug.Log("str2===" + str2);
            ComponentArrys= str2.Split('|');
            string PrefabNameBack = "";// TestPrefabName(g, childArrys);
            if (PrefabNameBack==""||PrefabNameBack==string.Empty)
            {
                TestPrefabName<MonoBehaviour>(g, ComponentArrys);
            }
            else
            {
                return null;
            }
        }
        str = testStr;
        Debug.Log("str==2=" + str);
        childArrys = testStr.Split('/');
        for (int i = 0; i < childArrys.Length; i++)
        {
            Debug.Log("childArrys===" + childArrys[i]);
        }

        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
