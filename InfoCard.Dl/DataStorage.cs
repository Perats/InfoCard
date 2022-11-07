using InfoCard.Di;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoCard.Dl
{
    public class DataStorage : IDataStorage
    {
        Dictionary<int, Di.InfoCard> cardsList = new Dictionary<int, Di.InfoCard>() {
            { 1, new Di.InfoCard() { Id = 1, Name = "Test1", ImageData = new byte[3] { 1,1,1} } }
        };
        int maxCardId = 1;

        public int AddOrUpdateCard(Di.InfoCard infoCard)
        {
            if (infoCard.Id == default)
            {
                cardsList.Add(++maxCardId, new Di.InfoCard() { Id = infoCard.Id, Name = infoCard.Name, ImageData = infoCard.ImageData });
                return maxCardId;
            }
            else
            {
                cardsList[infoCard.Id] = new Di.InfoCard() { Id = infoCard.Id, Name = infoCard.Name, ImageData = infoCard.ImageData };
                return infoCard.Id;
            }
        }

        public void DeleteCard(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                cardsList.Remove(id[i]);
            }
        }

        public Di.InfoCard GetCard(int id)
        {
            return cardsList[id];
        }

        public List<InfoCardBase> GetCards()
        {
            return cardsList.Values.Cast<InfoCardBase>().ToList();
        }
    }
}
