using System;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _playRb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private TextMeshProUGUI playerNameText;


    private static readonly int SpeedF = Animator.StringToHash("Speed_f");


    public event Action OnInteract;
    public GameObject ObjectInRange { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        _playRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
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
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        //move
        if(verticalInput != 0)
            _playRb.AddForce(transform.forward * (moveSpeed * verticalInput * Time.deltaTime));
            
        //_playRb.AddForce(Vector3.right * (moveSpeed * horizontalInput));
        
        //rotation
        transform.Rotate(Vector3.up * (turnSpeed * horizontalInput * Time.deltaTime));

        if(verticalInput != 0)
            _animator.SetFloat(SpeedF, 1f);
        else
            _animator.SetFloat(SpeedF, 0f);
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
