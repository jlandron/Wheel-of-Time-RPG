using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinamatics {
    public class CinamaticTrigger : MonoBehaviour, ISaveable {
        private bool wasTriggered = false;

        private void OnTriggerEnter( Collider other ) {
            if( !wasTriggered && other.gameObject.tag == "Player") {
                GetComponent<PlayableDirector>( ).Play( );
                wasTriggered = true;
            }
        }

        public object CaptureState( ) {
            return wasTriggered;
        }

        public void RestoreState( object state ) {
            wasTriggered = ( bool )state;
        }
    }
}
