using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (ThirdPersonController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }
        public static void Checkpoint (ThirdPersonController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.checkpoint";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

        public static void SaveEnemy (BaseEnemyAI enemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + enemy.name;
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();

    }

            public static void SaveInventory (Inventory inv)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Inventory.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inv);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
           return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

        public static PlayerData LoadCheckpoint ()
    {
        string path = Application.persistentDataPath + "/player.checkpoint";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
           return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

        public static EnemyData LoadEnemy (BaseEnemyAI enemy)
    {
        string path = Application.persistentDataPath + enemy.name;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
           return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
                public static InventoryData LoadInventory ()
    {
        string path = Application.persistentDataPath + "/Inventory.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
           return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
