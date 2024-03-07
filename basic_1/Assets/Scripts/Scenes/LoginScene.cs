using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Defines.Scene.Login;

        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < 5; i++)
        {
            list.Add(Managers.Resource.Instantiate("unitychan"));
        }

        foreach(GameObject ob in list)
        {
            Managers.Resource.Destroy(ob);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Defines.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }
}
