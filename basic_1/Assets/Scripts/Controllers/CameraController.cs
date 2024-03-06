using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Defines.CameraMode mode = Defines.CameraMode.QuaterView;

    [SerializeField]
    Vector3 delta = new Vector3(0.0f, 6.0f, -5.0f); // �÷��̾�� ���� ��ŭ ������ �ִ���

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // �÷��̾ �̵��Ҷ����� �����Ÿ��� ���� �߻�
        // � object�� update�� ���� ����ɰŶ�� ������ ���� ������ 
        // ī�޶� ��ġ ���� -> ĳ���� ��ǥ �̵� ������ �ϰԵǸ� ī�޶�� �̵��� �ߴµ� ĳ���ʹ� �����ְų� �ϴ� ������ �߻��Ѵ�
        // ù��° �ذ���)
        // �ϳ��� �������� update ������ �ִ� ī�޶� ��ġ ����, ĳ���� ��ǥ �̵� ����� �ϳ��� update�� ���Ƴְ� 
        // ĳ���� ��ǥ �̵� -> ī�޶� ��ġ ���� ������ ������ �����ؾ� �ȴ�.
        // �ι�° �ذ���)
        // ī�޶� ��ġ ����Ǵ� ����� LateUpdate()�� �����Ѵ� (LateUpdate�� Update ���Ŀ� ����Ǳ� ����)

        if(mode == Defines.CameraMode.QuaterView)
        {
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);

            /*RaycastHit hit;
            if(Physics.Raycast(player.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - player.transform.position).magnitude;
                transform.position = player.transform.position + delta.normalized * dist; 
            }
            else
            {
                transform.position = player.transform.position + delta;
                transform.LookAt(player.transform);
            }  */          
        }
        
    }

    public void SetQuaterView(Vector3 delta)
    {
        mode = Defines.CameraMode.QuaterView;
        this.delta = delta;
    }
}