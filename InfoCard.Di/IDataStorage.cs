using System;
using System.Collections.Generic;
using System.Text;

namespace InfoCard.Di
{
    public interface IDataStorage
    {
        List<InfoCardBase> GetCards();

        InfoCard GetCard(int id);

        void DeleteCard(int[] id);

        int AddOrUpdateCard(InfoCard infoCard);

    }
}
