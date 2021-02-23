using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace JEAPI_WS
{
    public class ClassIconHandler
    {

        private List<String> files = new List<String>();
        private bool fileExists = false;
        private String tmpDir;
        private static ILog logger = LogManager.GetLogger(typeof(ClassIconHandler));
        private String zipFile;

        public ClassIconHandler(String tmpDir)
        {
            this.tmpDir = tmpDir;

            zipFile = tmpDir + "/classicons.zip";
            fileExists = File.Exists(zipFile);

            logger.Debug(string.Format("zipFile: {0}   {1}", zipFile, File.Exists(zipFile)));
        }

        public void readStream(InputStream input)
        {
            logger.Info("ReadStream");
            tmpDir.mkdirs();

            OutputStream outputStream = new FileOutputStream(zipFile);

            int read = 0;
            byte[] bytes = new byte[1024];

            while ((read = input.read(bytes)) != -1)
            {
                outputStream.write(bytes, 0, read);
            }
            unZipIt(tmpDir, zipFile);

        }

        public bool FileExists()
        {
            return fileExists;
        }

        public Dictionary<String, Image> getClassIcon()
        {
            Dictionary<String, Image> map = new Dictionary<string, Image>();
            foreach (String fileName in Directory.GetFiles(tmpDir))
            {
                if (fileName.Contains(".png"))
                {
                    map.Add(fileName.Replace(".png", ""), Image.FromFile(fileName));
                }
            }
            return map;
        }

        public Image getClassIcon(String name)
        {
            foreach (string fileName in Directory.GetFiles(tmpDir))
            {
                if (fileName.Equals(name + ".png"))
                {
                    return Image.FromFile(fileName);
                }
            }
            return null;
        }

        public void unZipIt(File folder, File zipFile)
        {
            //        List<File> files = new ArrayList<>();

            byte[] buffer = new byte[1024];

            try
            {

                //create output directory is not exists
                if (!folder.exists())
                {
                    folder.mkdir();
                }
                folder.deleteOnExit();

                //get the zip file content
                ZipInputStream zis
                        = new ZipInputStream(new FileInputStream(zipFile));
                //get the zipped file list entry
                ZipEntry ze = zis.getNextEntry();

                while (ze != null)
                {

                    String fileName = ze.getName();
                    File newFile = new File(folder + File.separator + fileName);
                    newFile.deleteOnExit();
                    //                files.add(newFile);

                    //                logger.info("file unzip : " + newFile.getAbsoluteFile());
                    //create all non exists folders
                    //else you will hit FileNotFoundException for compressed folder
                    new File(newFile.getParent()).mkdirs();

                    FileOutputStream fos = new FileOutputStream(newFile);

                    int len;
                    while ((len = zis.read(buffer)) > 0)
                    {
                        fos.write(buffer, 0, len);
                    }

                    //                files.add(newFile);
                    fos.close();
                    ze = zis.getNextEntry();
                }

                zis.closeEntry();
                zis.close();

            }
            catch (IOException ex)
            {
                ex.printStackTrace();
            }
        }

    }
}
