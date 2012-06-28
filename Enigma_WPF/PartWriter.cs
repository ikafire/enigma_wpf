using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using EnigmaWPF.Model;

namespace EnigmaWPF
{
    internal class PartWriter
    {
        private PartCollection parts;

        public PartWriter(PartCollection parts)
        {
            List<Rotor> cloneRots = new List<Rotor>();
            parts.Rotors.ForEach(rot => cloneRots.Add(new Rotor(rot)));
            cloneRots.ForEach(rot => rot.Name += "(" + DateTime.Now.ToString("ddMMyy") + "Exported)");
            Reflector cloneRef = new Reflector(parts.Reflector);
            cloneRef.Name += "(" + DateTime.Now.ToString("ddMMyy") + "Exported)";
            this.parts = new PartCollection
            {
                PlugBoard = parts.PlugBoard,
                Rotors = cloneRots,
                Reflector = cloneRef
            };
        }

        public void WriteTo(string fileName)
        {
            Stream streamWrite = null;
            try
            {
                streamWrite = File.Create(fileName);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(streamWrite, this.parts);
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (streamWrite != null)
                {
                    streamWrite.Close();
                }
            }
        }
    }
}
