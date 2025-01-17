using UnityEngine;

namespace LBH
{
    public class Obstacle : MonoBehaviour
    {
        private Rigidbody2D _rb;
        
        [SerializeField] private float _mass = 1f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.mass = _mass;
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}
