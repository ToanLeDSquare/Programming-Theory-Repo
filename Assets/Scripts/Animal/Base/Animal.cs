using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animal.Base
{
    public class Animal : MonoBehaviour
    {
        //This is the base for all animals
        private bool _moveWait;
        protected bool MoveEnabled = true;
        protected bool IsDead;
        protected Animator AnimController;
        protected PlayerController Player;

        protected float Speed = 3f;
    
        //Animator
        private static readonly int JumpB = Animator.StringToHash("Jump_b");
     

        // Start is called before the first frame update
        void Start()
        {
            AnimController = GetComponent<Animator>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Player.OnInteract += DialogDisplay;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_moveWait && MoveEnabled)
            {
                StartCoroutine(Wander());
            }
        
            if(!MoveEnabled)
                StartCoroutine( AnimalAction());
            
            if(IsDead)
                Dead();
        }


        //Have the animal move in a random direction periodically.
        protected virtual IEnumerator Wander()
        {
            _moveWait = true;
            float delay = Random.Range(3f, 7f);
            yield return new WaitForSeconds(delay);
            int diceroll = Random.Range(1, 7);

            if (diceroll > 3 && MoveEnabled)
            {
                Vector3 direction = RandomDirection();
                StartCoroutine(MoveRoutine(direction));
            }
            _moveWait = false;
        }

        //Generate a random X/Z vector
        protected Vector3 RandomDirection()
        {
            float randomAngle = Random.Range(0f, 360f);

            Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

            return direction;
        }

        //Move the animal in the random direction for a randomised number of fixed updates
        protected virtual IEnumerator MoveRoutine(Vector3 direction)
        {
            int framesToMove = Random.Range(20, 80);
            Vector3 lookDir = new Vector3(direction.x, 0, direction.z);
            transform.rotation = Quaternion.LookRotation(lookDir);

            for (int i = 0; i < framesToMove; i++)
            {
                transform.Translate(Speed * Time.deltaTime * direction, Space.World);
                yield return new WaitForFixedUpdate();
            }
        }

    
        protected virtual void DialogDisplay()
        {
            if (Player.ObjectInRange == gameObject)
            {
                AnimalVoice();
            }
        }

        protected virtual void AnimalVoice()
        {
        
        }

        protected virtual IEnumerator AnimalAction()
        {
            yield return null;
        }

        protected virtual void Dead()
        {
            
        }

        protected virtual void ObjectiveComplete()
        {
            //Debug.Log("Objective Complete called: " + gameObject.name);
            AnimController.SetBool(JumpB, true);
        }

        private void OnMouseDown()
        {
            DialogDisplay();
        }
    }
}
