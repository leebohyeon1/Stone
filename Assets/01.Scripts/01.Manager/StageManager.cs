using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LBH
{
    [System.Serializable]
    public class Stage
    {
        public string SceneName;
        private bool _isClear = false;

        public void ClearStage()
        {
            _isClear = true;
        }

        public bool IsClear()
        {
            return _isClear;
        }
    }


    public class StageManager : MonoBehaviour
    {
        [SerializeField] private List<Stage> _stages;
        [SerializeField] private List<Button> _stageButtons;
        
        private void Start()
        {
            int i;
            for (i = 0; i < _stages.Count; i++)
            {
                int index = i;
                _stageButtons[i].onClick.AddListener(() => { LoadScene(index); });
            }

            Initialize();
        }

        /// <summary>
        /// ���� ���� �� ���ӿ� �ʿ��� ���� �ʱ�ȭ
        /// </summary>
        private void Initialize()
        {
            int i;

            for (i = 0; i < _stages.Count; i++)
            {
                if (GameManager.Instance.ClearStages[i])
                {
                    _stages[i].ClearStage();
                }

                if (_stages[i].IsClear())
                {
                    _stageButtons[i].interactable = false;
                }
                else if (i >= 1 && _stages[i - 1].IsClear())
                {
                    _stageButtons[i].interactable = true;
                }

                if (!_stages[0].IsClear())
                {
                    _stageButtons[0].interactable = true;
                }
            }
        }

        /// <summary>
        /// �� ��ư�� Ŭ������ �� ����Ǵ� �� ��Ī
        /// </summary>
        /// <param name="index">���������� ��ȣ</param>
        private void LoadScene(int index)
        {
            GameManager.Instance.CurrentStage = index;
            string sceneName = _stages[index].SceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}
