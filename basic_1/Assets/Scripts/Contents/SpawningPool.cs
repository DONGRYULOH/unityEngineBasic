using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    int _monsterCount = 0;

    int _reserveCount = 0;

    int _keepMonsterCount = 0;

    Vector3 _spawnPos;

    float _spawnRadius = 15.0f;

    float _spawnTime = 5.0f;

    public void AddMonsterCount(int value)
    {
        _monsterCount += value;
    }

    public void SetKeepMonsterCount(int count)
    {
        _keepMonsterCount = count;
    }


    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    // Update is called once per frame
    void Update()
    {
        while(_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    // �ٷ� ���͸� �����ϴ°� �ƴ϶� �������� ��ٷȴٰ� �����Ұ��̱� ������ ��ٸ��� �ð����� �ٸ� �۾��� ����ɼ� �ֵ��� �ڷ�ƾ ó��
    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));

        GameObject go = Managers.Game.Spawn(Defines.WorldObject.Monster, "Warrior");
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>(); // ��ã�� ������Ʈ�� �̿��ؼ� ������ �� �ִ� �������� �Ǵ�

        Vector3 randomPos;
        while (true)
        {
            Vector3 randomDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randomDir.y = 0;
            randomPos = go.transform.position + randomDir;

            // ������ �� �ִ� �������� üũ
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randomPos, path))
                break;
        }

        go.transform.position = randomPos;
        _reserveCount--;
    }
}
