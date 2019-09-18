using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    [CreateAssetMenu( fileName = "Weapon", menuName = "RPG Weapons/Make New Weapon", order = 0 )]
    public class Weapon : ScriptableObject {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float timeBetweenAttacks = 1.0f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;


        public void Spawn( Transform rightHand, Transform leftHand, Animator animator ) {
            if( weaponPrefab != null ) {
                Transform hand = GetWeaponHand( rightHand, leftHand );
                Instantiate( weaponPrefab, hand );
            }
            if( animatorOverride != null ) {
                animator.runtimeAnimatorController = animatorOverride;
            }

        }

        private Transform GetWeaponHand( Transform rightHand, Transform leftHand ) {
            if( isRightHanded ) {
                return rightHand;
            }
            return leftHand;
        }

        public float GetWeaponRange( ) {
            return weaponRange;
        }
        public float GetTimeBetweenAttacks( ) {
            return timeBetweenAttacks;
        }
        public float GetWeaponDamage( ) {
            return weaponDamage;
        }
        public bool HasProjectile( ) {
            return ( projectile != null );
        }
        public void LaunchProjectile( Transform rightHand, Transform leftHand, Health target ) {
            Transform hand = GetWeaponHand( rightHand, leftHand );
            Projectile projInstance = Instantiate( projectile , hand.position , Quaternion.identity);
            projInstance.SetTarget( target );
        }
    }
}
