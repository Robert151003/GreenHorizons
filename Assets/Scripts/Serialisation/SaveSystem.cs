using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void saveManager(Manager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        managerData data = new managerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static managerData loadManager()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                managerData data = formatter.Deserialize(stream) as managerData;
                stream.Close();
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading save file: " + e.Message);
                stream.Close();
                return null;
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

}
