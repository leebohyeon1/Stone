using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;


namespace LBH
{
    public class stoneTest : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private bool _isUnion = true;   // 합쳐진 상태
        private bool _isThrow = false;  // 던져진 상태
        private bool _isReturn = false; // 돌아오는 상태
        private bool _isKnockBack = false;
        private bool _isGround = false;
        private bool _checkGround = false;  


        [SerializeField] private Transform _bodyTransform;

        [SerializeField] private float _returnSpeed = 7f;
        [SerializeField] private float _jumpForce = 10f;


        [Space(10f)]
        private float _groundCheckDistance = 0.6f;
        [SerializeField] private LayerMask _groundMask = 1 << 6;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            if (!_isUnion && _isReturn && CompareWithBody())
            {
                LinkToBody();
            }

          

            if (_isUnion && !_isThrow && !_isReturn)
            {
                if(Input.GetMouseButtonUp(0))
                {
                    Throw();
                }
            }

            // 던져진 상태에서 돌아오는 상태로 전환
           

            if(_isUnion)
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
            if (_isThrow && _isGround && _rb.linearVelocityX <= 0.5f )
            {
                _isReturn = true;
                _isThrow = false;
            }

            if (_isReturn && !_isKnockBack)
            {
                ReturnToBody();
            }

            if (!_isReturn && _isKnockBack && _isGround )
            {
                _isKnockBack = false;
                _isReturn = true;
            }

            if(_checkGround)
            {
                _isGround = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundMask);
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.layer)
            {
                case 7: // 유리하고 부딪혔을 때
                    if(_isThrow)
                    {
                        Destroy(collision.gameObject);
                    }

                    if(_isReturn)
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 8: // 나무하고 부딪혔을 때
                    if (_isThrow)
                    {
                        Destroy(collision.gameObject);
                    }

                    if (_isReturn)
                    {
                        KnockBack(new Vector2(1, 1) * 3);
                        Destroy(collision.gameObject);
                    }
                    break ;
                case 9: // 철하고 부딪혔을 때
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
            if(collision.CompareTag("Goal"))
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
        ///  돌이 왼쪽으로 굴러가는 기능
        /// </summary>
        private void ReturnToBody()
        {
            _rb.linearVelocityX = -1f * _returnSpeed;
        }

        /// <summary>
        /// y축 위로 점프하는 기능
        /// </summary>
        private void Jump()
        {
            _isGround = false;
            _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
        }

        /// <summary>
        /// 몸과 돌의 x축 좌표를 비교하는 함수
        /// </summary>
        /// <returns> 돌이 몸보다 뒤에 있으면 true 아니면 false를 반환한다.</returns>
        private bool CompareWithBody()
        {
            return transform.position.x <= _bodyTransform.position.x + 0.5f ? true : false;
        }

        /// <summary>
        /// 몸과 합체하는 함수
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
        /// 돌을 던지는 기능
        /// </summary>
        private void Throw()
        {
            _isThrow = true;
            _checkGround = true;
            _isUnion = false;

            transform.SetParent(null);

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        }

        /// <summary>
        /// 장애물과 충돌했을 때 돌이 날아가는 기능
        /// </summary>
        /// <param name="force">돌이 날아가는 힘</param>
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
        /// 땅 체크를 시작하는 함수
        /// </summary>
        private void StartCheckGround()
        {
            _checkGround = true ;
        }

    }
}