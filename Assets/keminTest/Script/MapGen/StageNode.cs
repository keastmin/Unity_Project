using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class StageNode : MonoBehaviour
    {
        #region enum Node Type

        public enum NodeType
        {
            NoneNode,
            NormalNode,
            EventNode,
            EliteNode,
            RestNode,
            ShopNode,
            ChestNode
        }

        #endregion


        #region public 변수

        public Animator animator;
        public Sprite nodeSprite;
        public List<StageNode> nextNode;
        public List<StageNode> prevNode;
        //public bool selectEnable;
        public int x;
        public int floor;

        public Sprite[] sprites;

        #endregion


        #region private 변수

        [SerializeField] private bool _selectEnable;
        public bool selectEnable
        {
            get { return _selectEnable; }
            set
            {
                _selectEnable = value;
                UpdateNodeActivation();
            }
        }

        [SerializeField] private NodeType _nodeType;
        public NodeType nodeType
        {
            get { return _nodeType; }
            set
            {
                if(_nodeType != value)
                {
                    _nodeType = value;
                    UpdateNodeSprite();
                }
            }
        }

        public Button _button;
        private Coroutine _scaleCoroutine;
        private Vector2 _originScale;
        private Vector2 _targetScale;
        [SerializeField] private float _multiScale = 1.6f;
        [SerializeField] private float _scaleDuration = 1.0f;

        #endregion


        private void Start()
        {
            _button = GetComponent<Button>();
            if (_button != null)
            {
                _originScale = transform.localScale;
                _targetScale = _originScale * _multiScale;
                selectEnable = false;
                UpdateNodeSprite();
            }
            else
            {
                Debug.LogError("버튼을 찾을 수 없음");
            }
        }

        private void Update()
        {
            if (_button != null)
            {
                if (_selectEnable && _scaleCoroutine == null)
                {
                    _scaleCoroutine = StartCoroutine(DynamicButtonScale());
                }
                else if (!_selectEnable && _scaleCoroutine != null)
                {
                    StopCoroutine(_scaleCoroutine);
                    _scaleCoroutine = null;
                    transform.localScale = _originScale;
                }
            }
        }

        #region 코루틴 메서드

        private IEnumerator DynamicButtonScale()
        {
            while (_selectEnable)
            {
                yield return StartCoroutine(ScaleTo(_targetScale));

                yield return StartCoroutine(ScaleTo(_originScale));
            }

            transform.localScale = _originScale;
        }

        private IEnumerator ScaleTo(Vector2 scale)
        {
            Vector2 startScale = transform.localScale;
            float time = 0;

            while(time < _scaleDuration)
            {
                if (!_selectEnable)
                {
                    transform.localScale = _originScale;
                    yield break;
                }

                transform.localScale = Vector2.Lerp(startScale, scale, time / _scaleDuration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.localScale = scale;
        }

        #endregion

        void UpdateNodeActivation()
        {
            if (this.floor >= 0)
            {
                if (_button != null)
                {
                    _button.interactable = _selectEnable;
                    Debug.Log(_button);
                    Debug.Log(_selectEnable);
                }
                else
                {
                    Debug.Log(x + " " + floor);
                    Debug.LogError("버튼을 찾을 수 없음");
                }
            }
        }

        void UpdateNodeSprite()
        {
            if(sprites != null && (int)_nodeType < sprites.Length)
            {
                nodeSprite = sprites[(int)_nodeType];
                GetComponent<Image>().sprite = nodeSprite;
            }
        }

        public void InitNode(int x, int floor)
        {
            if (_button == null) _button = GetComponent<Button>();
            this.x = x;
            this.floor = floor;
            this.nextNode = new List<StageNode>();
            this.prevNode = new List<StageNode>();
            if (floor == 0) selectEnable = true;
        }

        public void OnClickNextStageInfo()
        {
            if (_selectEnable)
            {
                foreach(StageNode next in this.nextNode)
                {
                    next.selectEnable = true;
                }
                foreach(StageNode prev in this.prevNode)
                {
                    foreach(StageNode stopNode in prev.nextNode)
                    {
                        stopNode.selectEnable = false;
                    }
                }
            }
        }
    }
}