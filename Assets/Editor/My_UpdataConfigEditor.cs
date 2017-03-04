using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class My_UpdataConfigEditor
{

    private static string path1 = "";
    public static void CreateUpdataConfig()
    {
        GUI.Label(new Rect(250, 40, 60, 20), "选择路径：");
        GUI.TextArea(new Rect(310, 40, 500, 20), path1);
        if (GUI.Button(new Rect(810, 40, 100, 20), "选择"))
        {
            path1 = EditorUtility.OpenFolderPanel("", "", "");
            DirectoryInfo dir = new DirectoryInfo(path1);
            foreach (DirectoryInfo NextFolder in dir.GetDirectories("*"))
            {
              
            }
        }
        //if (GUI.Button(new Rect(810, 10, 100, 20), "加载UpdataConfig"))
        //{
        //    Debug.Log("11111111");
        //}
    }
}
