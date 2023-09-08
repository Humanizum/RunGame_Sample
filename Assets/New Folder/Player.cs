using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player: MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpForce;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private TextMeshProUGUI metol;
    [SerializeField] private int MaxJumpCount = 2;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int score = 0;
    public float kickForce = 100f; // キックの力

    private float km = 0;
    private bool isKicking = false;
    [SerializeField] private GameObject obj;

    private float speed;
    void Start()
    {
        km = transform.position.x;

       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !(rb.velocity.y < -0.5f))
        {
            //Debug.Log(Vector2.up * jumpForce);
            Jump();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            speed = Input.GetAxis("Horizontal") * moveSpeed;
        }
        else
            speed = 0;
            rb.velocity = new Vector2(speed, rb.velocity.y);

        float mm = transform.position.x - km;
        if (mm < 1000)
            metol.text = mm.ToString("0.000") + "m";
        else
            metol.text = (mm / 1000).ToString("0.000") + "km";

        // もし-50mまで落下したら
        if (this.transform.position.y < -10)
        {

            // 当たった相手を1秒後に削除
            Destroy(this.transform.gameObject, 1.0f);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.jumpCount < MaxJumpCount)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isKicking = false;
        }
    }

    private bool hit = false;

    private bool deadhit = false;
    private bool alivehit = false;
    private bool wallHit = false;

    // 床に着地したら、jumpCountを0にする
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }

        if (!wallHit && jumpCount > 0 && other.gameObject.CompareTag("wall"))
        {
            wallHit = true;
        }

        if (!hit && other.gameObject.CompareTag("coin")) hit = true;

        if (!deadhit && other.gameObject.CompareTag("dead")) deadhit = true;

        if (!alivehit && other.gameObject.CompareTag("life")) alivehit = true;
        

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (hit && other.gameObject.CompareTag("coin"))
        {
            Debug.Log("now = " + score);
            score++;
            tmp.text = "Coin: " + score;
            hit = false;
           // Destroy(other.gameObject);
        }

        if (deadhit && other.gameObject.CompareTag("dead"))
        {
            if(heart >0 )
                Dead();
            deadhit = false;
        }

        if (alivehit && other.gameObject.CompareTag("life"))
        {
           Alive();
            alivehit = false;
        }
        if (wallHit && other.gameObject.CompareTag("wall"))
        {
            Vector2 upwardRightDirection = new Vector2(5f, 5f).normalized;

            //// ベクトルに力の大きさを乗算
            Vector2 forceVector = upwardRightDirection * jumpForce;

            Debug.Log("wall");
            //// 壁キックを実行
            rb.AddForce(forceVector, ForceMode2D.Impulse);
            speed = Input.GetAxis("Horizontal") * -moveSpeed;

            //rb.velocity = new Vector2(, rb.velocity.y);
            wallHit = false;
        }

    }

    public int heart = 3;
    private void Dead()
    {
        string tag = "heart" + (heart).ToString();
        GameObject cube = obj.transform.GetChild(heart-1).gameObject;
        //GameObject cube = GameObject.Find(tag);

        Debug.Log(cube);
        Destroy(cube);
        heart--;
    }
    public GameObject prefab;
    private void Alive()
    {
        GameObject childObject = Instantiate(prefab, obj.transform, false);
        childObject.transform.parent = obj.transform;
        heart++;
    }
}