using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UtilitiesLibrary;

namespace HomesForSaleBLL
{
    [Serializable]
    public class EstateManager : ListManager<Estate>
    {

        public EstateManager()
        {
            
        }

        public void removeAt(int index)
        {
            DeleteAt(index);
        }

        public void addEstate(Estate estate)
        {
            Add(estate);
        }

        public bool indexExists(int index)
        {
           return CheckIndex(index);
        }

        public Estate getEstate(int index)
        {
            return GetAt(index);
        }

        public void clearList()
        {
            DeleteAll();
        }

        public List<Estate> getEstates()
        {
            List<Estate> estates = new List<Estate>();

            for(int i = 0; i < Count; i++)
            {
                estates.Add(getEstate(i));
            }

            return estates;
        }


        public void getDescription(int index)
        {
            getEstate(index).toString();
        }

        public bool idExists(int id)
        {

            for(int i = 0; i < Count; i++)
            {
                if(getEstate(i).EstateID == id)
                {
                    return true;
                }
            }

            return false;
        }



        public int getIndexOfEstate(int id)
        {
            int index = -1;

            for (int i = 0; i < Count; i++)
            {
                if (id == getEstate(i).EstateID)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

    }
}
