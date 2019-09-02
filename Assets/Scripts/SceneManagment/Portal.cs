using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment {
    public class Portal : MonoBehaviour {

        enum DestinationIdentifier {
            A, B, C, D, E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 3f;

        private Fader fader;
        private void Start( ) {
            spawnPoint = GetComponentInChildren<Transform>( );
        }
        private void OnTriggerEnter( Collider other ) {
            if( other.tag == "Player" ) {
                StartCoroutine( Transition( ) );
            }
        }
        private IEnumerator Transition( ) {
            if(sceneToLoad < 0 ) {
                Debug.Log( "Scene to load not set" );
                yield break;
            }
            
            DontDestroyOnLoad( gameObject );
            fader = FindObjectOfType<Fader>( );
            if( !fader ) {
                Debug.Log( "fader not found!" );
            }
            yield return fader.FadeOut( fadeOutTime );

            yield return SceneManager.LoadSceneAsync( sceneToLoad );
            Portal otherPortal = GetOtherPortal( );
            UpdatePlayer( otherPortal );

            yield return new WaitForSeconds( fadeInTime );
            yield return fader.FadeIn( fadeInTime );
            Destroy( gameObject );
        }

        private void UpdatePlayer( Portal otherPortal ) {
            GameObject player = GameObject.FindWithTag( "Player" );
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal( ) {
            Portal[] portals = FindObjectsOfType<Portal>( );
            foreach( Portal portal in portals ) {
                if( portal == this ) { continue; }
                if(this.destination == portal.destination ) {
                    return portal;
                }
            }
            return null;
        }
    }
}

