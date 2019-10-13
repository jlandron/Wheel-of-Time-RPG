using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    public class Projectile : MonoBehaviour {

        [SerializeField] private float flightSpeed = 10;
        private Health _target = null;
        private float _damage = 0;
        private bool wasAimed = false;

        void Update( ) {
            if( _target == null ) { return; }
            if( !wasAimed ) {
                transform.LookAt( GetAimLocation( ) );
                wasAimed = true;
            }
            transform.Translate( Vector3.forward * flightSpeed * Time.deltaTime );
        }

        private Vector3 GetAimLocation( ) {
            CapsuleCollider targetCapsuleCollider = _target.GetComponent<CapsuleCollider>( );
            return _target.transform.position + ( Vector3.up * targetCapsuleCollider.height / 2 );
        }

        public void SetTarget( Health target , float damage) {
            _target = target;
            _damage = damage;
        }

        private void OnTriggerEnter( Collider other) {
            if(_target != null ) {
                if(other.GetComponent<Health>() == _target ) {
                    _target.TakeDamage( _damage );
                }
                Destroy( gameObject );
            }
        }
    }
}
