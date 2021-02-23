using JEAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisUserWS : JEVisUser
    {

        private String firstName { get; set; }
        private String lastName { get; set; }
        private bool isSysAdmin { get; set; }
        private JEVisDataSourceWS ds { get; set; }
        private JEVisObjectWS obj { get; set; }
        private bool enabled = false;
        private UserRightManager urm;
        /** List of classes which can be updated with special rules **/
        private List<String> executeUpdateExceptions = new List<String>(new String[] { "Data Notes", "User Data", "Clean Data" });
        public JEVisUserWS(JEVisDataSourceWS ds, JEVisObjectWS obj)
        {

            this.ds = ds;
            this.obj = obj;
            this.urm = new UserRightManager(ds, this);
            FetchData();
        }

        private void FetchData()
        {
            foreach (JEVisAttribute att in obj.GetAttributes())
            {
                switch (att.GetName())
                {
                    case JEVisConstants.Attribute.USER_SYS_ADMIN:
                        JEVisSample sysAdminAtt = att.GetLatestSample();
                        if (sysAdminAtt != null)
                        {
                            isSysAdmin = sysAdminAtt.GetValueAsBoolean();
                        }
                        else
                        {
                            isSysAdmin = false;
                        }

                        break;
                    case JEVisConstants.Attribute.USER_FIRST_NAME:
                        JEVisSample firstNameAtt = att.GetLatestSample();
                        if (firstNameAtt != null)
                        {
                            firstName = firstNameAtt.GetValueAsString();
                        }
                        else
                        {
                            firstName = "";
                        }
                        break;
                    case JEVisConstants.Attribute.USER_LAST_NAME:
                        JEVisSample lastNameAtt = att.GetLatestSample();
                        if (lastNameAtt != null)
                        {
                            lastName = lastNameAtt.GetValueAsString();
                        }
                        else
                        {
                            lastName = "";
                        }
                        break;
                    case JEVisConstants.Attribute.USER_ENABLED:
                        //can only be true in the moment because if not the user can not access this information for him self
                        JEVisSample enabledAtt = att.GetLatestSample();
                        if (enabledAtt != null)
                        {
                            enabled = enabledAtt.GetValueAsBoolean();
                        }
                        else
                        {
                            enabled = false;
                        }
                        break;
                }
            }
        }


        public bool isEnabled()
        {
            return enabled;
        }


        public String getFirstName()
        {
            return firstName;
        }


        public String getLastName()
        {
            return lastName;
        }


        public bool IsSysAdmin()
        {
            return isSysAdmin;
        }


        public long GetUserID()
        {
            return obj.getID();
        }


        public JEVisObject GetUserObject()
        {
            return obj;//or return ds.getObject....?
        }


        public String GetAccountName()
        {
            return obj.GetName();
        }


        public bool CanRead(long objectID)
        {
            return urm.canRead(objectID);
        }


        public bool CanWrite(long objectID)
        {

            try
            {
                bool canWrite = urm.canWrite(objectID);

                if (canWrite)
                {
                    return true;
                }
                else
                {
                    if (executeUpdateExceptions.Contains(ds.GetObject(objectID).getJEVisClassName()) && CanExecute(objectID))
                    {
                        return true;
                    }
                }
                return false;


            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return false;
        }


        public bool CanCreate(long objectID)
        {

            return urm.canCreate(objectID);
        }


        public bool CanCreate(long objectID, String jevisClass)
        {

            bool CanCreate = CanCreate(objectID);

            if (CanCreate)
            {
                return true;
            }
            else
            {
                if (executeUpdateExceptions.Contains(jevisClass) && CanExecute(objectID))
                {
                    return true;
                }
            }

            return false;
        }


        public bool CanExecute(long objectID)
        {
            return urm.canExecute(objectID);
        }


        public bool CanDelete(long objectID)
        {
            return urm.canDelete(objectID);
        }


        public void Reload()
        {
            urm.reload();
        }

    }
}
