using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task_player : MonoBehaviour
{
    public float speed = 1.0f;

    public float jumpPower = 5.0f;
    
    Vector2 velocity;

    new Rigidbody2D rigidbody;

    Animator animator;

    private bool isGround;

    private bool jumpRequested;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float _hozInput = Input.GetAxisRaw("Horizontal"); // -1,0,1
        velocity = new Vector2(_hozInput,0);

        if(_hozInput > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if(_hozInput < 0){
            transform.rotation = Quaternion.Euler(0,180,0);
        }

        if(velocity.x != 0){
            animator.SetBool("isWalk",true);
        }
        else{
            animator.SetBool("isWalk",false);
        }
        
        if (isGround && Input.GetKeyDown(KeyCode.Space) && !jumpRequested)
        {
             // 점프 요청 플래그 설정
            StartCoroutine(PerformJumpAfterDelay(0.1f)); // 0.2초 지연 후 PerformJumpAfterDelay 메서드 호출
        }

        // 애니메이션 상태 전환
        if (rigidbody.velocity.y > 0)
        {
            animator.SetTrigger("Jump_fly");
        }
        else if (rigidbody.velocity.y < 0 && !isGround)
        {
            animator.SetTrigger("Jump_down");
        }

        if(rigidbody.velocity.y < 0){
            animator.SetBool("isWalk",false);
            animator.SetBool("isIdle",false);
            animator.SetTrigger("Jump_down");
        }

    }

    void FixedUpdate(){
        rigidbody.velocity = new Vector2(velocity.x,rigidbody.velocity.y);

    }


    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name.Equals("object"))
        {
            isGround = true;
            animator.SetBool("isIdle",true);
            jumpRequested = false;
            animator.ResetTrigger("Jump_up");
            animator.ResetTrigger("Jump_fly");
            animator.ResetTrigger("Jump_down");

        }
    }

    private IEnumerator PerformJumpAfterDelay(float delay)
    {
        animator.SetTrigger("Jump_up");
        
        // 애니메이션 재생 후 지정된 시간(1초)만큼 대기
        
        
        if (isGround)
        {
            jumpRequested = true;
            isGround = false;
            animator.SetBool("isIdle", false);
            yield return new WaitForSeconds(0.5f);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); // 현재 Y 속도 초기화
            rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);

            // 점프를 시작하면 동시에 Jump_fly 애니메이션 재생
            animator.SetTrigger("Jump_fly");
        }
        jumpRequested = false; // 점프 요청 플래그 리셋
    }
}
