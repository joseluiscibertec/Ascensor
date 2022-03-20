namespace Ascensor.WebAPI.DTO.Entities
{
    public class AscensorEntity
    {
        public int Asce_Id { get; set; }
        public int Asce_Piso { get; set; }
        public bool Asce_MiUbicacion { get; set; }
        public int Asce_Tiempo { get; set; }
        public bool Asce_Estado { get; set; }
    }
}