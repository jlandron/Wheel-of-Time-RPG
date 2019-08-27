using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Health : MonoBehaviour {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] float currentHealth;

        private void Start( ) {
            currentHealth = maxHealth;
        }
        public void TakeDamage( float damage ) {
            currentHealth = Mathf.Max( currentHealth - damage, 0);
            if(currentHealth == 0 ) {
                print( "dead!" );
            } else {
                print( currentHealth + " / " + maxHealth );
            }
        }

    }
}
