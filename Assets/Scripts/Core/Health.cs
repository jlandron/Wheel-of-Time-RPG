using RPG.Saving;
using UnityEngine;

namespace RPG.Core {
    public class Health : MonoBehaviour, ISaveable {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] float currentHealth;

        private bool _isDead = false;
        public bool isDead {
            get { return _isDead; }
            private set { }
        } 
        private void Awake( ) {
            currentHealth = maxHealth;
        }
        public void TakeDamage( float damage ) {
            currentHealth = Mathf.Max( currentHealth - damage, 0 );
            if( currentHealth == 0 ) {
                Die( );
            } else {
                Debug.Log( currentHealth + " / " + maxHealth );
            }
        }
        private void Die( ) {
            if( !isDead ) {
                GetComponent<Animator>( ).SetTrigger( "Die" );
                _isDead = true;
                GetComponent<ActionScheduler>( ).CancelCurrentAction( );
            }
        }

        public object CaptureState( ) {
            return currentHealth;
        }

        public void RestoreState( object state ) {
            currentHealth = ( float )state;
            if( currentHealth == 0 ) {
                Die( );
            }
        }
    }
}
