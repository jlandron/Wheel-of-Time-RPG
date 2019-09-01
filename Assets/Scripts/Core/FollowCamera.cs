using UnityEngine;

namespace RPG.Core {
    public class FollowCamera : MonoBehaviour {

        [SerializeField] GameObject target;
        private void Start( ) {
            target = GameObject.FindWithTag( "Player" );
        }
        // Update is called once per frame
        void LateUpdate( ) {
            transform.position = target.transform.position;
        }
    }
}
