using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;


namespace LBH
{
    public class stoneTest : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private bool _isUnion = true;   // ������ ����
        private bool _isThrow = false;  // ������ ����
        private bool _isReturn = false; // ���ƿ��� ����
        private bool _isGround = false;

        [SerializeField] private Transform _bodyTransform;

        [SerializeField] private float _returnSpeed = 7f;
        [SerializeField] private float _jumpForce = 10f;


        [Space(20f)]
        [SerializeField] private LayerMask _groundMask = 1 << 6;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Debug.Log(_groundMask.value);
        }

        private void Update()
        {
            if (!_isUnion && _isReturn && CompareWithBody())
            {
                LinkToBody();
            }

            _isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundMask);

            if (_isUnion &&!_isThrow && !_isReturn)
            {
                if(Input.GetMouseButtonUp(0))
                {
                    Throw();
                }
            }

            if(_isThrow && _isGround)
            {
                _isReturn = true;
                _isThrow = false;   
            }


            if(_isUnion)
            {
                return;
            }

            if (_isReturn)
            {
                ReturnToBody();

                if (_isGround && Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
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
            _rb.AddForceY(_jumpForce,ForceMode2D.Impulse);
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
            
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.linearVelocity = Vector2.zero;

            transform.SetParent(_bodyTransform);
            transform.localPosition = new Vector2(0, 1.6f);
        }

        /// <summary>
        /// ���� ������ ���
        /// </summary>
        private void Throw()
        {
            _isThrow = true;
            _isUnion = false;

            transform.SetParent(null);

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        }
    }
}