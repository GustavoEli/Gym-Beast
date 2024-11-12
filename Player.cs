using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [HideInInspector] public bool isNextToClient = false;
    [HideInInspector] public bool isNextToMachine = false;
    [HideInInspector] public bool canGetClient = false;
    //[HideInInspector] public bool canUseMachine = false;
    [HideInInspector] public bool canWalk;
    [SerializeField] GameObject levelUp_UI;
    bool checkAnimation;
    Stack<Character> objStack = new Stack<Character>();
    Material texture;
    float acceleration;
    public static int money = 0;
    int stackLimit = 2;
    int punchForce = 0;
    int level = 0;

    void Start() {
        //this.meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponent<Animator>();
        this.speed = 2;
        canWalk = true;
    }

    void Update() {
        SetAcceleration();
        
        if (checkAnimation) {
            CheckPunchAnim();
        }

        //Debug.Log(canUseMachine);
    }

    public void SideMovement(int dir) {
        AnimationWalk();
        Rotate();
        position += new Vector3(this.speed * dir, 0, 0) * Time.deltaTime;
    }

    public void Front_BackMovement(int dir) {
        AnimationWalk();
        Rotate();
        position += new Vector3(0, 0, this.speed * dir) * Time.deltaTime;
    }

    private void AnimationWalk(){
        animator.SetBool("Walk", true);
    }

    public void AnimationIdle() {
        animator.SetBool("Walk", false);
    }
    
    public void AnimationPunch() {
        canWalk = false;
        animator.SetTrigger("Punch");
        checkAnimation = true;
    }

    void CheckPunchAnim() {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) { 
            canWalk = true;
            checkAnimation = false;
        }
    }

    public void Rotate() {
        float verticalRotation = GetVerticalInput();
        float horizontalRotation = GetHorizontalInput();
        Vector3 direction = new Vector3(horizontalRotation, 0, verticalRotation);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200 * Time.deltaTime);

        /*position.Normalize();
          angle = position.magnitude;
          transform.rotation = Quaternion.AngleAxis(angle * 5, Vector3.up);
          transform.LookAt(position, Vector3.up);*/
    }

    void AddObjectToStack(Client chara) {
        if (objStack.Count < 1 && objStack.Count < stackLimit) {
            objStack.Push(chara);
            chara.AddParentObject(this); //no caso a quem esse objeto esta empilhado
            chara.alreadyPicked = true;
            
        } else if(objStack.Count < stackLimit) {
            chara.AddParentObject(objStack.Peek());
            objStack.Push(chara);
            chara.alreadyPicked = true;
        }
    }

    public void RemoveObjFromStack() {
        objStack.Peek().gameObject.GetComponent<Client>().LeaveParent();
        objStack.Pop();
    }

    public float GetVerticalInput(){ return Input.GetAxis("Vertical"); }

    public float GetHorizontalInput() { return Input.GetAxis("Horizontal"); }

    void ChangeColor() { }

    void SetAcceleration() {
        Vector3 dir = new Vector3(GetHorizontalInput(), 0, GetVerticalInput());
        dir.Normalize();
        acceleration = dir.magnitude;
    }

    public float GetAcceleration() { return acceleration; }

    public int GetStackLimit() { 
        return stackLimit;
    }

    public void SetStackLimit() {
        stackLimit += 5;
    }

    public void UseMachine() {
        if (levelUp_UI.activeSelf) {
            levelUp_UI.SetActive(false);
            //canUseMachine = true;
        } else {
            //canUseMachine = false;
            levelUp_UI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Client") {
            isNextToClient = true;
        } else { 
            isNextToClient= false;
        }

        if (other.gameObject.tag == "Machine"){
            isNextToMachine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Machine") {
            isNextToMachine = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (canGetClient && other.gameObject.tag == "Client") {
            if (!other.gameObject.GetComponent<Client>().alreadyPicked) {
                AddObjectToStack(other.gameObject.GetComponent<Client>());
                canGetClient = false;
            }
        }
    }
}
