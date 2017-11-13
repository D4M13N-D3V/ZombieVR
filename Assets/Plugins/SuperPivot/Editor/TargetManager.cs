using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace SuperPivot
{
    internal class TargetManager
    {
        public bool isEmpty { get { return m_Targets.Count == 0; } }
        public bool isUnique { get { return m_Targets.Count == 1; } }
        public TargetWrapper uniqueTarget { get { Debug.Assert(isUnique); return m_Targets[0]; } }
        public int count { get { return m_Targets.Count; } }
        public string errorMsg { get; private set; }

        List<TargetWrapper> m_Targets = new List<TargetWrapper>();
        static GUIContent ms_InvalidBoundsGUIContent = new GUIContent(
            "The bounds of this group are not valid.",
            "This object has no Renderer attached, so we cannot compute his bounds.");

        public IEnumerable<TargetWrapper> EveryTarget()
        {
            foreach (var t in m_Targets)
                yield return t;
        }

        public Transform[] GetTransformTargets()
        {
            var transforms = new Transform[m_Targets.Count];
            for (int i = 0; i < transforms.Length; i++)
                transforms[i] = m_Targets[i].transform;
            return transforms;
        }

        public void SetTargets(Transform[] selectedTransforms)
        {
            m_Targets.Clear();
            errorMsg = null;

            if (selectedTransforms != null)
            {
                foreach (var t in selectedTransforms)
                {
                    string localError = null;
                    if (API.CanChangePivot(t, out localError))
                    {
                        m_Targets.Add(new TargetWrapper(t));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(errorMsg) && localError != null)
                            errorMsg = localError;
                    }
                }
            }
        }

        public bool canModifyBoundsSliders
        {
            get
            {
                foreach (var t in m_Targets)
                    if (t.canModifyBoundsSliders)
                        return true;
                return false;
            }
        }

        Vector3 AverageInverseLerpBoundsPosition()
        {
            var avgpos = Vector3.zero;
            var count = 0;
            foreach (var t in m_Targets)
            {
                if (t.canModifyBoundsSliders)
                {
                    avgpos += t.InverseLerpBoundsPosition();
                    count++;
                }
            }

            return (count > 0) ? (avgpos / count) : Vector3.zero;
        }

        public void GUIBounds()
        {
            EditorGUILayout.Separator();
            {
                EditorGUILayout.LabelField("Bounds", EditorStyles.boldLabel);

                if (canModifyBoundsSliders)
                {
                    var avgSliderValue = AverageInverseLerpBoundsPosition();
                    GUISliderBounds(TargetWrapper.Component.X, "X", avgSliderValue.x);
                    GUISliderBounds(TargetWrapper.Component.Y, "Y", avgSliderValue.y);
                    GUISliderBounds(TargetWrapper.Component.Z, "Z", avgSliderValue.z);

                    if (GUILayout.Button("Bounds center"))
                        SetPivotLerpBoundsPosition(Vector3.one * 0.5f);
                }
                else
                {
                    EditorGUILayout.LabelField(ms_InvalidBoundsGUIContent);
                }
            }
        }

        void GUISliderBounds(TargetWrapper.Component comp, string label, float value)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.LabelField(label, GUILayout.Width(15f));
                value = EditorGUILayout.Slider(value, 0f, 1f);

                var buttonStyle = GUILayout.Width(20f);
                if (GUILayout.Button("<", buttonStyle)) value = 0.0f;
                if (GUILayout.Button("|", buttonStyle)) value = 0.5f;
                if (GUILayout.Button(">", buttonStyle)) value = 1.0f;

                if (EditorGUI.EndChangeCheck())
                    SetPivotLerpBoundsPosition(comp, value);
            }
        }

        void SetPivotLerpBoundsPosition(Vector3 sliderClampedValue)
        {
            foreach (var t in m_Targets)
                t.SetPivot(t.LerpBoundsPosition(sliderClampedValue), API.Space.Global);
        }

        void SetPivotLerpBoundsPosition(TargetWrapper.Component comp, float sliderClampedValue)
        {
            foreach (var t in m_Targets)
                t.SetPivot(comp, t.LerpBoundsPosition(comp, sliderClampedValue), API.Space.Global);
        }
    }
}