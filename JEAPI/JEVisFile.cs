using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JEAPI
{
    public interface JEVisFile
    {
        void saveToFile(File file);

        void loadFromFile(File file);

        void setBytes(byte[] data);

        byte[] getBytes();

        String getFilename();

        void setFilename(String name);

        String getFileExtension();
        //TODO: ? is encoding come object?
        //    void setEncoding(String encoding);
        //    String getEncoding();
    }
}
