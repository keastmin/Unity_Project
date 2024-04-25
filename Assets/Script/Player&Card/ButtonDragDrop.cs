using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform; // 버튼의 RectTransform 컴포넌트
    private Canvas canvas; // 버튼이 속한 Canvas
    private Vector2 originalPosition; // 드래그 시작 시 버튼의 원래 위치
    private bool isDragging = false; // 드래그 중인지 여부를 나타내는 플래그
    public Card card;
    private void Awake()
    {
        card = GetComponent<Card>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // 버튼을 클릭했을 때 호출되는 콜백
    public void OnPointerDown(PointerEventData eventData)
    {
        // 드래그 시작 시 버튼의 원래 위치를 저장
        originalPosition = rectTransform.anchoredPosition;
        // 드래깅 플래그를 true로 설정
        isDragging = true;
    }

    // 버튼에서 손을 뗐을 때 호출되는 콜백
    public void OnPointerUp(PointerEventData eventData)
    {
        // 드래깅 플래그를 false로 재설정
        isDragging = false;
        // 버튼을 원래 위치로 되돌림
        rectTransform.anchoredPosition = originalPosition;

        // 마우스 위치에서 몬스터 태그를 가진 게임 오브젝트를 찾아 카드를 사용
        UseCardAtMousePosition();
    }

    // 버튼을 드래그하는 동안 호출되는 콜백
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // 드래그하는 동안 버튼의 위치를 업데이트
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    // 마우스 위치에서 몬스터 태그를 가진 게임 오브젝트가 있는지 확인하고 카드를 사용하는 함수
    private void UseCardAtMousePosition()
    {
        // 마우스 위치를 세계 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 마우스 위치에서 몬스터 태그를 가진 게임 오브젝트 찾기
        Collider2D collider = Physics2D.OverlapPoint(mouseWorldPosition);
        if (collider != null && collider.CompareTag("Monster"))
        {
            Debug.Log("몬스터 찾음");
            // 몬스터 태그를 가진 게임 오브젝트를 발견하면 해당 오브젝트의 카드 유형에 따라 적절한 카드를 사용
            Debug.Log(card.type);
            // 스위치 문을 사용하여 해당하는 카드의 Play 함수 실행
            switch (card.type)
            {
                case CardType.Attack:
                    // 형변환
                    AttackCard attackCard = (AttackCard)card;
                    Debug.Log("공격카드 사용");
                    attackCard = new AttackCard(card.name, card.description, card.cost, attackCard.damage);
                    if (attackCard != null)
                    {
                        attackCard.Play();
                    }
                    break;
                case CardType.Skill:
                    SkillCard skillCard = card as SkillCard;
                    if (skillCard != null)
                    {
                        skillCard.Play();
                    }
                    break;
                case CardType.Curse:
                    CurseCard curseCard = card as CurseCard;
                    if (curseCard != null)
                    {
                        curseCard.Play();
                    }
                    break;
                default:
                    Debug.LogError("Unknown Card Type");
                    break;
            }
        }
    }
}
