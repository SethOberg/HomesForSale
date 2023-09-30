using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace UtilitiesLibrary
{
    //Generic list class
    [Serializable]
    public abstract class ListManager<T> : IListManager<T>
    {
        private List<T> list;

        public ListManager()
        {
            list = new List<T>();
        }

        public int Count {
            get { return list.Count; }
        }

        public bool Add(T aType)
        {
            if(!list.Contains(aType))
            {
                list.Add(aType);
                return true;
            }
            
            return false;
        }

        public bool BinaryDeSerialize(string fileName)
        {
            bool sucessfull = false;
            list = BinarySerializer.BinaryDeSerializeFromFile<List<T>>(fileName);

            return sucessfull;
        }

        public bool BinarySerialize(string fileName)
        {
            bool sucessfull = false;

            sucessfull = BinarySerializer.BinarySerializeToFile(list, fileName);

            return sucessfull;
        }

        public bool ChangeAt(T aType, int anIndex)
        {
            if(list.Contains(aType))
            {
                list[anIndex] = aType;
                return true;
            }

            return false;
        }

        public bool CheckIndex(int index)
        {
            if(list.Count >= index)
            {
                return true;
            }

            return false;
        }

        public void DeleteAll()
        {
            list = new List<T>();
        }

        public bool DeleteAt(int anIndex)
        {
            if(list[anIndex] != null)
            {
                list.RemoveAt(anIndex);
                return true;
            }

            return false;
        }

        public T GetAt(int anIndex)
        {
            return list[anIndex];
        }

        public string[] ToStringArray()
        {
            string[] descriptions = new string[list.Count];

            for(int i = 0; i < list.Count; i++)
            {
                descriptions[i] = list[i].ToString();
            }

            return descriptions;
        }

        public List<string> ToStringList()
        {
            List<string> descriptions = new List<string>();

            for(int i = 0; i < list.Count; i++)
            {
                descriptions.Add(list[i].ToString());
            }

            return descriptions;
        }

        public bool XMLSerialize(string fileName)
        {
            bool successful = false;

            successful = SerializeWithXML.XMLSerialize(list, fileName);

            return successful;
        }

        public bool XMLDeSerialize(string fileName)
        {
            bool successful = false;
            
            list = SerializeWithXML.XMLDeSerializeFile<List<T>>(fileName);

            return successful;
        }
    }
}
