using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{       
    protected override void Init()
    {
        base.Init();
        SceneType = Defines.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        for (int i = 0; i < 5; i++)
        {
            Managers.Resource.Instantiate("unityChan");
        }
    }

    public override void Clear()
    {

    }

}
