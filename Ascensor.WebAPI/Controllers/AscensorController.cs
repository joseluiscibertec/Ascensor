using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Interfaces;
using Ascensor.WebAPI.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Ascensor.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AscensorController : ControllerBase
    {
        // Instanciar al servicio
        private readonly IAscensor _service;

        public AscensorController(IAscensor service)
        {
            _service = service;
        }

        // GET: api/Ascensor/Get/1
        [HttpGet("{Asce_Id}")]
        public Response<AscensorEntity> Get(int Asce_Id)
        {
            try
            {
                var obj = _service.Get(Asce_Id);
                if (obj.Asce_Id > 0)
                {
                    return new Response<AscensorEntity>(true, obj, "OK", null);
                }
                else
                {
                    return new Response<AscensorEntity>(false, null, "Regitro no encontrado.", 1);
                }
            }
            catch (Exception ex)
            {
                return new Response<AscensorEntity>(false, null, $"{ex.Message}.", 0);
            }
        }

        // GET: api/Ascensor/GetAll
        [HttpGet]
        public Response<List<AscensorEntity>> GetAll()
        {
            try
            {
                var list = _service.GetAll();
                if (list.Count > 0)
                {
                    return new Response<List<AscensorEntity>>(true, list, "OK", null);
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún regitro.", 1);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<AscensorEntity>>(false, null, $"{ex.Message}.", 0);
            }
        }
    }
}