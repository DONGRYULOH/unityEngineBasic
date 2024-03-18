using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Type���� �����ϴ� Ŭ����

public class Defines
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }

    // �÷��̾�� ���Ͱ� �������� ����� ���� 
    public enum State
    {
        Die,
        Moving,
        Wait,
        Skill
    }

    public enum Layer
    {
        Monster1 = 6,
        Ground = 7,
        Block = 8,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        QuaterView,
    }
}
