using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeEventTest : MonoBehaviour, InterfaceTest
{
    public void Print()
    {
        throw new System.NotImplementedException();
    }

    // 애니메이션 callback 이벤트를 사용
    // 캐릭터가 검을 휘두르는 애니메이션이 있는데 검이 몬스터한테 부딛혔을 때 사운드를 발생시키는 callback 이벤트를 호출시킨다던지
    // 여러가지 응용이 가능함 
    void TestEventCallback()
    {
       // Debug.Log("event recived");
    }
}
