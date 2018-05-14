using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float speed = 20;
    public Text scoreText;
    public GameObject clearText;
    public CameraController cameraController;
    public GetMoney getMoneyPrefab;
    public bool clear = false;

    private Rigidbody rb;
    private int score = 0;
    private AudioSource kane1;
    private AudioSource kane2;
    private AudioSource goal;
    private AudioSource bgm;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        var audioSources = GetComponents<AudioSource>();
        kane1 = audioSources[0];
        kane2 = audioSources[1];
        goal = audioSources[2];
        bgm = audioSources[3];
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerMovement();
        UpdateScoreText();
	}

    private void OnTriggerEnter(Collider other)
    {
        // 金にぶつかったとき
        if (other.gameObject.CompareTag("Pickup"))
        {
            // スコアを増やす
            score += 10000;
            // 金を消す
            Destroy(other.gameObject);
            // 金ゲットエフェクトを出す
            var getMoney = Instantiate(getMoneyPrefab, transform.localPosition, Quaternion.identity);
            getMoney.player = gameObject;
            // ボイスを再生
            if (score > 100000)
            {
                // 所持金10万以上あるとテンションが上がる
                kane2.PlayOneShot(kane2.clip);
            } else
            {
                kane1.PlayOneShot(kane1.clip);
            }
        }

        // ゴール看板にぶつかったとき
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            cameraController.followPlayer = false;
            clearText.SetActive(true);
            bgm.Stop();
            goal.PlayOneShot(goal.clip);
        }
    }

    private void UpdatePlayerMovement()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
