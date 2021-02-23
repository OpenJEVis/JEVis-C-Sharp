using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisCommittable
    {

        /**
         * Commit the changes to the DataSource
         *
         * @throws JEVisException
         */
        void Commit();

        /**
         * Rollback the changes until the last commit
         *
         * @deprecated
         * @throws JEVisException
         */
        void RollBack();

        /**
         * Returns if the Object has changed
         *
         * @deprecated
         * @return
         */
        bool HasChanged();
    }
}
