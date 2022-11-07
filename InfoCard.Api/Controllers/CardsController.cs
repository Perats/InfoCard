using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InfoCard.Di;
using Microsoft.AspNetCore.Mvc;

namespace InfoCard.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IDataStorage dataStorage;

        public CardsController(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        [HttpGet]
        public ActionResult<List<InfoCardBase>> Get()
        {
            return dataStorage.GetCards();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody][Required] Di.InfoCard infoCard)
        {
            if (infoCard.Id != 0)
            {
                return BadRequest();
            }
            return dataStorage.AddOrUpdateCard(infoCard);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody][Required] Di.InfoCard infoCard)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            infoCard.Id = id;
            dataStorage.AddOrUpdateCard(infoCard);
            return Ok();
        }

        [HttpDelete()]
        public void DeleteMutiple(int[] id)
        {
            dataStorage.DeleteCard(id);
        }
    }
}
