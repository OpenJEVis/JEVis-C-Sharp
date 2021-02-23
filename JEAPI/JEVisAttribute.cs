using System;
using System.Collections.Generic;

namespace JEAPI
{

    public interface JEVisAttribute : JEVisComponent, JEVisCommittable, IComparable<JEVisAttribute> {

        /**
         * Get the Name of the attribute. The name is unique under this JEVisObject
         *
         * @return
         */
        String GetName();

        /**
         * Delete this object and remove all references to it.
         *
         * @deprecated
         * @return
         */
        bool Delete();

        /**
         * Get the JEVisType of this attribute
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisAttributeType GetType();

        /**
         * Get the JEVisObject this attribute belongs to
         *
         * @return
         */
        JEVisObject GetObject();

        /**
         * Get the Object ID
         *
         * @return
         */
        long GetObjectID();

        /**
         * Get all samples the attribute may hold
         *
         * @return
         */
        List<JEVisSample> GetAllSamples();

        /**
         * Get all samples from ">=" to "<=" a certain date
         *
         * @param from (>=)
         * @param to   (<=)
         * @return
         */
        List<JEVisSample> GetSamples(DateTime from, DateTime to);

        /**
         * Get all samples from ">=" to "<=" a certain date in defined aggregation and manipulation
         *
         * @param from              (>=)
         * @param to                (<=)
         * @param customWorkDay     custom work day parameter
         * @param aggregationPeriod aggregation period
         * @param manipulationMode  manipulation mode
         * @return
         */
        List<JEVisSample> GetSamples(DateTime from, DateTime to, bool customWorkDay, String aggregationPeriod, String manipulationMode);

        /**
         * Add and commit all samples
         *
         * @param samples
         * @return
         * @throws JEVisException
         */
        int AddSamples(List<JEVisSample> samples);

        /**
         * Create a new JEViSample for this attribute with the input unit.
         *
         * @param ts Timestamp of the sample, null if now()
         * @param value
         * @return
         * @throws JEVisException
         */
        JEVisSample BuildSample(DateTime ts, Object value);

        /**
         * Create a new JEViSample for this attribute in the given unit.
         *
         * @param ts of the sample, null if now()
         * @param value
         * @param unit
         * @return
         * @throws JEVisException
         */
        JEVisSample BuildSample(DateTime ts, double value, JEVisUnit unit);

        /**
         * Create an new JEViSample for this attribute with a note.
         *
         * @param ts of the sample, null if now()
         * @param value
         * @param note
         * @return
         * @throws JEVisException
         */
        JEVisSample BuildSample(DateTime ts, Object value, String note);

        /**
         * Create an new JEViSample for this attribute with a note in the the given
         * unit.
         *
         * @param ts of the sample, null if now()
         * @param value
         * @param note
         * @param unit
         * @return
         * @throws JEVisException
         */
        JEVisSample BuildSample(DateTime ts, double value, String note, JEVisUnit unit);

        /**
         * Get the latest sample by date
         *
         * @return
         */
        JEVisSample GetLatestSample();

        /**
         * Get the primitive type of the samples
         *
         * @throws org.jevis.api.JEVisException
         * @see org.jevis.jeapi.JEVisConstants.PrimitiveType
         *
         * @return
         */
        int GetPrimitiveType();

        /**
         * Returns true if the attribute holds any samples
         *
         * @return
         */
        bool HasSample();

        /**
         * Get the timestamp from the first sample of the attribute
         *
         * @return
         */
        DateTime GetTimestampFromFirstSample();

        /**
         * Get the last timestamp of the attribute
         *
         * @return
         */
        DateTime GetTimestampFromLastSample();

        /**
         * Delete all samples this attribute may holds
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool DeleteAllSamples();

        /**
         * Deletes all samples from ">=" to "<=" a certain date
         *
         * @param from (>=)
         * @param to (<=)
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool DeleteSamplesBetween(DateTime from, DateTime to);

        /**
         * Returns the displayed unit of this attribute.
         *
         * @return
         * @throws JEVisException
         */
        JEVisUnit GetDisplayUnit();

        /**
         * Set the displayed unit of this attribute.
         *
         * @param unit
         * @throws JEVisException
         */
        void SetDisplayUnit(JEVisUnit unit);

        /**
         * Returns the unit in which the data is stored in the datasource
         *
         * @return @throws JEVisException
         */
        JEVisUnit GetInputUnit();

        /**
         * Set the Unit in which the data will be stored in the data-source
         *
         * @param unit
         * @throws JEVisException
         */
        void SetInputUnit(JEVisUnit unit);

        /**
         * Returns the default sample rate for the end-user representation
         *
         * @return
         */
        TimeSpan GetDisplaySampleRate();

        /**
         * returns the sample rate in which the data is stored in the data-source
         *
         * @return
         */
        TimeSpan GetInputSampleRate();

        /**
         * Set the sample rate for in which the data is stored in the data-source
         *
         * @param period
         */
        void SetInputSampleRate(TimeSpan period);

        /**
         * default sample rate for the end-user representation
         *
         * @param period
         */
        void SetDisplaySampleRate(TimeSpan period);

        /**
         * Check if the attribute is from the given JEVisType
         *
         * @param type the type to check for
         * @return
         */
        bool IsType(JEVisAttributeType type);

        /**
         * Get the count of all samples allocated to this attribute
         *
         * @return
         */
        long GetSampleCount();

        /**
         * Get all additonal options for this attribute.
         *
         * @return list of all options
         */
        List<JEVisOption> GetOptions();

        /**
         * Add an new option to this attribute. Will overwrite an existion option
         * wthe the same name
         *
         * @deprecated
         * @param option
         */
        void AddOption(JEVisOption option);

        /**
         * Remove an option from this attribute.
         *
         * @deprecated
         * @param option
         */
        void RemoveOption(JEVisOption option);

    }
}