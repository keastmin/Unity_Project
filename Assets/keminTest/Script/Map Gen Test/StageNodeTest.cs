using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class StageNodeTest : MonoBehaviour
    {

        #region public ����

        public List<StageNodeTest> nextNode; // ���� ��� ����Ʈ
        public List<StageNodeTest> prevNode; // ���� ��� ����Ʈ

        #endregion


        #region private ����

        [SerializeField]
        Animator _animator; // Ȱ��ȭ ��ư�� ũ�� �ִϸ��̼�

        private NodeType _nodeType; // ����� Ÿ��
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

        private bool _isActive; // ����� Ȱ��ȭ ����
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
            // ���� ���� ���� ��� ����Ʈ �ʱ�ȭ
            nextNode = new List<StageNodeTest>();
            prevNode = new List<StageNodeTest>();

            // ��ư�� Ȱ��ȭ ���� false
            isActive = false;
        }

        void Start()
        {
            
        }

        void Update()
        {

        }

        // ��� Ȱ��ȭ���� �ִϸ��̼� ����
        private void OnEnable()
        {
            AnimatorActive();
        }

        #region ����� Ÿ�� ���� �޼���

        private void ChangeNodeType()
        {
            Image image = GetComponent<Image>();
            if(image != null)
            {
                image.sprite = NodeTypeManager.instance.GetSprite(_nodeType);
            }
        }

        #endregion


        #region ��� ��ư�� Ȱ��ȭ ���� �޼���

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


        #region ��ư�� ���� �� ����Ǵ� �޼���

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