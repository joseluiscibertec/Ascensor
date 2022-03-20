namespace Ascensor.WebAPI.DTO.Responses
{
    public class MoveFromInsideResponse
    {
        public int Asce_PisoInicial { get; set; }
        public int Asce_PisoFinal { get; set; }
        public int[] Asce_PisosRecorridos { get; set; }
        public int Asce_TiempoRecorrido { get; set; }
    }
}