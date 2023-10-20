using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static T[] FindObjectsOfTypeInChildren<T>(this Transform parent) where T : UnityEngine.Object
    {
        List<T> objects = new List<T>();
        if (parent.TryGetComponent(out T component)) objects.Add(component);
        for (int i = 0; i < parent.childCount; i++) objects.AddRange(FindObjectsOfTypeInChildren<T>(parent.GetChild(i)));
        return objects.ToArray();
    }

    public static GameObject[] FindGameObjectsWithNameInChildren(this Transform parent, string name)
    {
        List<GameObject> objects = new List<GameObject>();
        if (parent.gameObject.name == name) 
            objects.Add(parent.gameObject);
        for (int i = 0; i < parent.childCount; i++) 
            objects.AddRange(FindGameObjectsWithNameInChildren(parent.GetChild(i), name));
        return objects.ToArray();
    }

    public static T GetNearestObject<T>(IEnumerable<T> objects, Vector3 position) where T : Component
    {
        T targetObject = null;

        float minDistance = 10000f;
        foreach (T @object in objects)
        {
            float distance = Vector3.Distance(@object.transform.position, position);

            if (distance < minDistance)
            {
                targetObject = @object;
                minDistance = distance;
            }
        }

        return targetObject;
    }

    public static Quaternion GetScreenRotationFromGlobalDirection(Vector3 playerPosition, Vector3 targetPosition, Camera camera)
    {
        targetPosition = playerPosition + (targetPosition - playerPosition).normalized;
        Vector2 screenDirection = camera.WorldToScreenPoint(targetPosition) - camera.WorldToScreenPoint(playerPosition);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, screenDirection.normalized);

        return rotation;
    }

    public static float GetMagnitudeFromRotation(this float zAngle, Camera camera) =>
        new Vector2(Mathf.Sin(Mathf.Deg2Rad * zAngle) * camera.pixelWidth / 2, Mathf.Cos(Mathf.Deg2Rad * zAngle) * camera.pixelHeight / 2).magnitude;


    //public static void WorldDirectionToScreen(Vector3 startPosition, Vector3 targetDirection, Camera camera, 
    //    out Vector2 screenPosition, out Quaternion screenRotation, float boundsOffcet)
    //{
    //    Vector2 screenDirection = camera.WorldToScreenPoint(startPosition + (targetDirection - startPosition).normalized)
    //        - camera.WorldToScreenPoint(startPosition);

    //    screenPosition = (screenDirection)

    //}


    public static Vector3 GetGlobalPosition(this Transform transform)
    {
        if (transform.parent == null)
            return transform.localPosition;
        else return (transform.parent.localRotation * transform.localPosition) + transform.parent.GetGlobalPosition();
    }

    public static Quaternion GetGlobalRotation(this Transform transform)
    {
        if (transform.parent == null)
            return transform.localRotation;
        else return transform.parent.GetGlobalRotation() * transform.localRotation;
    }

    public static Quaternion GetGlobalRotation(this Transform transform, Quaternion localRotation)
    {
        if (transform.parent == null)
            return localRotation;
        else return transform.parent.rotation * localRotation;
    }

    public static Vector3 GetGlobalScale(this Transform transform)
    {
        Vector3 scale = transform.rotation * transform.localScale;

        if (transform.parent == null)
            return scale.Abs();
        else return Vector3.Scale(scale.Abs(), transform.parent.GetGlobalScale());
    }

    public static Quaternion LocalToWorldRotation(this Quaternion localRotation, Transform parent)
    {
        Stack<Quaternion> parentRotations = new Stack<Quaternion>();

        while (parent != null)
        {
            parentRotations.Push(parent.localRotation);
            parent = parent.parent;
        }

        Quaternion result = parentRotations.Pop();

        for (int i = 0; i < parentRotations.Count; i++)
            result *= parentRotations.Pop();

        return result * localRotation;
    }

    public static Pose LocalToWorldPose(this Pose localPose, Transform parent)
    {
        return new Pose(parent.TransformPoint(localPose.position), LocalToWorldRotation(localPose.rotation, parent));
    }
}
