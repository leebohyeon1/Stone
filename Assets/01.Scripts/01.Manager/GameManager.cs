using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace LBH
{
    [System.Serializable]
    public class SaveData
    {
        public List<bool> ClearStages = new List<bool>();
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance {get; private set;}
        private string _path;

        public List<bool> ClearStages;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void Start()
        {
            _path = Path.Combine(Application.dataPath + "/Data/", "database.json");
        }
        
        private void Update()
        {

        }

        /// <summary>
        /// ���̺� ������ �ҷ�����
        /// </summary>
        public void LoadGame()
        {
            SaveData saveData = new SaveData();

            if(!File.Exists(_path))
            {
                for (int i = 0; i < ClearStages.Count; i++)
                {
                    ClearStages[i] = false;
                }
            }
            else
            {
                // �ҷ�����
                string loadJson = File.ReadAllText(_path);
                saveData = JsonUtility.FromJson<SaveData>(loadJson);

                if (saveData != null)
                {
                    for (int i = 0; i < ClearStages.Count; i++)
                    {
                        ClearStages.Add(saveData.ClearStages[i]);
                    }
                }
            }
        }

        /// <summary>
        /// ���̺� ������ ����
        /// </summary>
        public void SaveGame()
        {
            SaveData saveData = new SaveData();

            for (int i = 0; i < ClearStages.Count; i++)
            {
                saveData.ClearStages.Add(ClearStages[i]);
            }

            // ����
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_path, json);
        }
    }
}
