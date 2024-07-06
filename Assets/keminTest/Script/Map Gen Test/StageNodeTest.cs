using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class StageNodeTest : MonoBehaviour
    {

        #region public 변수

        public List<StageNodeTest> nextNode; // 다음 노드 리스트
        public List<StageNodeTest> prevNode; // 이전 노드 리스트

        #endregion


        #region private 변수

        [SerializeField]
        Animator _animator; // 활성화 버튼의 크기 애니메이션

        private NodeType _nodeType; // 노드의 타입
        public NodeType nodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                _nodeType = value;
                ChangeNodeType();
            }
        }

        private bool _isActive; // 노드의 활성화 여부
        public bool isActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                ChangeButtonActive();
            }
        }

        #endregion

        private void Awake()
        {
            // 다음 노드와 이전 노드 리스트 초기화
            nextNode = new List<StageNodeTest>();
            prevNode = new List<StageNodeTest>();

            // 버튼의 활성화 여부 false
            isActive = false;
        }

        void Start()
        {
            
        }

        void Update()
        {

        }

        // 노드 활성화마다 애니메이션 실행
        private void OnEnable()
        {
            AnimatorActive();
        }

        #region 노드의 타입 변경 메서드

        private void ChangeNodeType()
        {
            Image image = GetComponent<Image>();
            if(image != null)
            {
                image.sprite = NodeTypeManager.instance.GetSprite(_nodeType);
            }
        }

        #endregion


        #region 노드 버튼의 활성화 변경 메서드

        private void ChangeButtonActive()
        {
            Button _button = GetComponent<Button>();
            if(_button != null)
            {
                _button.interactable = _isActive;
                AnimatorActive();
            }
        }

        private void AnimatorActive()
        {
            if(_animator != null)
            {
                _animator.SetBool("IsActive", _isActive);
            }
        }

        #endregion


        #region 버튼을 누를 때 실행되는 메서드

        public void OnClickNodeButton()
        {
            foreach(StageNodeTest prev in prevNode)
            {
                foreach (StageNodeTest pNext in prev.nextNode)
                {
                    pNext.isActive = false;
                }
            }

            foreach(StageNodeTest next in nextNode)
            {
                next.isActive = true;
            }
        }

        #endregion
    }
}