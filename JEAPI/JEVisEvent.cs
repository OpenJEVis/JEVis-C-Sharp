using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public class JEVisEvent : EventArgs
    {

    private TYPE type;
    private Object eventObject;

    public JEVisEvent(Object source, TYPE type, Object eventObject)
    {
        this.type = type;
        this.eventObject = eventObject;
    }

    public TYPE getType()
    {
        return type;
    }

    public Object getObject()
    {
        return eventObject;
    }

    public enum TYPE
    {
        OBJECT_BUILD_CHILD, OBJECT_NEW_CHILD, OBJECT_DELETE, OBJECT_CHILD_DELETED, OBJECT_UPDATE, OBJECT_UPDATED, OBJECT_MOVED, OBJECT_COPIED, OBJECT_LINKED,
        CLASS_DELETE, CLASS_CHILD_DELETE, CLASS_DELETE_TYPE, CLASS_BUILD_CHILD, CLASS_UPDATE, ATTRIBUTE_UPDATE
    }
}
}