using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinamatics {
    public class CinamaticsControlRemover : MonoBehaviour {

        private GameObject _player;

        private void Start( ) {
            GetComponent<PlayableDirector>( ).played += DisableControl;
            GetComponent<PlayableDirector>( ).stopped += EnableControl;
            _player = GameObject.FindWithTag( "Player" );
        }
        void DisableControl( PlayableDirector playableDirector ) {
            _player.GetComponent<ActionScheduler>( ).CancelCurrentAction( );
            _player.GetComponent<PlayerController>( ).enabled = false;
        }
        void EnableControl( PlayableDirector playableDirector ) {
            _player.GetComponent<PlayerController>( ).enabled = true;
        }
    }
}

