using System.Collections;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;
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

        private Fader _fader;
        private SavingWrapper _saver;
        private void Start( ) {
            spawnPoint = GetComponentInChildren<Transform>( );
            _saver = FindObjectOfType<SavingWrapper>( );
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
            _fader = FindObjectOfType<Fader>( );
            if( !_fader ) {
                Debug.Log( "fader not found!" );
            }
            yield return _fader.FadeOut( fadeOutTime );
         
            _saver.Save( );
            yield return SceneManager.LoadSceneAsync( sceneToLoad );
            _saver.Load( );

            Portal otherPortal = GetOtherPortal( );
            UpdatePlayer( otherPortal );
            //save again as a checkpoint after initial loading
            _saver.Save( );

            yield return new WaitForSeconds( fadeInTime );
            yield return _fader.FadeIn( fadeInTime );
            Destroy( gameObject );
        }

        private void UpdatePlayer( Portal otherPortal ) {
            GameObject player = GameObject.FindWithTag( "Player" );
            player.GetComponent<NavMeshAgent>( ).enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>( ).enabled = true;
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

