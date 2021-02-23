using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisUser
    {

        bool isEnabled();

        String getFirstName();

        String getLastName();

        bool IsSysAdmin();

        long GetUserID();

        JEVisObject GetUserObject();

        String GetAccountName();

        bool CanRead(long objectID);

        bool CanWrite(long objectID);

        bool CanCreate(long objectID);

        bool CanCreate(long objectID, String jevisClass);

        bool CanExecute(long objectID);

        bool CanDelete(long objectID);

        void Reload();


        //    public boolean canDeleteClass(String jclass);
    }
}
