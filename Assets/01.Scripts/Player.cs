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
        // GetComponent �־��� ���� ������Ʈ���� Ư�� ������Ʈ�� ã�ƿ��� �Լ���.
        // + ���� ������Ʈ�� �پ��ִ� ��ũ��Ʈ�� ������Ʈ�� ������ �� �ְ� �ϴ� �Լ�
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //3. ��ġ �̵�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name);
        }
        //�浹 �� ����� �ڵ� �ۼ�
        //��ֹ�(����, ��ö, ����)�� ������ ����. addforce ���?
        // + �ٴ��̳� ���� ������Ʈ�� �����ؾ� ��. 
        // ������ �ӵ����� --? -+? 
    }
}

