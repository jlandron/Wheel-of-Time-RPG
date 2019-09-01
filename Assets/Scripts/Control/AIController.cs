using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolorance = 1f;
        [SerializeField] float dwellTime = 2f;
        [Range(0.1f,5.66f)]
        [SerializeField] float patrolSpeed = 2f;
        [Range( 0.1f, 5.66f )]
        [SerializeField] float chaseSpeed = 5.66f;

        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private Vector3 _guardPosition;
        private int _currentWaypointIndex = 0;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;


        private void Start( ) {
            _fighter = GetComponent<Fighter>( );
            _player = GameObject.FindWithTag( "Player" );
            _health = GetComponent<Health>( );
            _mover = GetComponent<Mover>( );
            _guardPosition = transform.position;
        }

        void Update( ) {
            if( _health.isDead ) { return; }
            if( checkIfPlayerInRange( ) && _fighter.CanAttack( _player ) ) {
                AttackBehavior( );
                _mover.SetMaxSpeed( chaseSpeed );
            } else if( _timeSinceLastSawPlayer <= suspicionTime ) {
                SuscpicionBehavior( );
            } else {
                _mover.SetMaxSpeed( patrolSpeed );
                PatrolBehavior( );
            }
            UpdateTimers( );
        }

        private void UpdateTimers( ) {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehavior( ) {
            _fighter.Attack( _player );
            _timeSinceLastSawPlayer = 0;
        }

        private void SuscpicionBehavior( ) {
            GetComponent<ActionScheduler>( ).CancelCurrentAction( );

        }
        private void PatrolBehavior( ) {
            Vector3 nextPosition = _guardPosition;
            if( patrolPath ) {
                if( AtWaypoint( ) ) {
                    CycleWaypoint( );
                    _timeSinceArrivedAtWaypoint = 0;
                }
                nextPosition = GetCurrentWaypoint( );
            }
            if( _timeSinceArrivedAtWaypoint > dwellTime ) {
                _mover.StartMoveAction( nextPosition );
            }

        }

        private Vector3 GetCurrentWaypoint( ) {
            return patrolPath.GetWaypoint( _currentWaypointIndex );
        }

        private void CycleWaypoint( ) {
            _currentWaypointIndex = patrolPath.GetIndex( ++_currentWaypointIndex );
        }

        private bool AtWaypoint( ) {
            float distanceToWaypoint = Vector3.Distance( transform.position, GetCurrentWaypoint( ) );
            return distanceToWaypoint <= waypointTolorance;
        }

        private bool checkIfPlayerInRange( ) {
            float dist = Vector3.Distance( transform.position, _player.transform.position );
            return dist <= chaseDistance;
        }
        //called by Unity
        private void OnDrawGizmosSelected( ) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere( transform.position, chaseDistance );
        }
    }
}

