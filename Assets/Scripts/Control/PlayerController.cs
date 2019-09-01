using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour {
        private Health _health;
        private Fighter _fighter;

        private void Start( ) {
            _health = GetComponent<Health>( );
            _fighter = GetComponent<Fighter>( );
        }
        void Update( ) {
            if( _health.isDead ) { return; }
            if( InteractWithCombat( ) ) return;
            if( InteractWithMovement( ) ) return;
        }

        private bool InteractWithCombat( ) {
            RaycastHit[] hits = Physics.RaycastAll( GetMouseRay( ) );

            foreach( RaycastHit raycastHit in hits ) {
                CombatTarget target = raycastHit.transform.GetComponent<CombatTarget>( );
                if( target == null ) { continue; }
                if( !_fighter.CanAttack( target.gameObject ) ) { continue; }
                if( Input.GetMouseButton( 0 ) ) {
                    _fighter.Attack( target.gameObject );
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement( ) {
            RaycastHit hit;
            bool hasHit = Physics.Raycast( GetMouseRay( ), out hit );
            if( hasHit ) {
                if( Input.GetMouseButton( 0 ) ) {
                    GetComponent<Mover>( ).StartMoveAction( hit.point );
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay( ) {
            return Camera.main.ScreenPointToRay( Input.mousePosition );
        }
    }
}