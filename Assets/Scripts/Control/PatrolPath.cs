using UnityEngine;

namespace RPG.Control {
    public class PatrolPath : MonoBehaviour {

        private void OnDrawGizmos( ) {
            for( int i = 0; i < transform.childCount; i++ ) {
                Gizmos.DrawSphere( GetWaypoint( i ), 0.1f );
                Gizmos.DrawLine( GetWaypoint( i ), GetWaypoint( i + 1 ) );
            }
        }
        public Vector3 GetWaypoint( int i ) {
            return transform.GetChild( GetIndex( i ) ).position;
        }
        public int GetIndex( int i ) {
            return (( i ) % transform.childCount);
        }
    }
}
