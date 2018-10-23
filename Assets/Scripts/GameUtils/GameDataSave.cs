using UnityEngine;
using System.Collections;
/// <summary>
/// 1：系统PlayerPrefs数据保存
/// 2：二进制数据
/// </summary>
public class GameDataSave {
    private GameDataSave() { }
    //数据的查找
    public static bool isData(string key)
    {
        return PlayerPrefs.HasKey(key);//是否有此key
    }

    //数据的保存
    public static void saveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static void saveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key,value);
    }
    public static void saveData(string key, string value)
    {
        PlayerPrefs.SetString(key,value);
    }
    //数据的读取
    public static int getData(string key, int defaultValue)
    {
        if (defaultValue == 0)
        {
            return PlayerPrefs.GetInt(key);
        }
        else {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        
    }
    public static float getData(string key, float defaultValue)
    {
        if (defaultValue == 0.0f)
        {
            return PlayerPrefs.GetFloat(key);
        }
        else {
            return PlayerPrefs.GetFloat(key,defaultValue);
        }
    }
    public static string getData(string key, string defaultValue)
    {
        if (string.IsNullOrEmpty(defaultValue))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
    }
    //数据的删除
    public static void delData(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    public static void delAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
