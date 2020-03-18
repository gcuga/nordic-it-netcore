﻿using L22CW01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L22CW01.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : Controller
    {
        public CitiesDataStore CitiesStore { get; }

        public CitiesController()
        {
            CitiesStore = CitiesDataStore.Instance;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            return new JsonResult(CitiesStore);
        }

        [HttpGet("{id}", Name = "GetCityById")]
        public IActionResult GetCity(int id)
        {
            var city = CitiesStore.Cities.FirstOrDefault(x => x.Id == id);

            if (city != null)
            {
                // 200 Ok
                return Ok(city);
            }

            // 404 Not Found
            return NotFound("404 Not Found");
        }

        [HttpPost]
        public IActionResult AddCity([FromBody] City city)
        {
            if (CitiesStore.Cities.FirstOrDefault(
                x => x.Id == city.Id || x.Name == city.Name) != null)
            {
                return Conflict();
            }

            CitiesStore.Cities.Add(city);

            return CreatedAtRoute("GetCityById", new { id = city.Id }, city);
        }
    }
}
