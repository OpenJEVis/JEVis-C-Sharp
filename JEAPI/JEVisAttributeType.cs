using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisAttributeType : JEVisComponent, JEVisCommittable, IComparable<JEVisAttributeType> {

    /**
     * Returns the name of this type. The name is a unique identifier for a
     * type. The name does not have to be unique in the JEVis system but has to
     * be under an JEVisClass.
     *
     * @return
     * @throws JEVisException
     */
    String GetName();

    /**
     * Set the name for this type. The name is an unique identifier for an type.
     * The name does not have to be unique in the JEVis system but has to be
     * under an JEVisClass.
     *
     * @param name
     * @throws JEVisException
     */
    void SetName(String name);

    /**
     * Returns the primitive type.
     *
     * @see JEVisConstants
     * @return
     * @throws JEVisException
     */
    int GetPrimitiveType();

    /**
     * Set the primitive type.
     *
     * @see JEVisConstants
     * @param type
     * @throws JEVisException
     */
    void SetPrimitiveType(int type);

    /**
     * Returns the GUI display type. GUIs will use this type to display the
     * value, for example a String could be displayed as asterisk textfield or
     * clear text.
     *
     * @return
     * @throws JEVisException
     */
    String GetGUIDisplayType();

    /**
     * Set the GUI display type.
     *
     * @see JEVisConstants
     * @param type
     * @throws JEVisException
     */
    void SetGUIDisplayType(String type);

    /**
     * Set the order of the input field for this type in the GUI. The Fields
     * will be sorted from lowest-top to the highest-bottom.
     *
     * @param pos
     * @throws JEVisException
     */
    void SetGUIPosition(int pos);

    /**
     * Returns positions of this type in the GUI. The Fields will be sorted from
     * lowest-top to the highest-bottom.
     *
     * @return
     * @throws JEVisException
     */
    int GetGUIPosition();

    /**
     * returns the JEVisClass of this type.
     *
     * @return JEVisClass of this type
     * @throws JEVisException
     */
    JEVisClass GetJEVisClass();

    /**
     * Returns the JEVisClass name of this type.
     *
     * @return
     * @throws JEVisException
     */
    String getJEVisClassName();

    /**
     * Returns the validity. The validity tells the API how to handle die
     * timestamps. For example if only the last value is valid or if every
     * timestamp is valid at this time.
     *
     * @see JEVisConstants
     * @return validity of the sample
     * @throws JEVisException
     */
    int GetValidity();

    /**
     * Set the validity. The validity tells the API how to handle die
     * timestamps. For example if only the last value is valid or if every
     * timestamp is valid at this time.
     *
     * @see JEVisConstants
     * @param validity
     * @throws JEVisException
     */
    void SetValidity(int validity);

    /**
     * Return the additional configuration parameter.
     *
     * @deprecated
     * @return
     * @throws JEVisException
     * @deprecated This function is not in use and will be changed?!
     */
    String GetConfigurationValue();

    /**
     * Set the additional configuration parameter.
     *
     * @param value
     * @throws JEVisException
     * @deprecated This function is not in use and will be changed?!
     */
    void SetConfigurationValue(String value);

    /**
     * Set the expected unit for this type. All values of attributes from type
     * type will be stored as this unit in the JEVisDataSource.
     *
     * @param unit
     * @throws JEVisException
     */
    void SetUnit(JEVisUnit unit);

    /**
     * Return the expected unit for this type. All values of attributes from
     * type type will be stored as this unit in the JEVisDataSource.
     *
     * @return
     * @throws JEVisException
     */
    JEVisUnit GetUnit();

    /**
     * Get the alternative Symbol for the Unit of this type
     *
     * @return
     * @throws JEVisException
     */
    String GetAlternativeSymbol();

    /**
     * Set an alternative symbols for the unit of this type
     *
     * @param symbol
     * @throws JEVisException
     */
    void SetAlternativeSymbol(String symbol);

    /**
     * Returns the human description for the type. The function may be replaced
     * with a localized version.
     *
     * @deprecated
     * @return
     * @throws JEVisException
     */
    String GetDescription();

    /**
     * Set the human description for the type.
     *
     * @param discription
     * @throws JEVisException
     */
    void SetDescription(String discription);

    /**
     * Delete this type from the JEVisDataSource. This function does not need a
     * commit;
     *
     * @deprecated use JEVIsClass.DeleteType
     * @return
     * @throws JEVisException
     */
    bool Delete();

    /**
     * return true if this type is inherited from an other class
     *
     * @return
     * @throws JEVisException
     */
    bool IsInherited();

    /**
     * Set if this Type is inherited from an other class
     *
     * @param inherited
     * @throws JEVisException
     */
    void IsInherited(bool inherited);
}
}
