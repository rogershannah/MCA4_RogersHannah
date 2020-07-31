using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject attackPrefab;
    public GameObject breakPrefab;

    public float projectileSpeed = 100;
    public float reticleFocusSpeed = 3;

    public Image reticleImage;

    public Color reticleAttackColor;
    public Color reticleBreakColor;

    Color originalReticleColor;
    GameObject currentProjectilePrefab;
    void Start()
    {
        originalReticleColor = reticleImage.color;
        currentProjectilePrefab = attackPrefab;
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           GameObject projectile = Instantiate(currentProjectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) //may want to define public float/int range to limit speel reach
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                currentProjectilePrefab = attackPrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, reticleAttackColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, .7f, 1), Time.deltaTime * reticleFocusSpeed);
            }
            else if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("CeilingProb"))
            {
                currentProjectilePrefab = breakPrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, reticleBreakColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, .7f, 1), Time.deltaTime * reticleFocusSpeed);
            }
            else
            {
                currentProjectilePrefab = attackPrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * reticleFocusSpeed);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * reticleFocusSpeed);
        }
    }
}
