using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlay : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーのプレハブを設定")]
    private GameObject playerPrefab;


    [SerializeField]
    [Tooltip("カメラを設定")]
    private GameObject cam;
    // Update is called once per frame
    void Update()
    {
        // 設定したplayerPrefabと同じ名前(今回は"PlayerSphere")のGameObjectを探して取得
        GameObject playerObj = GameObject.Find(playerPrefab.name);

        // playerObjが存在していない場合
        if (playerObj == null)
        {

            //★ここでポジションを設定しリスポのいちを設定可能
            // playerPrefabから新しくGameObjectを作成
            GameObject newPlayerObj = Instantiate(playerPrefab);

            newPlayerObj.name = playerPrefab.name;
            // ※ここで名前を再設定しない場合、自動で決まる名前は、"PlayerSphere(Clone)"となるため
            //   13行目で探している"PlayerSphere"が永遠に見つからないことになり、playerが無限に生産される
            //   どういうことかは、22行目をコメントアウトしてゲームを実行すればわかります。
          
        }
        else
        {
            float depth = 5.0f;
            var rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, depth));
            var leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, depth));
            if (rightTop.x >= (-5.06 + 17.20416))
            {

                Debug.Log("create");
            }
           // Debug.Log(rightTop + " " + playerObj.transform.position.x);
            //Debug.Log(leftBottom);
        }
    }
    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            
            // 当たった相手を1秒後に削除
            Destroy(collision.gameObject, 1.0f);
        }
    }

}
