using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Camera _camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirVec = mousePos - (Vector2)transform.position; //���콺�� �ٶ󺸴� ������ ��Ÿ���� ����?
        transform.right = (Vector3)dirVec.normalized;
    }
}
