using L22CW01.Models;
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

        // PUT: api/cities/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] City city)
        {
            try
            {
                if (city == null)
                {
                    //_logger.LogError("Owner object sent from client is null.");
                    return BadRequest("City object is null");
                }

                // валидация модели не работает!!!
                if (!ModelState.IsValid)
                {
                    //_logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                int foundIndex = -1;
                for (int i = 0; i < CitiesStore.Cities.Count; i++)
                {
                    if (CitiesStore.Cities[i].Id == id)
                    {
                        foundIndex = i;
                        break;
                    }
                }

                if (foundIndex == -1)
                {
                    //_logger.LogError($"City with id: {id}, hasn't been found in db.");
                    return NotFound($"404 Not Found. City with id = {id} not found");
                }

                CitiesStore.Cities[foundIndex] = city;
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/cities/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int foundIndex = CitiesStore.Cities.FindIndex(x => x.Id == id);
                if (foundIndex == -1)
                {
                    //_logger.LogError($"City with id: {id}, hasn't been found in db.");
                    return NotFound($"404 Not Found. City with id = {id} not found");
                }

                CitiesStore.Cities.RemoveAt(foundIndex);
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
