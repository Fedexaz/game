using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        navMeshAgent.speed = speed;

        Movement();
    }

    void Movement()
    {
        if (navMeshAgent.velocity != Vector3.zero)
        {
            transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(navMeshAgent.velocity - Vector3.zero).eulerAngles.y, 0);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
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
}
