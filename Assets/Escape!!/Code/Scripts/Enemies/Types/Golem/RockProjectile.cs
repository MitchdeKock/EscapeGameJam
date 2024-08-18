using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [SerializeField] private float timeToImpact = 2.0f;
    [SerializeField] private float AoERadius;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private AnimationCurve scaleCurve;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float damage;

    private float elapsedTime = 0.0f;

    public void InitializeRockProjectile(Vector3 endPoint, float damage)
    {
        this.endPoint = endPoint;
        this.damage = damage;
    }

    private void Start()
    {
        startPoint = transform.position;
    }

    void Update()
    {
        if (elapsedTime < timeToImpact)
        {
            elapsedTime += Time.deltaTime * 0.9f;
            float t = Mathf.Clamp01(elapsedTime / timeToImpact);

            transform.position = Vector3.Lerp(startPoint, endPoint, movementCurve.Evaluate(t));
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(2f, 2f, 2f), scaleCurve.Evaluate(t));
        }
        else
        {
            DealAoE();
        }
    }

    private void DealAoE()
    {
        foreach (var target in TargetsInRange())
        {
            target.Health -= (int)damage;
        }

        //Todo partical effect
        Destroy(gameObject);
    }

    private List<CoreHealthHandler> TargetsInRange()
    {
        List<CoreHealthHandler> objectsInArc = new List<CoreHealthHandler>();

        Vector2 centerPosition = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, AoERadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<CoreHealthHandler>(out CoreHealthHandler playerHealth))
            {
                objectsInArc.Add(playerHealth);
            }
        }

        return objectsInArc;
    }
}
