using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu( fileName = "Weapon", menuName = "RPG Weapons/Make New Weapon", order = 0 )]
    public class Weapon : ScriptableObject {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float timeBetweenAttacks = 1.0f;
        [SerializeField] private float weaponDamage = 10f;


        public void spawn(Transform handTransform, Animator animator ) {
            if(weaponPrefab != null ) {
                Instantiate( weaponPrefab, handTransform );
            }
            if(animatorOverride != null ) {
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }
        public float getWeaponRange( ) {
            return weaponRange;
        }
        public float getTimeBetweenAttacks( ) {
            return timeBetweenAttacks;
        }
        public float getWeaponDamage( ) {
            return weaponDamage;
        }
    }
}
