using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class test : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _lr = this.GetComponent<LineRenderer>();
    }
    private void Start()
    {
        _lr.enabled = false;
    }

    private void Update()
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
            Throw();
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

    //private void OnMouseUp() //���콺�� ������Ʈ ���� ���� ���� �۵���.
    //{
    //    RemoveArrow();
    //    Throw();

    //}

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public void Throw()
    {
        _rb.AddForce(throwVector,ForceMode2D.Impulse);
    }
}
