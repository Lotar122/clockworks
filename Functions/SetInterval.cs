using System.Timers;

namespace clockworks.Methods
{
    public static partial class Functions
    {
        public static System.Timers.Timer SetInterval(ElapsedEventHandler action, int timestep)
        {
            System.Timers.Timer tmr = new System.Timers.Timer(timestep);
            tmr.Elapsed += action;
            return tmr;
        }
    }
}