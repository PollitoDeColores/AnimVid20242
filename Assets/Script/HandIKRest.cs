using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIKRest : MonoBehaviour
{
    [SerializeField] private Transform detectionReference;
    [SerializeField] private Transform hand;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask detectionLayers;
    [SerializeField] private AvatarIKGoal handGoal;
    [SerializeField] private FloatDamperclass animationTransition;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animationTransition.Update();
    }

    public void OnAnimatorIK(int layerIndex)
    {
       Collider [] detectedColliders =  Physics.OverlapSphere(detectionReference.position, detectionRadius, detectionLayers);
        if (detectedColliders.Length <= 0) return; 
        Vector3 nearestSurfacePoint = detectedColliders[0].ClosestPoint(hand.position); 
        foreach (Collider detectedCollider in detectedColliders) 
        {
            Vector3 currentClosedPoint = detectedCollider.ClosestPoint(hand.position);
            float currentHandDistance = (currentClosedPoint - hand.position).sqrMagnitude;
            //1. En caso de que la mano esté dentro del collider, se asume que la mano es el punto más cercano 
            if(currentHandDistance < 0.01f)
            {
                nearestSurfacePoint = currentClosedPoint;
                break; 
            }

            // Si el punto actual está mas cerca de la mano que la variable de referencia, intercambio 
            float distanceToSurfacePoint = (nearestSurfacePoint - hand.position).sqrMagnitude;
            if(currentHandDistance < distanceToSurfacePoint) 
            {
                nearestSurfacePoint = currentClosedPoint;
            }


        }

        Vector3 rayDir = nearestSurfacePoint - detectionReference.position;
        Ray r = new Ray(detectionReference.position, nearestSurfacePoint);
        RaycastHit hit; 
        bool hasSurface = Physics.Raycast(r, out hit, rayDir.magnitude * 1.05f, detectionLayers);
        animationTransition.TargetValue = hasSurface ? 1 : 0;
        anim.SetIKPositionWeight(handGoal, animationTransition.CurrentValue); 
        anim.SetIKPosition(handGoal, hit.point);

    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (detectionReference == null) 
        {
            return;
        }
        Gizmos.DrawWireSphere(detectionReference.position, detectionRadius);
    }

#endif

}
