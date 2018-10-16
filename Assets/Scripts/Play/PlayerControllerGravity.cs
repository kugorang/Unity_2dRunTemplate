using UnityEngine;

namespace Play
{
    public class PlayerControllerGravity : MonoBehaviour
    {
        /*private float _dirX;*/
	
        [SerializeField] 
        private float _jumpForce = 150f/*, _moveSpeed = 5f*/;
	
        private Rigidbody2D _rb;

        private bool _doubleJumpAllowed, _onTheGround;

        public StageManager StageManager;
	
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
	
        private void Update()
        {
            _onTheGround = Mathf.Abs(_rb.velocity.y) <= 0;
        
            if (_onTheGround)
            {
                _doubleJumpAllowed = true;
            }
        }
	
        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
	
        private void Jump()
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }
    
        public void OnJumpBtnClick()
        {
            if (StageManager.IsPause)
                return;
            
            _onTheGround = Mathf.Abs(_rb.velocity.y) <= 0;
        
            if (_onTheGround)
            {
                Jump();
            }
            else if (_doubleJumpAllowed)
            {
                _doubleJumpAllowed = false;
                Jump();
            }
        }
    }
}