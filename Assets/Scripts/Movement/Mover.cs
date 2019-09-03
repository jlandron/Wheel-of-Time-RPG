using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction, ISaveable {

        [SerializeField] Transform target;

        private NavMeshAgent _navMeshAgent;
        private Health _health;

        private void Start( ) {
            _navMeshAgent = GetComponent<NavMeshAgent>( );
            _health = GetComponent<Health>( );
        }
        void Update( ) {
            _navMeshAgent.enabled = !_health.isDead;
            UpdateAnimator( );
        }
        private void UpdateAnimator( ) {
            Vector3 velocity = GetComponent<NavMeshAgent>( ).velocity;
            Vector3 localVelocity = transform.InverseTransformDirection( velocity );
            float speed = localVelocity.z;
            GetComponentInChildren<Animator>( ).SetFloat( "ForwardSpeed", speed );
        }
        public void MoveTo( Vector3 destination ) {
            GetComponent<NavMeshAgent>( ).destination = destination;
            _navMeshAgent.isStopped = false;
        }
        public void StartMoveAction( Vector3 destination ) {
            GetComponent<ActionScheduler>( ).StartAction( this );
            MoveTo( destination );
        }
        public void Cancel( ) {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.isStopped = true;
        }
        public void SetMaxSpeed(float speed ) {
            _navMeshAgent.speed = speed;
        }

        public object CaptureState( ) {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState( object state ) {
            SerializableVector3 position = ( SerializableVector3 )state;
            GetComponent<NavMeshAgent>( ).enabled = false;
            transform.position = position.ToVector( );
            GetComponent<NavMeshAgent>( ).enabled = true;
        }
    }
}
