using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //创建PC例子
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    //修改杀敌人的效果
    public float bounceForce = 8f;

    private Vector3 moveDirection;
    public CharacterController charController;

    private Camera theCam;

    // 创建丝滑移动
    public GameObject playerModel;
    public float rotateSpeed;

    //动画
    public Animator anim;

    ////////knock the players backwards/////////////////
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockBackCounter;
    public Vector2 knockbackPower;
        
    //人物撞到    
    public GameObject[]playerPieces;

    public bool stopMove;

    //设置PC例子的AWAKE函数
    private void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnocking && !stopMove)
        {
            //记录人物的位置，以防定在某个位置
            float yStore = moveDirection.y;
            //人物运动
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
            //根据camera旋转
            moveDirection = (transform.forward*Input.GetAxisRaw("Vertical"))+(transform.right*Input.GetAxisRaw("Horizontal"));
            //初始化运动方向
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            //只有在地板上可以jump
            if(charController.isGrounded){

                moveDirection.y = -1f;

                if(Input.GetButtonDown("Jump")){
                moveDirection.y = jumpForce;
                }
            }

            

            //人物重力
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale ;

            //transform.position = transform.position + (moveDirection* Time.deltaTime * moveSpeed);
            charController.Move(moveDirection * Time.deltaTime);

            //人物随camera旋转
            if(Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical")!=0)
            {
                transform.rotation = Quaternion.Euler(0f,theCam.transform.rotation.eulerAngles.y,0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
                //playerModel.transform.rotation = newRotation;
                //丝滑移动
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,newRotation,rotateSpeed*Time.deltaTime);
            }
        }
        //停一下再继续[死亡机制中]
        if(isKnocking){
            knockBackCounter -= Time.deltaTime;

            //人物会弹回
            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            //只有在地板上可以jump
            if(charController.isGrounded){

                moveDirection.y = -1f;

                if(Input.GetButtonDown("Jump")){
                moveDirection.y = jumpForce;
                }
            }

            //人物重力
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale ;

            charController.Move(moveDirection*Time.deltaTime);

            if(knockBackCounter <=  0){
                isKnocking = false;
            }
        }

        if(stopMove){
            moveDirection = Vector3.zero;
            //人物重力
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection);
        }

        //动画的speed
        anim.SetFloat("Speed",Mathf.Abs(moveDirection.x)+Mathf.Abs(moveDirection.z));
        //创建jump的动画
        anim.SetBool("Ground",charController.isGrounded);
        
        
    }
    ////////knock the players backwards/////////////////
    public void knockback(){
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Knocked Back");
        //回弹
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection*Time.deltaTime);
    }

    //修改杀敌人的效果
    public void Bounce(){
        moveDirection.y = bounceForce;
        charController.Move(moveDirection*Time.deltaTime);
    }

}
