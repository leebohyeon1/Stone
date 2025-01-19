using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    public GameObject A;
    Transform AT; 
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AT = A.transform;
    }

    // Update is called once per frame
    void LateUpdate() //Update �Լ��� �״�� ���� �������� ��߳�. (�����Ÿ�)
    {
        transform.position = new Vector3(AT.position.x, transform.position.y, transform.position.z);
        // transform.position.z -> �ν�����â�� ������ �� �����ص� �� ���ϴ� �� ����.
        // + 0�� �Ÿ� �׳� y��ǥ �ڸ��� 0�ص� �Ǵ� ��.
    }
}
