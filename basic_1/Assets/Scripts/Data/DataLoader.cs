using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    �ε��� ������ ������ ���缭 key, value �������� �����͸� ��Բ� �������̽��� ����
    ex) key�� int�� ã������ object�� ã������.. .��� �ٸ��� ������ �����ϰ� ������
*/
public interface DataLoader<T, t>
{
    Dictionary<T, t> MakeDict();
}
