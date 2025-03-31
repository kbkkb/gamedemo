using UnityEngine;
using GameData;

public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
    [Header("地面检测")]
    public Transform groundCheck; // ✅ 地面检测点
    public float groundCheckRadius = 0.2f; // ✅ 触碰检测半径
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private bool canDash = true;
    private bool canDoubleJump = true;
    private float dashCooldownTimer;
    private float dashTimer;
    private Vector2 dashDirection;
    private bool facingRight = true;

    public bool IsGrounded => isGrounded;
    public bool CanDash => canDash;
    public float DashCooldownTimer => dashCooldownTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("未找到 Rigidbody2D 组件！");
        }
    }

    private void Update()
    {
        // ✅ 使用 groundCheck 进行地面检测
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ✅ 重置二段跳（回到地面时）
        if (isGrounded && !wasGrounded)
        {
            canDoubleJump = true;
        }

        // 更新冲刺冷却
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }

        // 冲刺状态更新
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                EndDash();
            }
        }

        // 获取输入
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (!isDashing)
        {
            Move(moveInput);
        }

        // ✅ 修复跳跃逻辑
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                DoubleJump();
            }
        }

        // 冲刺输入
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartDash();
        }
    }

    private void Move(float moveInput)
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        AudioManager.Instance.PlayJumpSound();
    }

    private void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canDoubleJump = false; // ✅ 只允许一次二段跳
        AudioManager.Instance.PlayJumpSound();
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashCooldownTimer = dashCooldown;
        dashTimer = dashDuration;
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = facingRight ? Vector2.right : Vector2.left;
        }
        rb.velocity = dashDirection * dashForce;
        AudioManager.Instance.PlayDashSound();
    }

    private void EndDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
