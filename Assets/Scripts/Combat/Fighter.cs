using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        private float _timeSinceLastAttack = Mathf.Infinity;

        private Mover _mover;
        private Health _target;
        private void Start( ) {
            _mover = GetComponent<Mover>( );
            SpawnWeapon( );
        }
        private void Update( ) {
            _timeSinceLastAttack += Time.deltaTime;
            if( !_target ) { return; }
            if( _target.isDead ) { return; }
            if( !GetIsInRange( ) ) {
                _mover.MoveTo( _target.transform.position );
            } else {
                _mover.Cancel( );
                AttackBehavior( );
            }
        }
        public void SpawnWeapon() {
            Instantiate( weaponPrefab, handTransform );

            Animator animator = GetComponent<Animator>( );
            animator.runtimeAnimatorController = weaponOverride;
        }
        private void AttackBehavior( ) {
            transform.LookAt( _target.transform );
            if( _timeSinceLastAttack >= timeBetweenAttacks ) {
                GetComponent<Animator>( ).ResetTrigger( "StopAttack" );
                GetComponent<Animator>( ).SetTrigger( "Attack" );
                //triggers Hit event at the right time
                _timeSinceLastAttack = 0;
            }
        }
        //animation event
        void Hit( ) {
            if( _target == null ) { return; }
            _target.TakeDamage( weaponDamage );
        }
        private bool GetIsInRange( ) {
            return Vector3.Distance( transform.position, _target.transform.position ) <= weaponRange;
        }
        public bool CanAttack( GameObject combatTarget ) {
            if(combatTarget == null ) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>( );
            return targetToTest != null && !targetToTest.isDead;
        }
        public void Attack( GameObject combatTarget ) {
            GetComponent<ActionScheduler>( ).StartAction( this );
            _target = combatTarget.GetComponent<Health>( );
        }
        public void Cancel( ) {
            GetComponent<Animator>( ).ResetTrigger( "Attack" );
            GetComponent<Animator>( ).SetTrigger( "StopAttack" );
            _target = null;
            GetComponent<Mover>( ).Cancel( );
        }

    }

}
