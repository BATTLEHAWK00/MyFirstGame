using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float TTL = 1f;
    private GameObject target;
    private UnityAction onTrigger;
    private bool isDestroying = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        TTL -= Time.fixedDeltaTime;
        if(TTL<=0 && !isDestroying)
        {
            Destroy(gameObject);
        }
    }
    public void Init(GameObject target,UnityAction onTrigger,float duration)
    {
        this.target = target;
        TTL = duration;
        this.onTrigger = onTrigger;
        GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 50f, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject;
        if (unit == target&&!isDestroying)
        {
            isDestroying = true;
            GetComponentInChildren<Light>().range = 30;
            GetComponentInChildren<Light>().intensity = 1.5f;
            onTrigger();
            GetComponent<Rigidbody>().isKinematic = true;
            MonoBase.Get().GetMono().RunDelayTask(() => { Destroy(gameObject); }, 0.05f);
        }
    }
}
