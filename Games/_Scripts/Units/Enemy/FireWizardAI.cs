using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizardAI : EnemyAI
{
    protected override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        _minDistance = 1.3f;
        _maxDistance = 3.5f;
        _moveSpeed = 750f;
    }

    protected override void Move()
    {
        base.Move();
        if (
            _path == null
            || _currentWaypoint >= _path.vectorPath.Count
            || _distanceToTarget.magnitude >= _maxDistance
        )
            return;

        LookAtTarget();
        // If the magnitude of the distance to the target is <= the minimum distance
        // we move the enemy vertically
        if (_distanceToTarget.magnitude <= _minDistance)
        {
            if (Mathf.Abs(_target.position.y - _rb.position.y) >= _verticalThreshold)
            {
                Vector2 verticalDirection = new Vector2(0, _target.position.y - _rb.position.y);
                verticalDirection.Normalize();
                Vector2 verticalForce = verticalDirection * _moveSpeed * Time.fixedDeltaTime;
                _rb.AddForce(verticalForce);
            }
        }
        else
        {
            Vector2 direction = (Vector2)_path.vectorPath[_currentWaypoint] - _rb.position;
            direction.Normalize();
            Vector2 force = direction * _moveSpeed * Time.fixedDeltaTime;
            _rb.AddForce(force);
            float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
                ++_currentWaypoint;
        }
    }
}
