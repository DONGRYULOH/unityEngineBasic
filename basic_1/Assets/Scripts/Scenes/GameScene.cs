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

        gameObject.GetOrAddComponent<CursorController>();
    }
    

    public override void Clear()
    {

    }



}
