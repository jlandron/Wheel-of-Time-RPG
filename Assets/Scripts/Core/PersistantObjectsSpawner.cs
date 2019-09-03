using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.SceneManagment {
    public class PersistantObjectsSpawner : MonoBehaviour {
        [SerializeField] GameObject[] peristantObjectPrefabs;

        static bool hasSpawned = false;
        private void Awake( ) {
            if( hasSpawned ) return;

            SpawnPersistantObjects( );

            hasSpawned = true;
        }

        private void SpawnPersistantObjects( ) {
            foreach( GameObject gameObject in peristantObjectPrefabs ) {
                GameObject persistantObject = Instantiate( gameObject );
                DontDestroyOnLoad( persistantObject );
            }
        }
    }
}
