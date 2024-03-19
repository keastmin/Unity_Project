using UnityEngine;

public class CardSc : MonoBehaviour
{
    PlayerSc playerSc;

    // 카드 변수들
    public string cardName; // 카드 이름
    public string cardType; // 카드 타입
    public int cost; // 비용
    public int damage; // 데미지
    public int block; // 방어량


    public void Play() // 대상이 있는 카드를 사용할때 불러올 함수
    {
        if (playerSc.energy > cost) // 플레이어의 energy가 cost 보다 크면
        {
            playerSc.TakeEnergy(cost); // 플레이어의 energy를 cost만큼 줄이기
        }
        if(block > 0)
        {
            playerSc.GainBlock(block);
        }
    }

    public void Play(GameObject obj) // 대상이 있는 카드를 사용할때 불러올 함수
    {
        if (damage > 0) // 데미지가 0보다 크고 대상의 체력이 0 이상이면
        {
            //몬스터의 TakeDamage함수 불러오기
        }
        if (playerSc.energy > cost) // 플레이어의 energy가 cost 보다 크면
        {
            playerSc.TakeEnergy(cost); // 플레이어의 energy를 cost만큼 줄이기
        }
        if (block > 0)
        {
            playerSc.GainBlock(block);
        }
    }
}