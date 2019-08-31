using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 10f;

        private float timeSinceLastAttack;

        Mover mover;
        Health target;
        private void Start( ) {
            mover = GetComponent<Mover>( );
        }
        private void Update( ) {
            timeSinceLastAttack += Time.deltaTime;
            if( !target ) return;
            if( target.isDead ) return;
            if( !GetIsInRange( ) ) {
                mover.MoveTo( target.transform.position );
            } else {
                mover.Cancel( );
                AttackBehavior( );
            }
        }

        private void AttackBehavior( ) {
            transform.LookAt( target.transform );
            if( timeSinceLastAttack >= timeBetweenAttacks ) {
                GetComponent<Animator>( ).ResetTrigger( "StopAttack" );
                GetComponent<Animator>( ).SetTrigger( "Attack" );
                //triggers Hit event at the right time
                timeSinceLastAttack = 0;
            }
        }
        //animation event
        void Hit( ) {
            target.TakeDamage( weaponDamage );
        }
        private bool GetIsInRange( ) {
            return Vector3.Distance( transform.position, target.transform.position ) <= weaponRange;
        }

        public void Attack( CombatTarget combatTarget ) {
            GetComponent<ActionScheduler>( ).StartAction( this );
            target = combatTarget.GetComponent<Health>( );
        }
        public void Cancel( ) {
            GetComponent<Animator>( ).ResetTrigger( "Attack" );
            GetComponent<Animator>( ).SetTrigger( "StopAttack" );
            target = null;
        }

    }

}
