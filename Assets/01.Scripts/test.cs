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

    public Transform bodyTransform; // 몸의 transform
    private bool isThrow =false; // 현재 던져진 상태인지 확인하기 위한 변수

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
        // 만약 던지는 중이 아니면
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
     
        // 던져진 상태이고 현재 위치가 몸의 위치보다 왼쪽에 있으면 
        if(isThrow && transform.position.x <= bodyTransform.position.x + 0.8f)
        {
            Follow();
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

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public IEnumerator Throw() // 돌을 던지는 함수
    {
        transform.SetParent(null); // 부모 오브젝트에서 벗어난다.
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(throwVector,ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        isThrow = true; // 던지는 상태로 변경
    }
   

    public void Follow() // 플레이어와 떨어져 있다가 다시 플레이어 따라갈 때
    {
       
        isThrow = false;
        _rb.bodyType = RigidbodyType2D.Kinematic; 
        transform.SetParent(bodyTransform);
        transform.localPosition = new Vector2(0f, 1.6f);
    }
}
