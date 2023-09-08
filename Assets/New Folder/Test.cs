using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gravity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private MoveFlg isMoveX;
    [SerializeField] private MoveFlg isMoveY;
    [SerializeField] private int MaxJumpCount;
    [SerializeField] private int jumpCount;
    private bool isJump;
    private float dx;
    private float dy;
    private float ddy;
    private const float a = -9.8f;
    // Start is called before the first frame update
    public enum MoveFlg
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 前進/ジャンプ
        /// </summary>
        Forward,
        /// <summary>
        /// 後進/滑空
        /// </summary>
        Backward,
    }
    void Start()
    {
        dx = 0;
        ddy = 0;
        dy = 0;
        isMoveX = MoveFlg.Stop;
        isMoveY = MoveFlg.Stop;
        isJump = false;
    }

    // UI またはボタン操作
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            var arrowX = Input.GetAxis("Horizontal");

            //X移動
            if (arrowX == 0)
            {
                dx = 0;
            }
            else if (arrowX > 0)
            {
                dx = moveSpeed;
            }
            else if (arrowX < 0)
            {
                dx = moveSpeed * -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !(rb.velocity.y < -0.5f))
        {
            Jump();
        }
        if(jumpCount > 0)
        {
            totalFallTime += Time.deltaTime;
            dy = jumpForce* jumpCount - (9.8f * totalFallTime);
        }
    }
    private float totalFallTime = 0f;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.jumpCount < MaxJumpCount)
        {
            //ddy = rb.velocity.y + a;
            jumpCount++;
        }
    }

    //object
    void FixedUpdate()
    {
        rb.velocity = new Vector2(
               dx,
               dy
           );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
            dy = 0;
            totalFallTime = 0;
        }
        if (other.gameObject.CompareTag("wall"))
        {
            if (dx > 0)
            {
                dx = moveSpeed * -1;
            }
            else if (dx < 0)
            {
                dx = moveSpeed;
            }
        }
    }
 }
