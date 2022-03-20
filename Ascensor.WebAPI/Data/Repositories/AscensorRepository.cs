using Ascensor.WebAPI.Data.Context;
using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ascensor.WebAPI.Data.Repositories
{
    public class AscensorRepository : IAscensor
    {
        // Instancia al DB Context
        private readonly DBAscensorContext _context;

        public AscensorRepository(DBAscensorContext context)
        {
            _context = context;
        }

        public AscensorEntity Get(int Asce_Id)
        {
            return _context.Ascensors.Where(x => x.Asce_Id == Asce_Id).FirstOrDefault() ?? new AscensorEntity();
        }
        public List<AscensorEntity> GetAll()
        {
            return _context.Ascensors.ToList();
        }
        public List<AscensorEntity> GetAllSP(int Coun_Id)
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            using (SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_Country_LIS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Coun_Id", SqlDbType.Int).Value = (object)Coun_Id ?? DBNull.Value;
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AscensorEntity obj = new AscensorEntity
                            {
                                Asce_Id = (dr["Asce_Id"] != DBNull.Value) ? Convert.ToInt32(dr["Asce_Id"]) : 0,
                                Asce_Piso = (dr["Asce_Piso"] != DBNull.Value) ? Convert.ToInt32(dr["Asce_Piso"]) : 0,
                                Asce_MiUbicacion = (dr["Asce_MiUbicacion"] != DBNull.Value) ? Convert.ToBoolean(dr["Asce_MiUbicacion"]) : false,
                                Asce_Tiempo = (dr["Asce_Tiempo"] != DBNull.Value) ? Convert.ToInt32(dr["Asce_Tiempo"]) : 0,
                                Asce_Estado = (dr["Asce_Estado"] != DBNull.Value) ? Convert.ToBoolean(dr["Asce_Estado"]) : false,
                            };

                            list.Add(obj);
                        }

                        dr.Close();
                        dr.DisposeAsync();
                    }

                    cmd.Dispose();
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }
        public int Insert(AscensorEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Ascensors.Add(entity);
            _context.SaveChanges();

            return entity.Asce_Id;
        }
        public int Update(AscensorEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Ascensors.Add(entity);
            _context.SaveChanges();

            return entity.Asce_Id;
        }
        public int Delete(int Asce_Id)
        {
            var obj = _context.Ascensors.Where(x => x.Asce_Id == Asce_Id).First();
            _context.Ascensors.Attach(obj);
            _context.Ascensors.Remove(obj);
            _context.SaveChanges();

            return Asce_Id;
        }
    }
}