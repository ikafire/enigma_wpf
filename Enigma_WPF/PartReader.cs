using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using EnigmaWPF.Model;

namespace EnigmaWPF
{
    internal class PartReader
    {
        private PartCollection parts = null;

        public PartCollection ReadFromFile(string fileName)
        {
            Stream streamRead = null;
            try
            {
                streamRead = File.OpenRead(fileName);
                BinaryFormatter formatter = new BinaryFormatter();
                this.parts = (PartCollection)formatter.Deserialize(streamRead);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (streamRead != null)
                {
                    streamRead.Close();
                }
            }

            return this.parts;
        }
    }
}
