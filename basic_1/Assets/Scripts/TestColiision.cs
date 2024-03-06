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
        // Debug.Log(Input.mousePosition); // screen 좌표(x, y) 모니터의 픽셀 단위
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // screen 보이는 비율?

        /*        if (Input.GetMouseButtonDown(0))
                {
                    // 스크린 좌표(2D) -> 월드 좌표(3D) 변환
                    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                    Vector3 dir = mousePos - Camera.main.transform.position;
                    dir = dir.normalized; // 정규화를 하는 이유는???

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

            // 레이캐스트(레이어 마스크를 사용)
            // 사용이유 : 해당 오브젝트에 한해서만 raycasting을 주고 싶은 경우 (모든 오브젝트가 레이캐스팅이 될 경우 연산이 너무 많아짐)

            // 사용방법) 
            // 1. 비트 플래그 사용 (레이어를 0~31 까지 총 32개를 사용) int(4byte) -> 32bit
            // 6번 비트 사용 (시작 지점부터 1을 왼쪽으로 6번 이동 -> 7번째 1이 켜짐)
            // 2. Layer 이름으로 찾는 방식

            int mask = (1 << 6) | (1 << 7);
            string[] maskObject = { "Monster", "Wall" };
            // LayerMask mask = LayerMask.GetMask(maskObject);

            RaycastHit hit; // 가장가까운 충돌체가 어디에 있는지에 대한 정보?
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
            }
        }

    }
}
