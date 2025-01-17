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
        /// 게임 시작 시 게임에 필요한 세팅 초기화
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
        /// 각 버튼을 클릭했을 때 연결되는 씬 매칭
        /// </summary>
        /// <param name="index">스테이지의 번호</param>
        private void LoadScene(int index)
        {
            GameManager.Instance.CurrentStage = index;
            string sceneName = _stages[index].SceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}
