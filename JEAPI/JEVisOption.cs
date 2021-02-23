using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisOption
    {

        /**
         * Get a list with all children options
         *
         * @return
         */
        List<JEVisOption> getOptions();

        /**
         * returns a single option by name
         *
         * @param optionName
         * @return
         */
        JEVisOption getOption(String optionName);

        /**
         * returns if this option has a child with the given name,
         *
         * @param optionName
         * @return true if the option exists, false if not
         */
        bool hasOption(String optionName);

        /**
         * Add a new child option to this option.
         *
         * @param option new child option
         * @param overwrite if true overwrite the already existing option.
         */
        void addOption(JEVisOption option, bool overwrite);

        /**
         * Remove and option from this option.
         *
         * @param option
         */
        void removeOption(JEVisOption option);

        /**
         * returns the value for this option
         *
         * @return
         */
        String getValue();

        /**
         * Set the value for this option
         *
         * @param value
         */
        void setValue(String value);

        /**
         * return the key/name of this option
         *
         * @return
         */
        String getKey();

        /**
         * Set the key of this option
         *
         * @TODO: maybe this function is not save because the parent cannot check if
         * the open is already in use. Better use the constructor and add to check
         * this
         * @param key
         */
        void setKey(String key);

        /**
         * Returns an human readable descripion
         *
         * @return
         */
        String getDescription();

        /**
         * Set the human readable description
         *
         * @param description
         */
        void setDescription(String description);



    }
}
