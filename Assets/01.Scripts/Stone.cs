using LBH;
using UnityEngine;

public class Stone : MonoBehaviour
{

    private bool _isUnion = true;   // ������ ����
    private bool _isThrow = false;  // ������ ����
    private bool _isReturn = false; // ���ƿ��� ����
    private bool _isKnockBack = false;
    private bool _isGround = false;
    private bool _checkGround = false;

    private Vector3 _throwVector;
    private Rigidbody2D _rb;
    //private LineRenderer _lr;


    [SerializeField] private Transform _bodyTransform;

    [SerializeField] private float _returnSpeed = 7f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _maxThrowForce = 30f;
    [SerializeField] private float _increaseForceAmount = 1f;
    private float _throwForce = 0f;

    [Space(10f)]
    private float _groundCheckDistance = 0.6f;
    [SerializeField] private LayerMask _groundMask = 1 << 6;

    [Space(10f)]
    public GameObject Arrow;
    private SpriteRenderer _arrowSpriteRenderer;
    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        //_lr = this.GetComponent<LineRenderer>();
    }

    void Start()
    {
        //_lr.enabled = false;
        //_lr.positionCount = 2; //��,�� ��
        Arrow.SetActive(false);
        _arrowSpriteRenderer = Arrow.GetComponentInChildren<SpriteRenderer>();

        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (!_isUnion && _isReturn && CompareWithBody())
        {
            LinkToBody();
        }



        if (_isUnion && !_isThrow && !_isReturn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _throwForce = 0f;
                CalculateThrowVector();
                SetArrow();
            }
            if (Input.GetMouseButton(0))
            {
                CalculateThrowForce();
                CalculateThrowVector();
            }
            if (Input.GetMouseButtonUp(0))
            {
                RemoveArrow();
                Throw();
            }
        }

        // ������ ���¿��� ���ƿ��� ���·� ��ȯ


        if (_isUnion)
        {
            return;
        }


        if (_isReturn && !_isKnockBack)
        {
            if (_isGround && Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }


    }

    private void FixedUpdate()
    {
        if (_isThrow && _isGround && _rb.linearVelocityX <= 0.5f)
        {
            _isReturn = true;
            _isThrow = false;
        }

        if (_isReturn && !_isKnockBack)
        {
            ReturnToBody();
        }

        if (!_isReturn && _isKnockBack && _isGround)
        {
            _isKnockBack = false;
            _isReturn = true;
        }

        if (_checkGround)
        {
            _isGround = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundMask);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7: // �����ϰ� �ε����� ��
                if (_isThrow)
                {
                    Destroy(collision.gameObject);
                }

                if (_isReturn)
                {
                    Destroy(collision.gameObject);
                }
                break;
            case 8: // �����ϰ� �ε����� ��
                if (_isThrow)
                {
                    Destroy(collision.gameObject);
                }

                if (_isReturn)
                {
                    KnockBack(new Vector2(1, 1) * 3);
                    Destroy(collision.gameObject);
                }
                break;
            case 9: // ö�ϰ� �ε����� ��
                if (_isThrow)
                {

                }

                if (_isReturn)
                {
                    KnockBack(new Vector2(1, 1) * 3);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            GameManager.Instance.ClearStage();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.down * 0.6f);
    }

    /// <summary>
    ///  ���� �������� �������� ���
    /// </summary>
    private void ReturnToBody()
    {
        _rb.linearVelocityX = -1f * _returnSpeed;
    }

    /// <summary>
    /// y�� ���� �����ϴ� ���
    /// </summary>
    private void Jump()
    {
        _isGround = false;
        _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// ���� ���� x�� ��ǥ�� ���ϴ� �Լ�
    /// </summary>
    /// <returns> ���� ������ �ڿ� ������ true �ƴϸ� false�� ��ȯ�Ѵ�.</returns>
    private bool CompareWithBody()
    {
        return transform.position.x <= _bodyTransform.position.x + 0.5f ? true : false;
    }

    /// <summary>
    /// ���� ��ü�ϴ� �Լ�
    /// </summary>
    private void LinkToBody()
    {
        _isReturn = false;
        _isUnion = true;

        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;

        transform.SetParent(_bodyTransform);
        transform.localPosition = new Vector2(0, 1.6f);
    }

    /// <summary>
    /// ���� ������ ���
    /// </summary>
    private void Throw()
    {
        _isThrow = true;
        _checkGround = true;
        _isUnion = false;

        transform.SetParent(null);

        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(_throwVector * _throwForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// ��ֹ��� �浹���� �� ���� ���ư��� ���
    /// </summary>
    /// <param name="force">���� ���ư��� ��</param>
    private void KnockBack(Vector2 force)
    {
        _checkGround = false;
        _isReturn = false;
        _isGround = false;
        _isKnockBack = true;
        _rb.linearVelocityX = 0;
        _rb.AddForce(force, ForceMode2D.Impulse);

        Invoke("StartCheckGround", 1f);
    }

    /// <summary>
    /// �� üũ�� �����ϴ� �Լ�
    /// </summary>
    private void StartCheckGround()
    {
        _checkGround = true;
    }

    //���콺 Ŭ��, �巡��?
    private void CalculateThrowVector()
    {
        float angle;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 distance = mousePos - transform.position;
        Vector3 throwVector = distance.normalized * 10;

        angle = GetAngle(transform.position, throwVector);
        Debug.Log(angle);
        Arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.position.z + angle));
       
        if (angle >= 45f)
        {
            Arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.position.z + 45f));
            return;
        }
        else if(angle <= -45f)
        {
            Arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.position.z - 45f));
            return;
        }
        else
        {
            _throwVector = throwVector.normalized;

        }
    }

    private void CalculateThrowForce()
    {
        _throwForce += _increaseForceAmount * Time.deltaTime;
        
        if(_throwForce >= _maxThrowForce || _throwForce <= 0)
        {
            _increaseForceAmount *= -1f;
        }

        float x = 1 - _throwForce / _maxThrowForce;
        _arrowSpriteRenderer.color = new Color(1, x, x);
    }

    private void SetArrow()
    {
        Arrow.SetActive(true);
    }

    private void RemoveArrow()
    {
        Arrow.SetActive(false);
    }

    private float GetAngle(Vector3 from,Vector3 to)
    {
        Vector3 v = to - from;  
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
