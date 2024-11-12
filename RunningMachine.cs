using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMachine : MonoBehaviour
{
    [SerializeField] Player player;
    float time = 0;
    bool isOnMachine = false;

    void Update() {
        if (this.isOnMachine)
        {
            this.checkTimeToGiveMoney();
        }
        Debug.Log(this.time);
    }

    void checkTimeToGiveMoney() {
        if (Mathf.RoundToInt(this.time) % 10f == 0) {
            Player.money += 5;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Client")
        {
            this.isOnMachine = true;
            this.time += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Client") {
            this.time = 0;
            this.isOnMachine = false;
        }
    }
}
