#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using RASM.ThirdParty.SerializeReferenceUI.Core;
using UnityEditor;
using UnityEngine;

public static class SerializeReferenceInspectorButton
{
    public static readonly Color SerializedReferenceMenuBackgroundColor = new Color(0.1f, 0.55f, 0.9f, 1f);

    /// Must be drawn before DefaultProperty in order to receive input
    public static void DrawSelectionButtonForManagedReference(this SerializedProperty property, Rect position,
        IEnumerable<Func<Type, bool>>                                                 filters = null) =>
        property.DrawSelectionButtonForManagedReference(position, SerializedReferenceMenuBackgroundColor, filters);

    /// Must be drawn before DefaultProperty in order to receive input
    public static void DrawSelectionButtonForManagedReference(this SerializedProperty property,
        Rect position, Color color, IEnumerable<Func<Type, bool>> filters = null)
    {
        var backgroundColor = color;

        var buttonPosition = position;
        buttonPosition.x += EditorGUIUtility.labelWidth + 1 * EditorGUIUtility.standardVerticalSpacing;
        buttonPosition.width =
            position.width - EditorGUIUtility.labelWidth - 1 * EditorGUIUtility.standardVerticalSpacing;
        buttonPosition.height = EditorGUIUtility.singleLineHeight;

        var storedIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        var storedColor = GUI.backgroundColor;
        GUI.backgroundColor = backgroundColor;


        var names = ManagedReferenceUtility.GetSplitNamesFromTypename(property.managedReferenceFullTypename);
        if (!Event.current.alt)
        {
            names.ClassName    = names.ClassName.Substring(names.ClassName.LastIndexOf('.')       + 1);
            names.AssemblyName = names.AssemblyName.Substring(names.AssemblyName.LastIndexOf('.') + 1);
        }

        var className    = string.IsNullOrEmpty(names.ClassName) ? "Null (Assign)" : names.ClassName;
        var assemblyName = names.AssemblyName;
        var tooltip      = (Event.current.alt ? className + $" ( {assemblyName} )" : className);
        if (GUI.Button(buttonPosition, new GUIContent(className, tooltip)))
            property.ShowContextMenuForManagedReference(filters);

        GUI.backgroundColor   = storedColor;
        EditorGUI.indentLevel = storedIndent;
    }
}

#endif