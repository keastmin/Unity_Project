using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin
{
    public class NodeTypeManager : MonoBehaviour
    {
        public static NodeTypeManager instance;

        [SerializeField]
        private NodeSpriteMapping[] nodeSprite;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if(nodeSprite.Length != System.Enum.GetValues(typeof(NodeType)).Length)
            {
                Debug.LogError("배열의 길이가 열거형의 길이와 다릅니다.");
            }
        }

        // 노드 타입에 맞는 이미지 반환
        public Sprite GetSprite(NodeType nodeType)
        {
            return nodeSprite[(int)nodeType].sprite;
        }

        // 확률에 따른 노드 타입 랜덤 반환
        public NodeType GetRandomNodeType()
        {
            float rand = Random.value;
            if(rand < 0.45f)
            {
                return NodeType.Normal;
            }
            else if(rand < 0.67f)
            {
                return NodeType.Event;
            }
            else if(rand < 0.83f)
            {
                return NodeType.Elite;
            }
            else if(rand < 0.95f)
            {
                return NodeType.Rest;
            }
            return NodeType.Merchant;
        }
    }
}