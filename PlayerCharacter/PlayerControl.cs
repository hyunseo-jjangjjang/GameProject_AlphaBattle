using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerControl : MonoBehaviour {
    private Rigidbody2D PlayerRigidbody;
    private bool isJump = true;
    private int JumpCounter = 40;
    public float Speed;

    private BoxCollider2D GroundColider;
    private GameObject Bullet;

    //여기 수정
    public GameObject HitAniObject;
    public Animator HItAni;
    public Animator CameraAni;

    public AudioClip aaaa;
    
    void Start()
    {
        GroundColider = GetComponent<BoxCollider2D>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        CameraAni = Camera.main.GetComponentInParent<Animator>();
    }
    void Update()
    {
        transform.Translate(InputData() * Speed * Time.deltaTime);
        InputJump();

        //if(Input.GetMouseButtonDown(0))
        //{
        //    GameObject _bullet = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity,0);
        //    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    float digree = Mathf.Atan2(target.y - transform.position.y,
        //        target.x - transform.position.x) * 180f / Mathf.PI;
        //    _bullet.transform.rotation = Quaternion.Euler(0, 0, digree - 90.0f);

        //    _bullet.tag = "fish";

        //}
        //여기 수정
        HitTest();
    }
    //여기 수정
    void HitTest()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            HItAni.Play("Bullet_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            HItAni.Play("Fist_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            HItAni.Play("Public_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            HItAni.Play("Lightning_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
    }



    void InputJump()
    {
        if (Input.GetKey(KeyCode.S) && isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GroundColider.enabled = false;
            }
        }
        else if (isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = false;
                GetComponent<Animator>().SetBool("Jump", true);
                PlayerRigidbody.AddForce(Vector2.up * 12.0f, ForceMode2D.Impulse);
                Speed = 3;
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (JumpCounter > 0)
            {
                JumpCounter--;
            }
            else if (JumpCounter == 0)
            {
                PlayerRigidbody.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                JumpCounter--;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpCounter = 40;
        }
    }
    Vector2 InputData()
    {
        Vector2 MoveVec = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            MoveVec = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveVec = Vector2.right;
        }
        if (MoveVec == Vector2.zero)
        {
            GetComponent<Animator>().SetBool("Run", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", true);
        }
        return MoveVec;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("땅부딪혔다고 십팔");

            if (GroundColider != null)
                GroundColider.enabled = true;
            GroundColider = collision.collider.gameObject.GetComponent<BoxCollider2D>();
            GetComponent<Animator>().SetBool("Jump", false);
            Speed = 8;
            isJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Bullet") && enabled == true)
        {
            SoundManger.instance.Play(AudioEnum.HITAA);
            GetComponent<AudioSource>().PlayOneShot(aaaa);
            Debug.Log("시펄 멋하는짓이노 ");
            coll.gameObject.GetComponent<PhotonView>().RPC("BulletDestory", PhotonTargets.All);
            InGameController.InGame.BulletHit();
        }
        else if (coll.CompareTag("Bullet2") && enabled == true)
        {
            SoundManger.instance.Play(AudioEnum.HITAA);
            GetComponent<AudioSource>().PlayOneShot(aaaa);
            Debug.Log("시펄 멋하는짓이노 ");
            InGameController.InGame.BulletHit();
        }


        //여기 수정
        if (coll.CompareTag("Bullet"))
        {
            HitAniObject.transform.eulerAngles = -coll.transform.eulerAngles;
            HItAni.Play("Bullet_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
        //else if (coll.CompareTag("주먹"))
        //{
        //    HitAniObject.transform.eulerAngles = -coll.transform.eulerAngles;
        //    HItAni.Play("Fist_Hit_ani");
        //    CameraAni.Play("ShakeCamera3");
        //}
        else if (coll.CompareTag("SWORD"))
        {
            HItAni.Play("Public_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }
        else if (coll.CompareTag("jjang"))
        {
            GetComponent<Animator>().Play("Lightning_Hit_ani");
            CameraAni.Play("ShakeCamera3");
        }

    }
}
