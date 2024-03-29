using CustomInspector.Extensions;
using CustomInspector.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace CustomInspector.Editor
{
    [CustomPropertyDrawer(typeof(TabAttribute))]
    public class TabAttributeDrawer : PropertyDrawer
    {
        /// <summary> Distance between outer rect and inner rects </summary>
        const float outerSpacing = 15;
        const float toolbarHeight = 25;




        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PropInfo info = GetInfo(property);

            if(info.errorMessage != null)
            {
                DrawProperties.DrawPropertyWithMessage(position, label, property, info.errorMessage, MessageType.Error);
                return;
            }

            bool isVisible = info.groupIndex == GetSelected(fieldInfo.DeclaringType);

            //clip not seen
            if (!isVisible && !info.isAllFirst)
                return;

            using (new NewIndentLevel(0))
            {

                //Background
                position.height = GetPropertyHeight(property, label);

                float halfSpacing = EditorGUIUtility.standardVerticalSpacing / 2f;
                if (!info.isAllFirst)
                {
                    //mit oberen verbinden
                    position.y -= halfSpacing;
                    position.height += halfSpacing;
                }


                //mit unterem verbinden
                if ((info.isAllFirst && !isVisible) || !info.isGroupLast) //allFirst (the toolbar) has to connect all
                    position.height += halfSpacing;

                EditorGUI.DrawRect(position, ((TabAttribute)attribute).backgroundColor.ToColor());

                //verbindung oben ende
                if (!info.isAllFirst)
                    position.y += halfSpacing;

                //abstand zu oben
                if (info.isAllFirst)
                    position.y += EditorGUIUtility.standardVerticalSpacing;

                //sides distance
                position = ExpandRectWidth(position, -outerSpacing);
                //Toolbar
                if (info.isAllFirst)
                {
                    Rect tRect = new(position)
                    {
                        height = toolbarHeight
                    };
                    GUIContent[] guiContents = GetGroupNames(fieldInfo.DeclaringType);
                    EditorGUI.BeginChangeCheck();
                    int res = GUI.Toolbar(tRect, GetSelected(fieldInfo.DeclaringType), guiContents);
                    if (EditorGUI.EndChangeCheck())
                        SetSelected(fieldInfo.DeclaringType, res);

                    position.y = tRect.y + toolbarHeight + outerSpacing + EditorGUIUtility.standardVerticalSpacing;
                }
                //Draw Property
                if (isVisible)
                {
                    position.height = DrawProperties.GetPropertyHeight(label, property);
                    DrawProperties.PropertyField(position, label, property);
                }
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            PropInfo info = GetInfo(property);

            if (info.errorMessage != null)
                return DrawProperties.GetPropertyWithMessageHeight(label, property);

            bool isVisible = info.groupIndex == GetSelected(fieldInfo.DeclaringType);

            float totalHeight = isVisible ?
                        DrawProperties.GetPropertyHeight(label, property) : -EditorGUIUtility.standardVerticalSpacing;

            if (info.isAllFirst)
            {
                totalHeight += EditorGUIUtility.standardVerticalSpacing
                    + toolbarHeight
                    + outerSpacing
                    + EditorGUIUtility.standardVerticalSpacing;
            }
            if (info.isGroupLast && isVisible)
                totalHeight += outerSpacing;
            return totalHeight;
        }

        Rect ExpandRectWidth(Rect rect, float value)
        {
            rect.x -= value;
            rect.width += 2 * value;
            return rect;
        }


        /// <summary> all group names for each declaringType </summary>
        readonly static Dictionary<Type, GUIContent[]> groupNames = new();
        static GUIContent[] GetGroupNames(Type t)
        {
            if(!groupNames.TryGetValue(t, out GUIContent[] res))
            {
                var names = FindGroupNames(t).ToArray();
                res = names.Select(_ => StylesConvert.ToInternalIconName(_))
                           .Select(_ => InternalEditorStylesConvert.IconNameToGUIContent(_))
                           .ToArray();

                groupNames.Add(t, res);
            }
            return res;
        }
        static IEnumerable<string> FindGroupNames(Type classType)
        {
            IEnumerable<FieldInfo> fields = classType.GetAllSerializableFields(alsoFromBases: false);
            IEnumerable<TabAttribute> allAttr = fields.Select(_ => _.GetCustomAttribute<TabAttribute>()).Where(_ => _ is not null);
            return allAttr.Select(_ => _.groupName).Distinct();
        }


        ///<summary>DeclaringType to selected group</summary>
        readonly static Dictionary<Type, int> selected = new();
        static int GetSelected(Type classType)
        {
            if (selected.TryGetValue(classType, out int value))
                return value;
            else
                return 0;
        }
        void SetSelected(Type classType, int value)
        {
            if (!selected.TryAdd(classType, value))
                selected[classType] = value;
        }
        readonly static Dictionary<PropertyIdentifier, PropInfo> savedInfos = new();

        PropInfo GetInfo(SerializedProperty property)
        {
            PropertyIdentifier id = new(property);
            if (!savedInfos.TryGetValue(id, out PropInfo res))
            {
                res = new PropInfo(fieldInfo, (TabAttribute)attribute);
                savedInfos.Add(id, res);
            }
            return res;
        }
        class PropInfo
        {
            public int groupIndex;
            /// <summary> Defines, if it has to draw the toolbar </summary>
            public bool isAllFirst;
            /// <summary> Defines, if it has to draw space on the end </summary>
            public bool isGroupLast;
            public string errorMessage = null;

            public PropInfo(FieldInfo fieldInfo, TabAttribute attribute)
            {
                //In array nothing matters - just error it
                if (typeof(IEnumerable).IsAssignableFrom(fieldInfo.FieldType))
                {
                    errorMessage = "TabAttribute not valid on list elements." +
                        "\nHint: Put your list in a (holder-)class, give it this attribute and UnwrapAttribute to assign the entire list to a tab";
                    return;
                }

                //-
                (isAllFirst, isGroupLast) = GetMyPosition(fieldInfo, attribute);
                groupIndex = FindGroupNames(fieldInfo.DeclaringType).ToList().IndexOf(attribute.groupName);
                Debug.Assert(groupIndex != -1, $"No group for {fieldInfo.Name} found");


                static (bool isAllFirst, bool isGroupLast) GetMyPosition(FieldInfo fieldInfo, TabAttribute attribute)
                {
                    bool isAllFirst = false;
                    bool isGroupLast = false;

                    var allProps = fieldInfo.DeclaringType.GetAllSerializableFields(alsoFromBases: false)
                        .Select(f => (fieldInfo: f, attr: f.GetCustomAttribute<TabAttribute>()))
                        .Where(ta => ta.attr != null);

                    (FieldInfo fieldInfo, TabAttribute ta) first = allProps.First();
                    if (first.fieldInfo == fieldInfo)
                        isAllFirst = true;

                    var propsInMyGroup = allProps.Where(_ => _.attr.groupName == attribute.groupName);

                    if (!propsInMyGroup.Any())
                    {
                        Debug.LogError($"No properties found for Tab-group '{attribute.groupName}' in '{fieldInfo.DeclaringType}'" +
                                       $"\nError at displaying {fieldInfo.Name}");
                        return (false, false);
                    }
                    var last_iG = propsInMyGroup.Last();
                    if (last_iG.fieldInfo == fieldInfo)
                        isGroupLast = true;

                    return (isAllFirst, isGroupLast);
                }
            }
        }
    }
}