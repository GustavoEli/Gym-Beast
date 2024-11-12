using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Player player;
    private bool isPaused;

    void Update(){
        Control();
    }

    void Control() {
        if (Input.GetKey(KeyCode.A) && player.canWalk || Input.GetButtonDown("Vertical") && player.canWalk)
        {
            player.SideMovement(-1);
        }

        if (Input.GetKey(KeyCode.W) && player.canWalk || Input.GetButtonDown("Vertical") && player.canWalk)
        {
            player.Front_BackMovement(1);
        }

        if (Input.GetKey(KeyCode.D) && player.canWalk || Input.GetButtonDown("Horizontal") && player.canWalk) {
            player.SideMovement(1);
        }

        if (Input.GetKey(KeyCode.S) && player.canWalk || Input.GetButtonDown("Vertical") && player.canWalk) {
            player.Front_BackMovement(-1);
        }

        if (Input.GetKeyDown(KeyCode.K) && player.canWalk || Input.GetButtonDown("Punch") && player.canWalk) {
            player.AnimationPunch();
        }

        if (Input.GetKeyDown(KeyCode.L) && player.isNextToClient || Input.GetButtonDown("GetClient") && player.canWalk) {
            player.canGetClient = true;
        } 
        
        if (Input.GetKeyDown(KeyCode.L) && player.isNextToMachine) {
            player.UseMachine();
            //player.canUseMachine = true;
        }
        
        if (Input.GetKeyDown(KeyCode.O) && player.canWalk || Input.GetButtonDown("Drop") && player.canWalk) {
            player.RemoveObjFromStack();
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) { 
            player.AnimationIdle();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            if (!isPaused) {
                Time.timeScale = 0f;
                isPaused = true;
            } else {
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }
}
