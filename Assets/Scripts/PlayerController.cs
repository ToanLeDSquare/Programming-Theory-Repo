using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _playRb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private TextMeshProUGUI playerNameText;
    
    [SerializeField] float lookRadius = 10f;
    private bool _isMoving;

    private static readonly int SpeedF = Animator.StringToHash("Speed_f");
    
    public event Action OnInteract;
    public GameObject ObjectInRange { get; set; }
    
    private Vector3 _hitPos;
    [SerializeField] private GameObject cyclePrefab;
    private GameObject _cyclePrefab;
    private List<GameObject> _cyclePrefabsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = moveSpeed;
        _agent.acceleration = 999;
        _agent.angularSpeed = 999;
        
        _playRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _hitPos = transform.position;
        
        if(DataManager.Instance.userName != null)
            playerNameText.text = DataManager.Instance.userName;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        // var horizontalInput = Input.GetAxis("Horizontal");
        // var verticalInput = Input.GetAxis("Vertical");
        //
        // //move
        // if(verticalInput != 0)
        //     _playRb.AddForce(transform.forward * (moveSpeed * verticalInput * Time.deltaTime));
        //     
        // //_playRb.AddForce(Vector3.right * (moveSpeed * horizontalInput));
        //
        // //rotation
        // transform.Rotate(Vector3.up * (turnSpeed * horizontalInput * Time.deltaTime));
        
        if ( Input.GetMouseButtonDown(0))
        {
            HideCyclePoint();
            HandleAction();
        }
        
        // if (_hitPos != transform.position)
        //     _animator.SetFloat(SpeedF, 1f);
        // else
        //     _animator.SetFloat(SpeedF, 0f);
        
        _distance = Vector3.Distance(_hitPos, transform.position);

        if (_distance <= 3.5f)
        {
            _animator.SetFloat(SpeedF, 0);
            HideCyclePoint();
        }
        else
        {
            _animator.SetFloat(SpeedF, 1f);
        }
    }

    private float _distance;
    public void HandleAction()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _distance= Vector3.Distance(hit.point, transform.position);
            _hitPos = hit.point;
            _hitPos = new Vector3(_hitPos.x, 0, _hitPos.z);
             //_cyclePrefab = Instantiate(cyclePrefab, _hitPos, Quaternion.identity);
             _cyclePrefabsList.Add(Instantiate(cyclePrefab, _hitPos, Quaternion.identity));
            if (_distance > 1f)
            {
                StartCoroutine(GoTo(_hitPos));
            }
        }
    }

    void HideCyclePoint()
    {
        if (_cyclePrefabsList.Count > 0)
        {
            foreach (var cycle in _cyclePrefabsList)
            {
                Destroy(cycle);
            }

            _cyclePrefabsList.Clear();
        }
    }

    IEnumerator GoTo(Vector3 pos)
    {
        yield return _agent.SetDestination(pos);
    }
    
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnInteract != null && ObjectInRange != null)
        {
            //Trigger the OnInteract event with is detected by the Pickup and Character scripts
            OnInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag($"Animal"))
            ObjectInRange = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectInRange = null;
    }
}
