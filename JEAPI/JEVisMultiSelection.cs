using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisMultiSelection
    {

        JEVisClass getFilteredClass();

        List<JEVisObject> getSelectableObjects();

        //TODO: ?implement mulyselect?
        List<JEVisObject> getSelectedObjects();

        void setSelectedObject(List<JEVisObject> objects);
    }
}
