using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitAndGeese.DataAccess;
using RabbitAndGeese.Models;

namespace RabbitAndGeese.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RabbitStorage _storage;

        public RabbitController()
        {
            _storage = new RabbitStorage();
        }

        [HttpPost]
        public void AddACustomer(Rabbit rabbit)
        {
            _storage.Add(rabbit);
        }

        [HttpPut("{id}/geese")]
        public IActionResult AddGooseToRabbit(int id, Goose goose)
        {
            var rabbit = _storage.GetById(id);

            if (rabbit == null) return NotFound();

            rabbit.OwnedGeese.Add(goose);
            return Ok();
        }

        [HttpPut("{id}/saddles")]
        public IActionResult ProcureGooseSaddle(int id, Saddle saddle)
        {
            var rabbit = _storage.GetById(id);

            if (rabbit == null) return NotFound();

            rabbit.OwnedSaddles.Add(saddle);
            return Ok();
        }

    }
}