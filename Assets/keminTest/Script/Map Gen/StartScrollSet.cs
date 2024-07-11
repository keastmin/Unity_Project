using UnityEngine;

namespace keastmin
{
    public class StartScrollSet : MonoBehaviour
    {
        // 스크롤뷰의 컨텐츠 오브젝트의 시작 위치를 조절하기 위한 변수
        private RectTransform _scrollViewContent;

        // 스크롤의 시작 위치와 층이 올라갈 때마다 증가할 위치 값
        private int _startHeight = 450;
        private int _floorHeight = 80;

        private void Awake()
        {
            _scrollViewContent = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (MapCreatorTest.instance != null)
            {
                int currFloor = MapCreatorTest.instance.GetCurrentActiveFloor();
                Vector2 newPos = _scrollViewContent.anchoredPosition;
                newPos.y = _startHeight - (currFloor * _floorHeight);
                _scrollViewContent.anchoredPosition = newPos;
            }
        }
    }
}