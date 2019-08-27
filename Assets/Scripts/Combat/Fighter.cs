using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 10f;

        private float timeSinceLastAttack; 

        Mover mover;
        Transform target;
        private void Start( ) {
            mover = GetComponent<Mover>( );
        }
        private void Update( ) {
            timeSinceLastAttack += Time.deltaTime;
            if( !target ) return;
            if( !GetIsInRange( ) ) {
                mover.MoveTo( target.position );
            } else {
                mover.Cancel( );
                AttackBehavior( );
            }
        }

        private void AttackBehavior( ) {
            if(timeSinceLastAttack >= timeBetweenAttacks ) {
                GetComponent<Animator>( ).SetTrigger( "Attack" );
                //triggers Hit event at the right time
                timeSinceLastAttack = 0;
            }
        }
        //animation event
        void Hit( ) {
            target.GetComponent<Health>( ).TakeDamage( weaponDamage );
        }
        private bool GetIsInRange( ) {
            return Vector3.Distance( transform.position, target.position ) <= weaponRange;
        }

        public void Attack( CombatTarget combatTarget ) {
            GetComponent<ActionScheduler>( ).StartAction( this );
            target = combatTarget.transform;
        }
        public void Cancel( ) {
            target = null;
        }
        
    }

}
