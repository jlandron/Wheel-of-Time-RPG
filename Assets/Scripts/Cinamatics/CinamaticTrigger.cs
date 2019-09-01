using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinamatics {
    public class CinamaticTrigger : MonoBehaviour {
        private bool wasTriggered = false;
        private void OnTriggerEnter( Collider other ) {
            if( !wasTriggered && other.gameObject.tag == "Player") {
                GetComponent<PlayableDirector>( ).Play( );
                wasTriggered = true;
            }
        }
    }
}
