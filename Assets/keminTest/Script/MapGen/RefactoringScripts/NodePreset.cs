using keastmin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin {
    public static class NodePreset
    {
        // CreateMap의 CreateButton 함수에서 node의 Type을 변화할 때 사용할 함수
        public static StageNode.NodeType GetRandomNodeType()
        {
            // NodeType에서 모든 값 가져오기
            Array types = Enum.GetValues(typeof(StageNode.NodeType));

            // NoneNode를 제외한 랜덤 NodeType 반환
            int randomType = UnityEngine.Random.Range(1, types.Length);
            return (StageNode.NodeType)types.GetValue(randomType);
        }
    }
}