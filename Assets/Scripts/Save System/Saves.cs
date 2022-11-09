using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Saves
{
    [System.Serializable]
    public struct EnemyData
    {
        public string m_Uuid;
        public int m_Health;
        public Transform enemyPosition;
    }
    
    public int m_Score;
    public List<EnemyData> m_EnemyData = new List<EnemyData>();
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(Saves a_Saves);
    void LoadFromSaveData(Saves a_Saves);
}
