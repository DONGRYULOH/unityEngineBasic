using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    <���� ��ü�� �����ϴ� �Ŵ���>

    �Ŵ����� MonoBehaviour�� ���� ������ ���� �����ɶ� ���� ������Ʈ�� ��� �ε�ǰ� 
    MonoBehaviour�� ���� C# ��ũ��Ʈ�� ������� ������Ʈ���� ��� �۾��� ����Ǳ� ������ �Ѱ� �Ŵ����� ��� MonoBehavior�� �ʿ���
*/
public class Managers : MonoBehaviour
{
    static Managers S_Instance;
    static Managers Instance { get { Init(); return S_Instance; } }

    // ���� �Ŵ���
    InputManager input = new InputManager();
    UiManager ui = new UiManager();
    ResourcesManager resource = new ResourcesManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();


    public static InputManager Input { get { return Instance.input; } }    
    public static UiManager UI { get { return Instance.ui; } }    
    public static ResourcesManager Resource { get { return Instance.resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }

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
        // Manager ������Ʈ�� ������ �����Ѵٴ� ������ ���� ������ null üũ�� ����� ��                 
        if (S_Instance == null) {
            GameObject manager = GameObject.Find("Manager");

            if (manager == null) {
                manager = new GameObject { name = "@Managers" };
                manager.AddComponent<Managers>();
            }

            DontDestroyOnLoad(manager);
            S_Instance = manager.GetComponent<Managers>();

            S_Instance._sound.Init(); // ���ӽ��۽� Sound ���̱�
        }
    }

    static public void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
    }
}