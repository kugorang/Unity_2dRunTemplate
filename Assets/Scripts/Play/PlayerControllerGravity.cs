using Common;
using UnityEngine;

namespace Play
{
    public class PlayerControllerGravity : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce = 4f;
	
        private Rigidbody2D rb;

        private bool doubleJumpAllowed, onTheGround;

        public bool DoubleJumpAllowed
        {
            get
            {
                return doubleJumpAllowed;
            }
            set
            {
                doubleJumpAllowed = value;
                animator.SetBool("bDoubleJumpAllowed", value);
            }
        }

        private bool OnTheGround
        {
            get
            {
                return onTheGround;
            }
            set
            {
                onTheGround = value;
                animator.SetBool("bOnTheGround", value);
            }
        }

        private AudioManager audioManager;

        [SerializeField]
        private Animator animator = null;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            audioManager = AudioManager.Instance;
        }
	
        private void Update()
        {
            OnTheGround = Mathf.Abs(rb.velocity.y) <= 0;
        
            if (OnTheGround)
            {
                DoubleJumpAllowed = true;
                animator.SetBool("bJump", false);
            }
        }
	
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
	
        private void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;
            
            audioManager.Play("Jump");
            animator.SetBool("bJump", true);
        }
    
        public void OnJumpBtnClick()
        {
            if (OnTheGround)
            {
                Jump();
            }
            else if (DoubleJumpAllowed)
            {
                DoubleJumpAllowed = false;
                Jump();
            }
        }
    }
}