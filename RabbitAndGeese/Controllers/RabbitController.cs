using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitAndGeese.DataAccess;
using RabbitAndGeese.Models;

namespace RabbitAndGeese.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RabbitStorage _storage;

        public RabbitController(IConfiguration config)
        {
            _storage = new RabbitStorage(config);
        }

        [HttpPost]
        public void AddACustomer(Rabbit rabbit)
        {
            _storage.Add(rabbit);
        }

        [HttpGet]
        public IActionResult GetRabbits()
        {
            return Ok(_storage.GetAllRabbits());
        }

        [HttpGet("{id}")]
        public IActionResult GetRabbitById(int id)
        {
            return Ok(_storage.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRabbit(int id)
        {
            var rabbit = _storage.GetById(id);

            if (rabbit == null)
            {
                return NotFound();
            }

            var success = _storage.DeleteById(id);

            if (success)
            {
                return Ok();
            }

            return BadRequest(new {Message = "Delete was unsuccessful"});
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