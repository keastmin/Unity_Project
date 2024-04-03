using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawWorm : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_JawWorm jawWorm;

    private void Awake()
    {
        jawWorm = new B_JawWorm();
        jawWorm.player = player;

        Debug.Log(jawWorm.GetName() + " 생성"); // 테스트 코드
    }
}
