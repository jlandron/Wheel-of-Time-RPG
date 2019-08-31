using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction {
        NavMeshAgent navMeshAgent;
        [SerializeField] Transform target;
        private bool dead = false;
        private void Start( ) {
            navMeshAgent = GetComponent<NavMeshAgent>( );
        }
        void Update( ) {
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
            navMeshAgent.isStopped = false;
        }
        public void StartMoveAction( Vector3 destination ) {
            GetComponent<ActionScheduler>( ).StartAction( this );
            MoveTo( destination );
        }
        public void Cancel( ) {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
        }
    }
}
