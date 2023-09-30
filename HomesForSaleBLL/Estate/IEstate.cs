using System;
namespace HomesForSaleBLL
{

    //Interface that all estate objects implements
    //Defines the necessary attributes for every type of Estate object
    public interface IEstate
    {
        int EstateID { get; set; }
        Adress EstateAdress { get; set; }
        LegalForm LegalForm { get; set; }

    }

}
