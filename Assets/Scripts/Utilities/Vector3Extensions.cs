using UnityEngine;

public static class Vector3Extensions
{
    public static float GetMagnitudeOnAxis(this Vector3 vector, Vector3 axis)
		{
			var vectorMagnitude = vector.magnitude;
			if (vectorMagnitude <= 0)
			{
				return 0;
			}
			var dot = Vector3.Dot(axis, vector / vectorMagnitude);
			var val = dot * vectorMagnitude;
			return val;
		}
}
