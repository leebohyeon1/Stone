using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;



public class test : MonoBehaviour
{
    private Vector3 throwVector;
    private Rigidbody2D _rb;
    private LineRenderer _lr;

    public Transform bodyTransform; // ���� transform
    private bool isThrow =false; // ���� ������ �������� Ȯ���ϱ� ���� ����

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _lr = this.GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _lr.enabled = false;
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        // ���� ������ ���� �ƴϸ�
        if(!isThrow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CalculateThrowVector();
                SetArrow();
            }

            if (Input.GetMouseButton(0))
            {
                CalculateThrowVector();
                SetArrow();
            }

            if (Input.GetMouseButtonUp(0))
            {
                RemoveArrow();
                StartCoroutine("Throw");
            }
        }
     
        // ������ �����̰� ���� ��ġ�� ���� ��ġ���� ���ʿ� ������ 
        if(isThrow && transform.position.x <= bodyTransform.position.x + 0.8f)
        {
            Follow();
        }

    }

    //���콺 Ŭ��, �巡��?
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePos - transform.position;
        throwVector = distance.normalized * 10;  //����, ��
    }

    void SetArrow()
    {
        _lr.positionCount = 2; //��,�� ��
        _lr.SetPosition(0, transform.position); 
        _lr.SetPosition(1, throwVector.normalized *10); //����ȭ -> ���� ���ϱ�
        _lr.enabled = true; //enabled -> ������Ʈ �¿��� �� �� ����
    }

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public IEnumerator Throw() // ���� ������ �Լ�
    {
        transform.SetParent(null); // �θ� ������Ʈ���� �����.
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(throwVector,ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        isThrow = true; // ������ ���·� ����
    }
   

    public void Follow() // �÷��̾�� ������ �ִٰ� �ٽ� �÷��̾� ���� ��
    {
       
        isThrow = false;
        _rb.bodyType = RigidbodyType2D.Kinematic; 
        transform.SetParent(bodyTransform);
        transform.localPosition = new Vector2(0f, 1.6f);
    }
}
