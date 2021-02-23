using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisSelection
    {

        JEVisClass getFilteredClass();

        List<JEVisObject> getSelectableObjects();

        //TODO: ?implement multiselect?
        JEVisObject getSelectedObject();

        void setSelectedObject(JEVisObject jEVisObject);
    }
}
