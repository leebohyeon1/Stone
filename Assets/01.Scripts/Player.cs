using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent <Rigidbody2D>();
        // GetComponent 주어진 게임 오브젝트에서 특정 컴포넌트를 찾아오는 함수임.
        // + 게임 오브젝트에 붙어있는 스크립트나 컴포넌트를 가져올 수 있게 하는 함수
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //3. 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name);
        }
        //충돌 시 실행될 코드 작성
        //장애물(유리, 강철, 나무)과 닿으면 감속. addforce 사용?
        // + 바닥이나 벽의 오브젝트는 제외해야 됨. 
        // 던짐의 속도에서 --? -+? 
    }
}

