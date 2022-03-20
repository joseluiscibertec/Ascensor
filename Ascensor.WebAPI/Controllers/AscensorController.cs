using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Interfaces;
using Ascensor.WebAPI.DTO.Responses;
using Ascensor.WebAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        // GET: api/Ascensor/GetAll/100
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

        // GET: api/Ascensor/MoveFromInside/1
        [HttpGet("{Asce_Piso}")]
        public Response<List<AscensorEntity>> MoveFromInside(int Asce_Piso)
        {
            MoveFromInsideResponse res = new MoveFromInsideResponse();

            try
            {
                int cont = 0;
                int Asce_PisoInicial = 0;

                var list = _service.GetAll();
                if (list.Count > 0)
                {
                    Asce_PisoInicial = new RandomHelper().RandomGenerate(list.Count);
                    // Si el piso inicial es igual piso final => se genera otro numero random
                    while (Asce_PisoInicial == Asce_Piso)
                    {
                        Asce_PisoInicial = new RandomHelper().RandomGenerate(list.Count);
                        if (cont == 10)
                        {
                            Asce_PisoInicial = (list.Count == Asce_Piso) ? Asce_Piso - 1 : Asce_Piso + 1;
                            break;
                        }

                        cont++;
                    }

                    foreach (var item in list)
                    {
                        Thread.Sleep(item.Asce_Tiempo);
                        var output = _service.Update(item);
                    }

                    res.Asce_PisoInicial = Asce_PisoInicial;
                    res.Asce_PisoFinal = Asce_Piso;
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún piso regitrado.", 1);
                }

                foreach (var item in list)
                {
                    Thread.Sleep(item.Asce_Tiempo);
                    var output = _service.Update(item);
                }

                list.ForEach(s => { s.Asce_Estado = false; s.Asce_MiUbicacion = false; });
                list.Where(x => x.Asce_Piso == Asce_Piso).ToList().ForEach(s => { s.Asce_Estado = true; s.Asce_MiUbicacion = true; });

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

        // GET: api/Ascensor/MoveFromOutside/1
        [HttpGet("{Asce_Piso}")]
        public Response<List<AscensorEntity>> MoveFromOutside(int Asce_Piso)
        {
            try
            {
                var list = _service.GetAll();
                list.ForEach(s => { s.Asce_Estado = false; s.Asce_MiUbicacion = false; });
                list.Where(x => x.Asce_Piso == Asce_Piso).ToList().ForEach(s => { s.Asce_Estado = true; s.Asce_MiUbicacion = true; });

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