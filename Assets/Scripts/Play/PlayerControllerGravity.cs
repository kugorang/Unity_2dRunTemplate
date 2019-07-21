using Common;
using UnityEngine;

namespace Play
{
    public class PlayerControllerGravity : MonoBehaviour
    {
        [SerializeField] 
        private float _jumpForce = 150f;
	
        private Rigidbody2D _rb;

        private bool _doubleJumpAllowed, _onTheGround;

        [HideInInspector] public StageManager StageManager;
        private AudioManager _audioManager;
	
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _audioManager = AudioManager.Instance;
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
            
            _audioManager.Play("Jump");
        }
    
        public void OnJumpBtnClick()
        {
            if (StageManager.bPause)
            {
                return;
            }   
        
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