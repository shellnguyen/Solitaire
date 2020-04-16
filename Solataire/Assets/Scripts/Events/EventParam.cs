using System;
using System.Collections.Generic;

public class EventParam
{
    protected int m_EventID;
    private Dictionary<string, object> m_ParamList;

    public int EventID
    {
        get
        {
            return m_EventID;
        }

        set
        {
            m_EventID = value;
        }
    }

    public EventParam()
    {
        m_EventID = -1;
        m_ParamList = new Dictionary<string, object>();
        
    }

    public void Add<T>(string key, T value)
    {
        m_ParamList.Add(key, value);
    }

    public string GetString(string key)
    {
        try
        {
            return (string)m_ParamList[key];
        }
        catch(Exception ex)
        {
            Logger.Instance.PrintExc(Common.DEBUG_TAG, ex.Message);
        }

        return null;
    }

    public int GetInt(string key)
    {
        try
        {
            return (int)m_ParamList[key];
        }
        catch(Exception ex)
        {
            Logger.Instance.PrintExc(Common.DEBUG_TAG, ex.Message);
        }

        return -1;
    }

    public float GetFloat(string key)
    {
        try
        {
            return (float)m_ParamList[key];
        }
        catch (Exception ex)
        {
            Logger.Instance.PrintExc(Common.DEBUG_TAG, ex.Message);
        }

        return -999.9f;
    }

    public bool GetBoolean(string key)
    {
        try
        {
            return (bool)m_ParamList[key];
        }
        catch (Exception ex)
        {
            Logger.Instance.PrintExc(Common.DEBUG_TAG, ex.Message);
        }

        return false;
    }
}
