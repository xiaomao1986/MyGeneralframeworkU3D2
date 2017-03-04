using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
public class SavedText 
{
    public static void CreateFile(string path, string name, string jd,Callback _callback=null)
    {
        try
        {

            string s1 = path + name;
            string filename = Path.GetFileName(s1);
            string tSt = s1.Replace(filename, "");
            while (Directory.Exists(tSt) == false)
            {
                Directory.CreateDirectory(tSt);
            }

            Debug.Log("---jd---" + jd);
            My_debugNet.SendDebugLog(" jd 2---------------"+ jd);
            string p = path + name;
            Debug.Log("---p---" + p );
            My_debugNet.SendDebugLog("---p---" + p);
            StreamWriter sw;
            FileInfo t = new FileInfo(p);
            My_debugNet.SendDebugLog("---FileInfo---");
            if (!t.Exists)
            {
                My_debugNet.SendDebugLog("---Exists---");
                sw = t.CreateText();
            }
            else
            {
                My_debugNet.SendDebugLog("---12wqeqweqweq--");
                FileStream fs = new FileStream(path + name ,FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
            }
            My_debugNet.SendDebugLog("--- sw.Flush();--");
            sw.Flush();
            sw.Write(jd);
            sw.Close();
            sw.Dispose();
            My_debugNet.SendDebugLog("--- _callback--");
            if (_callback != null)
            {
                My_debugNet.SendDebugLog("---p222222222222---" );
                _callback();
            }
        }
        catch (System.Exception e)
        {
            My_debugNet.SendDebugLog("---System.Exception--" + e.ToString());
        }
    }
}
