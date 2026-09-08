using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Material _runtimeMaterial;

    /// <summary>Creates the line renderer used to display the range in the Game view.</summary>
    private void Awake()
    {
        EnsureRenderer();
    }

    private void EnsureRenderer()
    {
        if (_lineRenderer != null)
        {
            return;
        }

        GameObject visualizerObject = new GameObject("Range Visualizer");
        visualizerObject.transform.SetParent(transform);

        _lineRenderer = visualizerObject.AddComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _runtimeMaterial = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.material = _runtimeMaterial;
    }

    /// <summary>Updates the displayed circle using the supplied range settings.</summary>
    public void Configure(float range, Color color, int segments)
    {
        EnsureRenderer();

        int safeSegmentCount = Mathf.Max(3, segments);
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        _lineRenderer.positionCount = safeSegmentCount;

        for (int i = 0; i < safeSegmentCount; i++)
        {
            float angle = i * Mathf.PI * 2f / safeSegmentCount;
            _lineRenderer.SetPosition(i, new Vector3(Mathf.Cos(angle) * range, 0f, Mathf.Sin(angle) * range));
        }
    }

    /// <summary>Releases the material created specifically for the runtime visualizer.</summary>
    private void OnDestroy()
    {
        if (_runtimeMaterial != null)
        {
            Destroy(_runtimeMaterial);
        }
    }
}
