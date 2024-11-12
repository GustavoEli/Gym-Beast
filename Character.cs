using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected float angle;
    protected Animator animator;
    protected float inertiaForce;
    public static int mass = 10;
    protected float twistMoment;
    protected const float maxAngle = 45f;//angulo maximo para a pilha não cair
    //protected SkinnedMeshRenderer meshRenderer;

    [HideInInspector] public Vector3 position {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    //public float GetTopBound() { return this.meshRenderer.bounds.center.y; }

    public void Movement() { }
}
