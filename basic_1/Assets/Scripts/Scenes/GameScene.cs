using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();
        SceneType = Defines.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        // 플레이어가 해당 스킬을 쓰면 4초 뒤에 스킬이 나간다고 했을때

        // 코루틴에서 그 스킬을 시전하고 4초 뒤에 다시 코루틴으로 돌아와서 스킬을 실행시킴
        // 코루틴을 사용하지 않는다면 4초가 지났는지 계속 검사하다가 스킬을 실행시키는 방법이 필요한데 4초가 지났는지 검사하는 동안은 다른 작업을 수행할 수 없다.
        // 4초라는 시간이 낭비되고 있으므로 코루틴을 사용해서 멀티 태스킹 작업을 할 수 있다.

        // 코루틴 예시를 들면 
        // 롤에서 이즈리얼이 궁을쓰면 쿨타임이 1분이라고 했을때 궁을 다시 쓰기 위해서 1분을 기다려야 되는데 
        // 1분이 지났으면 궁을 다시 활성화 상태로 만드는 것을 코루틴으로 만들어서 최초에 궁을 썼을때 코루틴을 호출하고 
        // 나머지 1분은 다른 작업을 수행할 수 있도록 하다가 플레이어가 중간마다 신비한 화살로 적 유닛을 명중시키면 
        // 다시 코루틴을 호출해서 궁의 쿨타임을 감소시키고 만약에 궁의 쿨타임이 없어지면 코루틴을 종료함 

        co = StartCoroutine("ExplodeAfter4Seconds", 4.0f);
        Debug.Log("플레이어 이동");
        StartCoroutine("CoStopExplode", 2.0f);        
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Excute");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }            
    }
    
    IEnumerator ExplodeAfter4Seconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds); // seconds 동안 해당 코루틴을 중지(중지 하는 동안 다른 작업을 수행하다가 정해진 초가 지나면 다시 해당 코루틴을 호출)
        Debug.Log("Explode Excute");
        co = null; // Explode 실행됬으니까 해당 코루틴 사용 X
    }

    public override void Clear()
    {

    }



}
