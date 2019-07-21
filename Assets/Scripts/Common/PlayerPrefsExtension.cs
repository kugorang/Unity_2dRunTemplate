using UnityEngine;

public static class PlayerPrefsExtension
{
    public static void SetBool(string key, bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
    public static bool GetBool(string key)
    {
        int tmp = PlayerPrefs.GetInt(key);

        if (tmp == 1)
        {
            return true;
        }

        return false;
    }

    public static bool GetBool(string key, bool defaultValue)
    {
        int tmp = PlayerPrefs.GetInt(key);

        if (tmp == 1)
        {
            return true;
        }
        else if (tmp == 0)
        {
            return false;
        }

        return defaultValue;
    }

    public static void SetVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "X", value.x);
        PlayerPrefs.SetFloat(key + "Y", value.y);
        PlayerPrefs.SetFloat(key + "Z", value.z);
    }

    public static Vector3 GetVector3(string key)
    {
        Vector3 v3 = Vector3.zero;

        v3.x = PlayerPrefs.GetFloat(key + "X", 0f);
        v3.y = PlayerPrefs.GetFloat(key + "Y", 0f);
        v3.z = PlayerPrefs.GetFloat(key + "Z", 0f);

        return v3;
    }
}
