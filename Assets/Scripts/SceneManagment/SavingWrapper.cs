using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagment {
    public class SavingWrapper : MonoBehaviour {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 2f;

        private IEnumerator Start( ) {
            Fader fader = FindObjectOfType<Fader>( );
            fader.FadeOutImmeduate( );
            yield return GetComponent<SavingSystem>( ).LoadLastScene( defaultSaveFile );
            yield return fader.FadeIn( fadeInTime );


        }

        void Update( ) {
            if( Input.GetKeyDown( KeyCode.L ) ) {
                Load( );
            }
            if( Input.GetKeyDown( KeyCode.S ) ) {
                Save( );
            }
        }

        public void Save( ) {
            GetComponent<SavingSystem>( ).Save( defaultSaveFile );
        }

        public void Load( ) {
            GetComponent<SavingSystem>( ).Load( defaultSaveFile );
        }
    }
}
