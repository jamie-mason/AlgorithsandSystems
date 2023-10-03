using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] public PathManager pathManager;

    List<Waypoints> thePath;
    Waypoints target;

    public float MoveSpeed;
    public float RotateSpeed;
   

    public Animator animator;
    bool isWalking;

    void Start()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
        
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
       
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
        if (Input.anyKeyDown)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        //target = pathManager.GetNextTarget();
        
    }
}
