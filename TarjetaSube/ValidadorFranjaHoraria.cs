using System;

namespace TarjetaSube
{
    public static class ValidadorFranjaHoraria
    {
        public static bool EstaEnFranjaHorariaPermitida(DateTime fechaHora)
        {
            return fechaHora.DayOfWeek >= DayOfWeek.Monday && 
                    fechaHora.DayOfWeek <= DayOfWeek.Friday &&
                    fechaHora.Hour >= 6 && 
                    fechaHora.Hour < 22;
        }
    }
}