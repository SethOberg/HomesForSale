using System;

namespace HomesForSaleBLL
{
    //Abstract class that all the concrete commcercial estates inherit from
    [Serializable]
    public abstract class Commercial : Estate
    {
        public Commercial()
        {
        }

        public Commercial(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {

        }

    }
}
