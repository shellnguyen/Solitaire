﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class AppController : Singleton<AppController>
{

    private void OnEnable()
    {
        //EventManager.Instance.Register(Solitaire.Event.SaveData, OnSaveSetting);
        //EventManager.Instance.Register(Solitaire.Event.LoadData, OnLoadSetting);
    }

    private void OnDisable()
    {
        //EventManager.Instance.Unregister(Solitaire.Event.SaveData, OnSaveSetting);
        //EventManager.Instance.Unregister(Solitaire.Event.LoadData, OnLoadSetting);
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SaveSetting()
    {
        CheckSaveSetting();
    }

    public void LoadSetting()
    {
        CheckLoadSetting();
        if(GameSetting.Instance.enableAds)
        {
            AdsController.Instance.Initialized();
        }  
    }

    private bool CheckSaveSetting()
    {
        FileStream fs = new FileStream(Common.SAVE_PATH, FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            SettingData data = new SettingData();
            data.enableAds = GameSetting.Instance.enableAds;
            data.enableAudio = GameSetting.Instance.enableAudio;
            data.currentCardSkin = GameSetting.Instance.currentCardSkin;
            formatter.Serialize(fs, data);
        }
        catch (SerializationException e)
        {
            Logger.Instance.PrintExc(Common.DEBUG_TAG, "Failed to serialize. Reason: " + e.Message);
            return false;
        }
        finally
        {
            fs.Close();
        }

        return true;
    }

    private bool CheckLoadSetting()
    {
        if(File.Exists(Common.SAVE_PATH))
        {
            FileStream fs = new FileStream(Common.SAVE_PATH, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                SettingData data = (SettingData)formatter.Deserialize(fs);

                GameSetting.Instance.enableAds = data.enableAds;
                GameSetting.Instance.enableAudio = data.enableAudio;
                GameSetting.Instance.currentCardSkin = data.currentCardSkin;
            }
            catch (SerializationException e)
            {
                Logger.Instance.PrintExc(Common.DEBUG_TAG, "Failed to deserialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            Logger.Instance.PrintError(Common.DEBUG_TAG, "File not found !!!");
            return false;
        }

        return true;
    }
}
