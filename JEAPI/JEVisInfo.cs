using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisInfo
    {

        /**
         * Returns the version of the JEAPI Implementation
         *
         * @return
         */
        public String getVersion();

        /**
         * Returns the name of the JEAPI implementation
         *
         * @return
         */
        public String getName();

    }
}
