using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    <게임 전체를 관리하는 매니저>

    매니저에 MonoBehaviour를 붙인 이유는 씬이 구성될때 게임 오브젝트가 모두 로드되고 
    MonoBehaviour가 붙은 C# 스크립트로 만들어진 컴포넌트들이 모두 작업이 수행되기 때문에 총괄 매니저의 경우 MonoBehavior가 필요함
*/
public class Managers : MonoBehaviour
{
    static Managers S_Instance;
    static Managers Instance { get { Init(); return S_Instance; } }

    // 각종 매니저
    InputManager input = new InputManager();
    UiManager ui = new UiManager();
    ResourcesManager resource = new ResourcesManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();


    public static InputManager Input { get { return Instance.input; } }    
    public static UiManager UI { get { return Instance.ui; } }    
    public static ResourcesManager Resource { get { return Instance.resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }

    public static Managers GetInstance() {
        Init();
        return Instance;
    }       

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Update()
    {
        input.OnUpdate();    
    }

    static void Init() {
        // Manager 오브젝트가 무조건 존재한다는 보장이 없기 때문에 null 체크를 해줘야 함                 
        if (S_Instance == null) {
            GameObject manager = GameObject.Find("Manager");

            if (manager == null) {
                manager = new GameObject { name = "@Managers" };
                manager.AddComponent<Managers>();
            }

            DontDestroyOnLoad(manager);
            S_Instance = manager.GetComponent<Managers>();

            S_Instance._sound.Init(); // 게임시작시 Sound 붙이기
            S_Instance._pool.Init(); // pool 객체를 담을 Root를 만들어줌
        }
    }

    static public void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();

        Pool.Clear();
    }
}
