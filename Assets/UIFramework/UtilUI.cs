using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class UtilUI
{
    private UtilUI() { }
    
    /// <summary>
    /// 把对象放入到指定的父物体下面
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    public static void addChildToParent(Transform parent, Transform child)
    {
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// 查找父物体下面的子物体
    /// </summary>
    /// <param name="goParent"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public static Transform findChild(GameObject goParent, string childName)
    {
        Transform transChild = goParent.transform.Find(childName);
        if (transChild == null)//如果没有找到
        {
            foreach (Transform t in goParent.transform)//父物体下面的所有子物体
            {
                transChild=findChild(t.gameObject, childName);
                if (transChild != null)
                {
                    return transChild;
                }
            }
        }
        return transChild;
    }
    /// <summary>
    /// 清除内存
    /// </summary>
    public static void clearMemory()
    {
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }
    public static void OpenLoadSceneHelper()
    {
        GameObject uiRoot = GameObject.FindGameObjectWithTag("UIRoot");
        if (uiRoot != null)
        {
            GameObject goLoadHelp = findChild(uiRoot, "LoadSceneHelper").gameObject;
            if (goLoadHelp.activeInHierarchy == false)
            {
                goLoadHelp.SetActive(true);
            }
        }
    }
}