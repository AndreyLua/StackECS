using System;
using UnityEngine;

public static class MathExtensions
{
    public static float Round(this float value, float step)
    {
        bool isNegative = value < 0;

        if (isNegative)
            value = -value;

        float remains = value % step;

        float result = remains >= step / 2f ? value + (step - remains) : value - remains;

        return isNegative ? -result : result;
    }

    public static int Round(this int value, int step)
    {
        bool isNegative = value < 0;

        if (isNegative)
            value = -value; 

        int remains = value % step;

        int result = remains >= step / 2f ? value + (step - remains) : value - remains;

        return isNegative ? -result : result;
    }

    public static int ToInt(this float value) => Convert.ToInt32(value);

    public static float GetGeometricProgressionSum(float firstNum, float multiplier, int memberCount)
    {
        if (memberCount == 0) return 0;
        return (firstNum * (Mathf.Pow(multiplier, memberCount) - 1)) / (multiplier - 1);
    }

    public static float GetGeometricProgressionNumber(float firstNum, float multiplier, int memberNumber)
    {
        if (memberNumber == 0) return 0;
        return firstNum * Mathf.Pow(multiplier, memberNumber - 1);
    }

    public static bool FloatInRange(this float value, float targetValue, float maxOffcet)
    {
        return Mathf.Abs(value - targetValue) <= maxOffcet;
    }

    public static float Normalize(this float value, float treshold = 0.005f)
    {
        if (value >= treshold)
            return 1f;

        if (value <= -treshold)
            return -1f;

        return 0f;
    }

    public static int RoundNumber(this int number, int roundNumber) => number - (number % roundNumber);

    public static Vector2 CalculateIconScreenPosition(Transform player, Vector3 pointPosition, Vector3 pointGlobalScale,
        Camera camera, Vector2? lastIconPosition = null)
    {
        Vector3 zoneHalfScale = pointGlobalScale * 0.5f;
        zoneHalfScale = new Vector3(Mathf.Abs(zoneHalfScale.x), 0, Mathf.Abs(zoneHalfScale.z));

        Vector3 toPlayerDirection = player.position - pointPosition;
        toPlayerDirection = new Vector3(Mathf.Clamp(toPlayerDirection.x, -zoneHalfScale.x, zoneHalfScale.x), 0,
            Mathf.Clamp(toPlayerDirection.z, -zoneHalfScale.z, zoneHalfScale.z));

        float iconMoveMultiplier = Mathf.Clamp01(Mathf.Pow(toPlayerDirection.magnitude / Vector3.Distance(player.position, pointPosition), 2f));

        Vector2 iconPosition = camera.WorldToScreenPoint(new Vector3(pointPosition.x,
            Mathf.Clamp(pointPosition.y, 1f, 5f), pointPosition.z) + toPlayerDirection * iconMoveMultiplier);
        if (lastIconPosition != null)
            return Vector2.Lerp(lastIconPosition.Value, iconPosition, Time.deltaTime * 3f);
        else return iconPosition;
    }

    public static bool WorldPointInScreenBounds(this Vector3 position, Camera camera, int boundOffcet)
    {
        return PointInScreenBounds(camera.WorldToScreenPoint(position), camera, boundOffcet);
    }

    public static bool PointInScreenBounds(this Vector3 screenPosition, Camera camera, int boundOffcet) =>
        PointInScreenBounds((Vector2)screenPosition, camera, boundOffcet);
    public static bool PointInScreenBounds(this Vector2 screenPosition, Camera camera, int boundOffcet)
    {
        if (screenPosition.x < 0 - boundOffcet || screenPosition.x > camera.pixelWidth + boundOffcet) return false;
        if (screenPosition.y < 0 - boundOffcet || screenPosition.y > camera.pixelHeight + boundOffcet) return false;

        return true;
    }

    public static float LerpByStep(this float origin, float target, float step)
    {
        float targetOffcet = target - origin;
        float offcet = targetOffcet;

        if (Mathf.Abs(offcet) > step)
            offcet = offcet.Normalize() * step;

        return origin + offcet;
    }
}
