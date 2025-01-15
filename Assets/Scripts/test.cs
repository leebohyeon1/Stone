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

    //마우스 클릭, 드래그?
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePos - transform.position;
        throwVector = distance.normalized * 10;  //방향, 힘
    }
    void SetArrow()
    {
        _lr.positionCount = 2; //점,두 개
        _lr.SetPosition(0, transform.position); 
        _lr.SetPosition(1, throwVector.normalized *10); //정규화 -> 방향 구하기
        _lr.enabled = true; //enabled -> 컨포넌트 온오프 할 수 있음
    }

    //private void OnMouseUp() //마우스가 오브젝트 위에 있을 때만 작동함.
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
