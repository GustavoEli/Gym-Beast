using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Character
{

    Character parent;
    static Player player;
    static float distanceOfParent = 2f;
    float distance;
    [HideInInspector] public bool alreadyPicked = false;

    Vector3 rotation = new Vector3(0, 0, 0);

    private void Start()
    {
        player = GameObject.Find("Character_Man").GetComponent<Player>();
        //this.angle = 180f * Mathf.Deg2Rad;
        //this.meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        this.StackMovement();
    }

    /*void RotateWithPlayer() {
        Quaternion rotation = Quaternion.LookRotation(this.parent.position - this.position, Vector3.up);
        this.transform.rotation = rotation;
    }*/

    void StackMovement() {
        if (this.parent != null)
        {
            if (this.parent.gameObject.tag == "Player")
            {
                this.position = parent.transform.TransformPoint(Vector3.back);
                transform.rotation = parent.transform.rotation * Quaternion.Euler(rotation);

                /*Vector3 backDir = new Vector3(Mathf.Cos(player.transform.rotation.eulerAngles.y + this.angle), 0, 
                                              Mathf.Atan(Mathf.Cos(player.transform.rotation.eulerAngles.y + this.angle) + Mathf.Sin(player.transform.rotation.y + this.angle)));

                this.position = new Vector3(this.parent.position.x - backDir.normalized.x, 
                                            this.parent.position.y, 
                                            (this.parent.position.z + 1.2f) - (backDir.normalized.z));
                
                RotateWithPlayer();*/
            }
            else
            {
                this.position = parent.transform.TransformPoint(Vector3.up * 2);
                transform.rotation = parent.transform.rotation * Quaternion.Euler(rotation);
            }
        }
    }
    public void AddParentObject(Character obj) {
        this.parent = obj;
        if (this.parent.gameObject.tag != "Player")
        {
            this.position = new Vector3(this.parent.position.x, this.parent.position.y + distanceOfParent, this.parent.position.z);
            this.distance = this.parent.position.y + distanceOfParent;
        }
    }

    public void LeaveParent() {
        this.parent = null;
        this.position = new Vector3(player.position.x, player.position.y, player.position.z + 1.5f);
        this.alreadyPicked = false;
    }

    void inerciaForce()
    {
        this.inertiaForce = mass * player.GetAcceleration();
    }

    float SetTwistMoment()
    {
        //um dos dois que funcionara
        //this.twistMoment = this.inertiaForce * (this.position.y - (this.parent.position.y + distanceOfParent));
        return this.twistMoment = (mass * player.GetAcceleration()) * this.position.y + distanceOfParent;
    }

    void MovPhysics(Character obj) { }

    void RotPhysics(Character obj) { }
}
