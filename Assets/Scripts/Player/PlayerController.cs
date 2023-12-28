using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float normalSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] bool isCrouched = false;
    [SerializeField] bool isRunning = false;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;

    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;

    void Start()
    {
        normalSpeed = speed;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        navMeshAgent.speed = speed;

        Movement();

        CrouchAndRun();
    }

    void Movement()
    {
        if (navMeshAgent.velocity.magnitude != 0)
        {
            transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(navMeshAgent.velocity - Vector3.zero).eulerAngles.y, 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("walkCrouch", false);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                MoveCharacter(hit.point);
            }
        }
    }

    void MoveCharacter(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    void CrouchAndRun()
    {
        if (Input.GetKeyDown(runKey) && !isRunning && !isCrouched && !animator.GetBool("walkCrouch") && !Input.GetKeyUp(runKey))
        {
            print("isRun");
            speed = runSpeed;
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKeyUp(runKey) && isRunning)
        {
            speed = normalSpeed;
            isRunning = false;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKey(crouchKey) && Input.GetKey(runKey) && speed < runSpeed)
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            if (!isCrouched)
            {
                print("isCrouch");
                speed = crouchSpeed;
                isCrouched = true;
                isRunning = false;
                animator.SetBool("isCrouching", isCrouched);
            }
            else
            {
                speed = normalSpeed;
                isCrouched = false;
                animator.SetBool("isCrouching", isCrouched);
            }
        }

        if (navMeshAgent.velocity.magnitude != 0 && animator.GetBool("isCrouching") && !isRunning) 
        {
            animator.SetBool("walkCrouch", true);
        }
        else
        {
            animator.SetBool("walkCrouch", false);
        }

        if (navMeshAgent.velocity.magnitude != 0 && isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (navMeshAgent.velocity.magnitude != 0 && !isRunning && !isCrouched)
        {
            animator.SetBool("isWalking", true);
        }
    }
}
