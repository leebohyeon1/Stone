using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float moveSpeed;
    private Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent <Rigidbody2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //1. 힘을 준다
        rigid.AddForce (inputVec*moveSpeed);

        //2. 속도 제어
        //rigid.angularVelocity = inputVec;

        //3. 위치 이동
        //rigid
    }
}
