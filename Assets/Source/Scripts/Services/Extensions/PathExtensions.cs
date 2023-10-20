using UnityEngine;

public static class PathExtensions
{
    public static Vector3[] CreateFlyPath(this Vector3 startPosition, Vector3 elementFinalPosition, float height)
    {
        return new Vector3[]
        {
            (startPosition + elementFinalPosition) / 2f + Vector3.up * height,
            elementFinalPosition
        };
    }

    public static Vector3[] CreateLongFlyPath(this Vector3 startPosition, Vector3 elementFinalPosition, float height,
        Vector3 upPointOffcet = new Vector3())
    {
        Vector3 moveDirection = (elementFinalPosition - startPosition).normalized;

        return new Vector3[]
        {
            startPosition + Vector3.up * height * 0.7f - moveDirection + upPointOffcet * 0.7f,
            (startPosition + elementFinalPosition) / 2f + Vector3.up * height + upPointOffcet,
            elementFinalPosition + Vector3.up * height * 0.7f + moveDirection + upPointOffcet * 0.7f,
            elementFinalPosition
        };
    }

    //public static Vector3[] CreateFlyPath(this Transform transform, Vector3 elementFinalPosition, float height) =>
    //    CreateFlyPath(transform.position, elementFinalPosition, height);

    public static Vector3[] CreateStaticPath(this Transform pathParent)
    {
        Vector3[] path = new Vector3[pathParent.childCount];

        for (int i = 0; i < path.Length; i++)
            path[i] = pathParent.GetChild(i).position;

        return path;
    }
}
