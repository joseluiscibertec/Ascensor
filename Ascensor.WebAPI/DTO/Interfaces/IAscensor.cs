using Ascensor.WebAPI.DTO.Entities;
using System.Collections.Generic;

namespace Ascensor.WebAPI.DTO.Interfaces
{
    public interface IAscensor
    {
        public AscensorEntity Get(int Asce_Id);
        public AscensorEntity GetByPiso(int Asce_Piso);
        public List<AscensorEntity> GetAll(bool update = false);
        public int Insert(AscensorEntity entity);
        public int Update(AscensorEntity entity);
        public int Delete(int Asce_Id);
    }
}