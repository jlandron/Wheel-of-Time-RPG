using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour {

        [SerializeField] float weaponRange = 2.0f;
        Mover mover;
        Transform target;
        private void Start( ) {
            mover = GetComponent<Mover>( );
        }
        private void Update( ) {
            if( !target ) return;
            if( !GetIsInRange( ) ) {
                mover.MoveTo( target.position );
            } else {
                mover.Stop( );
            }
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
