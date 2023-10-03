using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2Controller : MonoBehaviour
{
    [SerializeField] public PathManager pathManager;

    List<Waypoints> thePath;
    Waypoints target;

    public float MoveSpeed;
    public float RotateSpeed;
    bool isPath = false;
    bool canInput = true;

    public Animator animator;
    bool isWalking;

    void Start()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            isPath = false;
            target = thePath[0];
        }
    }

    bool rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        if (transform.eulerAngles == newDir)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distaceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distaceToTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && canInput)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);



        }
        if (isWalking)
        {
            animator.speed = MoveSpeed;
            rotateTowardsTarget();
            moveForward();
        }
        else
        {
            animator.speed = 1f;
        }
        if (isPath)
        {
            if (!rotateTowardsTarget())
            {
                rotateTowardsTarget();
            }
            else
            {
                isPath = false;
                isWalking = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        target = pathManager.GetNextTarget();
        if (isWalking && other.transform.position == thePath[thePath.Count - 2].getPos())
        {
            animator.SetBool("isWalking", !isWalking);
            isWalking = false;
            isPath = true;
            canInput = false;
        }
    }
}
