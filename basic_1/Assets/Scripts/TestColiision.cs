using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColiision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition); // screen ��ǥ(x, y) ������� �ȼ� ����
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // screen ���̴� ����?

        /*        if (Input.GetMouseButtonDown(0))
                {
                    // ��ũ�� ��ǥ(2D) -> ���� ��ǥ(3D) ��ȯ
                    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                    Vector3 dir = mousePos - Camera.main.transform.position;
                    dir = dir.normalized; // ����ȭ�� �ϴ� ������???

                    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.transform.position, dir, out hit))
                    {
                        Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
                    }
                }*/

        if (Input.GetMouseButtonDown(0))
        {
            // GameObject.FindGameObjectWithTag("Monster");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            // ����ĳ��Ʈ(���̾� ����ũ�� ���)
            // ������� : �ش� ������Ʈ�� ���ؼ��� raycasting�� �ְ� ���� ��� (��� ������Ʈ�� ����ĳ������ �� ��� ������ �ʹ� ������)

            // �����) 
            // 1. ��Ʈ �÷��� ��� (���̾ 0~31 ���� �� 32���� ���) int(4byte) -> 32bit
            // 6�� ��Ʈ ��� (���� �������� 1�� �������� 6�� �̵� -> 7��° 1�� ����)
            // 2. Layer �̸����� ã�� ���

            int mask = (1 << 6) | (1 << 7);
            string[] maskObject = { "Monster", "Wall" };
            // LayerMask mask = LayerMask.GetMask(maskObject);

            RaycastHit hit; // ���尡��� �浹ü�� ��� �ִ����� ���� ����?
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
            }
        }

    }
}
