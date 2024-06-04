using keastmin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin {
    public static class NodePreset
    {
        // CreateMap의 CreateButton 함수에서 node의 Type을 변화할 때 사용할 함수
        public static StageNode.NodeType GetRandomNodeType(int floor)
        {
            // 규칙: 0층은 무조건 일반 전투, 8층은 무조건 보물 방, 14층은 무조건 휴식
            if (floor != 0 && floor != 8 && floor != 14)
            {
                // StageNode에 선언된 enum의 순서대로 확률을 명시하면 아래와 같다.
                // NormalNode = 45%, EventNode = 22%, EliteNode = 16%, RestNode = 12%, ShopNode = 5%
                int[] probabilities = new int[] { 45, 67, 83, 95, 100 };

                // 0 ~ 100 사이의 값 리턴
                int randomPoint = UnityEngine.Random.Range(0, 100);

                for (int i = 0; i < probabilities.Length; i++)
                {
                    if (randomPoint < probabilities[i])
                    {
                        return (StageNode.NodeType)(i + 1);
                    }
                }
            }
            else if(floor == 0)
            {
                return StageNode.NodeType.NormalNode;
            }
            else if(floor == 8)
            {
                return StageNode.NodeType.ChestNode;
            }
            else if(floor == 14)
            {
                return StageNode.NodeType.RestNode;
            }
            return StageNode.NodeType.NoneNode;
        }


        #region 노드 타입 번호 리턴 함수

        public static int GetEliteNodeNum()
        {
            return (int)StageNode.NodeType.EliteNode;
        }
        public static int GetRestNodeNum()
        {
            return (int)StageNode.NodeType.RestNode;
        }
        public static int GetShopNodeNum()
        {
            return (int)StageNode.NodeType.ShopNode;
        }

        #endregion
    }
}