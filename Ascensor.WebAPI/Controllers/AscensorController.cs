using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Interfaces;
using Ascensor.WebAPI.DTO.Responses;
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

        // GET: api/Ascensor/GetAll/
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

        // GET: api/Ascensor/MoveFromInside/1/4
        [HttpGet("{Asce_PisoInicial}/{Asce_PisoFinal}")]
        public Response<List<AscensorEntity>> MoveFromInside(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            string direccionAscensor = string.Empty;

            try
            {
                #region  VALIDACIONES

                if (Asce_PisoInicial == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso inicial que ingresó no es válido.", 1); }
                if (Asce_PisoFinal == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso final que ingresó no es válido.", 2); }
                if (Asce_PisoInicial == Asce_PisoFinal) { return new Response<List<AscensorEntity>>(false, null, "Ingrese un piso diferente a tu piso actual.", 3); }

                #endregion

                var list = _service.GetAll(true);
                if (list.Count > 0)
                {
                    if (Asce_PisoFinal > list.Count) { return new Response<List<AscensorEntity>>(false, null, $"El piso final que ingresó no es válido(pisos disponibles hasta: {list.Count}).", 4); }

                    direccionAscensor = (Asce_PisoFinal < Asce_PisoInicial) ? "B" : "S";
                    //list = (direccionAscensor == "B") ? list.OrderByDescending(x => x.Asce_Piso).ToList() : list.OrderBy(x => x.Asce_Piso).ToList();
                    int order = 1;
                    foreach (var item in list)
                    {
                        if (direccionAscensor == "B" && Asce_PisoFinal <= item.Asce_Piso || direccionAscensor == "S" && Asce_PisoFinal >= item.Asce_Piso)
                        {
                            //Thread.Sleep(item.Asce_Tiempo * 1000);
                            if (direccionAscensor == "B")
                            {
                                if (Asce_PisoInicial >= item.Asce_Piso)
                                {
                                    item.Asce_Recorrido = true;
                                    item.Asce_OrdenR = order;
                                    order++;
                                }
                            }
                            else
                            {
                                if (Asce_PisoInicial <= item.Asce_Piso)
                                {
                                    item.Asce_Recorrido = true;
                                    item.Asce_OrdenR = order;
                                    order++;
                                }

                            }


                            if (Asce_PisoFinal == item.Asce_Piso)
                            {
                                item.Asce_MiUbicacion = true;
                                item.Asce_Estado = true;
                            }

                            var output = _service.Update(item);
                        }
                    }

                    return new Response<List<AscensorEntity>>(true, list, "OK", null);
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún piso regitrado.", 5);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<AscensorEntity>>(false, null, $"{ex.Message}.", 0);
            }
        }

        // GET: api/Ascensor/MoveFromOutside/1/4
        [HttpGet("{Asce_PisoInicial}/{Asce_PisoFinal}")]
        public Response<List<AscensorEntity>> MoveFromOutside(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            string direccionAscensor = string.Empty;

            try
            {
                #region  VALIDACIONES

                if (Asce_PisoInicial == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso inicial que ingresó no es válido.", 1); }
                if (Asce_PisoFinal == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso final que ingresó no es válido.", 2); }
                if (Asce_PisoInicial == Asce_PisoFinal) { return new Response<List<AscensorEntity>>(false, null, "Ingrese un piso diferente a tu piso actual.", 3); }

                #endregion

                var list = _service.GetAll(true);
                if (list.Count > 0)
                {
                    if (Asce_PisoFinal > list.Count) { return new Response<List<AscensorEntity>>(false, null, $"El piso final que ingresó no es válido(pisos disponibles hasta: {list.Count}).", 4); }

                    direccionAscensor = (Asce_PisoFinal < Asce_PisoInicial) ? "B" : "S";
                    list = (direccionAscensor == "B") ? list.OrderByDescending(x => x.Asce_Piso).ToList() : list.OrderBy(x => x.Asce_Piso).ToList();

                    foreach (var item in list)
                    {
                        if (direccionAscensor == "B" && Asce_PisoFinal <= item.Asce_Piso || direccionAscensor == "S" && Asce_PisoFinal >= item.Asce_Piso)
                        {
                            Thread.Sleep(item.Asce_Tiempo * 1000);
                            item.Asce_Recorrido = true;
                            if (Asce_PisoFinal == item.Asce_Piso)
                            {
                                item.Asce_MiUbicacion = true;
                                item.Asce_Estado = true;
                            }

                            var output = _service.Update(item);
                        }
                    }

                    return new Response<List<AscensorEntity>>(true, list, "OK", null);
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún piso regitrado.", 5);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<AscensorEntity>>(false, null, $"{ex.Message}.", 0);
            }
        }

        // GET: api/Ascensor/ListOfPendingFloors
        [HttpGet("{Asce_PisoInicial}/{Asce_PisoFinal}")]
        public Response<List<AscensorEntity>> ListOfPendingFloors(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            string direccionAscensor = string.Empty;
            List<AscensorEntity> listAux = new List<AscensorEntity>();

            try
            {
                #region  VALIDACIONES

                if (Asce_PisoInicial == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso inicial que ingresó no es válido.", 1); }
                if (Asce_PisoFinal == 0) { return new Response<List<AscensorEntity>>(false, null, "El piso final que ingresó no es válido.", 2); }
                if (Asce_PisoInicial == Asce_PisoFinal) { return new Response<List<AscensorEntity>>(false, null, "Ingrese un piso diferente a tu piso actual.", 3); }

                #endregion

                var list = _service.GetAll();
                if (list.Count > 0)
                {
                    if (Asce_PisoFinal > list.Count) { return new Response<List<AscensorEntity>>(false, null, $"El piso final que ingresó no es válido(pisos disponibles hasta: {list.Count}).", 4); }

                    direccionAscensor = (Asce_PisoFinal < Asce_PisoInicial) ? "B" : "S";
                    list = (direccionAscensor == "B") ? list.OrderByDescending(x => x.Asce_Piso).ToList() : list.OrderBy(x => x.Asce_Piso).ToList();

                    foreach (var item in list)
                    {
                        if (direccionAscensor == "B" && Asce_PisoFinal <= item.Asce_Piso || direccionAscensor == "S" && Asce_PisoFinal >= item.Asce_Piso)
                        {
                            if (!item.Asce_Recorrido)
                            {
                                listAux.Add(item);
                            }
                        }
                    }

                    return new Response<List<AscensorEntity>>(true, listAux, "OK", null);
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún piso regitrado.", 5);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<AscensorEntity>>(false, null, $"{ex.Message}.", 0);
            }
        }

        // GET: api/Ascensor/GetCurrentFloor
        [HttpGet]
        public Response<List<AscensorEntity>> GetCurrentFloor()
        {
            try
            {
                var list = _service.GetAll();
                if (list.Count > 0)
                {
                    list = list.Where(x => x.Asce_Estado == true && x.Asce_MiUbicacion == true && x.Asce_Recorrido == true).ToList();
                    return new Response<List<AscensorEntity>>(true, list, "OK", null);
                }
                else
                {
                    return new Response<List<AscensorEntity>>(false, null, "No existe ningún piso regitrado.", 5);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<AscensorEntity>>(false, null, $"{ex.Message}.", 0);
            }
        }
    }
}