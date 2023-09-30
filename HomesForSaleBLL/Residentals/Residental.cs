using System;
namespace HomesForSaleBLL
{
    //Abstract class that all the concrete residental estates inherit from
    [Serializable]
    public abstract class Residental : Estate
    {

        public Residental()
        {

        }


        public Residental(int estateId, Adress adress, LegalForm legalForm) : base(estateId, adress, legalForm)
        {
            
        }

      
    }
}
