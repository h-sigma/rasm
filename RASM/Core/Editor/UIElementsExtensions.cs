using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GDG.Scripts.Patterns.FSM.Editor
{
    public static class UIElementsExtensions
    {
        public static VisualElement AddExtn(this VisualElement element, VisualElement other)
        {
            element.Add(other);
            return element;
        }

        public static void BuildInspectorProperties(SerializedObject obj, VisualElement container,
            Dictionary<string, Func<PropertyInfo, VisualElement>> customDrawers,
            Func<SerializedProperty, VisualElement> propContainer = null)
        {
            // TODO [Header()] and [Space()] are manually added until Unity supports them.

            SerializedProperty iterator   = obj.GetIterator();
            Type               targetType = obj.targetObject.GetType();
            List<MemberInfo>   members    = new List<MemberInfo>(targetType.GetMembers());

            if (!iterator.NextVisible(true)) return;
            do
            {
                PropertyField propertyField = new PropertyField()
                {
                    name = "PropertyField:" + iterator.propertyPath,
                };
                propertyField.BindProperty(iterator.Copy());

                MemberInfo member = members.Find(x => x.Name == propertyField.bindingPath);
                if (member != null)
                {
                    IEnumerable<Attribute> headers = member.GetCustomAttributes(typeof(HeaderAttribute));
                    IEnumerable<Attribute> spaces  = member.GetCustomAttributes(typeof(SpaceAttribute));

                    foreach (Attribute x in headers)
                    {
                        HeaderAttribute actual = (HeaderAttribute) x;
                        Label           header = new Label {text = actual.header};
                        header.style.unityFontStyleAndWeight = FontStyle.Bold;
                        container.Add(new Label {text = " ", name = "Header Spacer"});
                        container.Add(header);
                    }

                    foreach (Attribute unused in spaces)
                    {
                        container.Add(new Label {text = " "});
                    }
                }

                if (iterator.propertyPath == "m_Script" && obj.targetObject != null)
                {
                    continue;
                    //propertyField.SetEnabled(value: false);
                }

                if (propContainer != null)
                {
                    var holder = propContainer.Invoke(iterator);
                    holder.AddExtn(propertyField);
                    container.AddExtn(holder);
                }
                else
                {
                    container.Add(propertyField);
                }
            } while (iterator.NextVisible(false));
        }

        public static void AddClasses(VisualElement element, params string[] classes)
        {
            foreach (var @class in classes)
            {
                element.AddToClassList(@class);
            }
        }

        public static T AddClasses<T>(this T element, params string[] classes) where T : VisualElement
        {
            foreach (var @class in classes)
            {
                element.AddToClassList(@class);
            }

            return element;
        }
    }
}