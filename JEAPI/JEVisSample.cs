using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisSample : JEVisComponent, JEVisCommittable {

    /**
     * Returns the sample's timestamp
     *
     * @return JevCalendar timestamp
     * @throws org.jevis.api.JEVisException
     */
    DateTime GetTimestamp();

    /**
     * Returns the sample's value
     *
     * @return value of generic type T
     * @throws org.jevis.api.JEVisException
     */
    Object GetValue();

    /**
     * Returns a String representation of the value.
     *
     * @return
     * @throws JEVisException
     */
    String GetValueAsString();

    /**
     * Returns a long representation of the value.
     *
     * @return
     * @throws JEVisException
     */
    long GetValueAsLong();

    /**
     * Returns the value converted to the given unit.
     *
     * @param unit
     * @return
     * @throws JEVisException
     */
    long GetValueAsLong(JEVisUnit unit);

    /**
     * Returns a double representation of the value.
     *
     * @return
     * @throws JEVisException
     */
    Double GetValueAsDouble();

    /**
     * Returns the value converted to the given unit.
     *
     * @param unit
     * @return
     * @throws JEVisException
     */
    Double GetValueAsDouble(JEVisUnit unit);

    /**
     * Returns a boolean representation of the value.
     *
     * @return
     * @throws JEVisException
     */
    Boolean GetValueAsBoolean();

    /**
     * Returns a JEVisFile representation of this value.
     *
     * @return
     * @throws JEVisException
     */
    JEVisFile GetValueAsFile();

    /**
     * Returns a JEVisSelection representation of this value.
     *
     * @return
     * @throws JEVisException
     */
    JEVisSelection GetValueAsSelection();

    /**
     * Returns a JEVisMultiSelection representation of this sample.
     *
     * @return
     * @throws JEVisException
     */
    JEVisMultiSelection GetValueAsMultiSelection();

    /**
     * Set the value of this sample. The value has to be in the default unit of
     * the attribute
     *
     * @param value can be()
     * @throws org.jevis.api.JEVisException
     * @throws ClassCastException
     */
    void SetValue(Object value);

    /**
     * Set the value of this sample in the given unit. JEVisSample will try
     * to convert the value from the given unit to the set value for storage.
     *
     * @param value The value to set this sample to
     * @param unit The unit of the given value
     * @throws org.jevis.api.JEVisException
     */
    void SetValue(Object value, JEVisUnit unit);

    /**
     * Get the attribute for this sample.
     * 
     * @return
     * @throws org.jevis.api.JEVisException
     */
    JEVisAttribute SetAttribute();

    /**
     * Get the human readable note for this sample.
     *
     * @return The human readable note for this sample.
     * @throws org.jevis.api.JEVisException
     */
    String GetNote();

    /**
     * Set the human readable note for this sample
     *
     * @param note
     * @throws org.jevis.api.JEVisException
     */
    void SetNote(String note);

    /**
     * Get the unit of sample
     *
     * @return
     * @throws JEVisException
     */
    public JEVisUnit GetUnit();
}
}
