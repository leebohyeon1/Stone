using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LBH
{
    [System.Serializable]
    public class SaveData
    {
        public List<bool> ClearStages = new List<bool>();
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}
        private string _path;

        public List<bool> ClearStages;
        public int CurrentStage;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            _path = Path.Combine(Application.dataPath, "database.json");
            LoadGame();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && Input.GetKeyUp(KeyCode.R))
            {
                ResetGame();
            }
        }

        /// <summary>
        /// 세이브 데이터 불러오기
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
                SaveGame();
            }
            else
            {
                // 불러오기
                string loadJson = File.ReadAllText(_path);
                saveData = JsonUtility.FromJson<SaveData>(loadJson);

                if (saveData != null)
                {
                    for (int i = 0; i < ClearStages.Count; i++)
                    {
                        ClearStages[i] = saveData.ClearStages[i];
                    }
                }
            }
        }

        /// <summary>
        /// 세이브 데이터 저장
        /// </summary>
        public void SaveGame()
        {
            SaveData saveData = new SaveData();

            for (int i = 0; i < ClearStages.Count; i++)
            {
                saveData.ClearStages.Add(ClearStages[i]);
            }

            // 저장
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_path, json);
        }

        /// <summary>
        ///  게임 데이터 초기화
        /// </summary>
        public void ResetGame()
        {
            for (int i = 0; i < ClearStages.Count; i++)
            {
                ClearStages[i] = false;
            }

            SaveGame();
            SceneManager.LoadScene("01.Title");
        }

        public void ClearStage()
        {
            ClearStages[CurrentStage] = true;
            SaveGame();
            SceneManager.LoadScene("01.Title");
        }
    }
}
