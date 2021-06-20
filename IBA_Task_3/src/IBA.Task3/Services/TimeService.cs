using System;

namespace IBA.Task3
{
    public class TimeService
    {
        private static Func<DateTime> now;

        /// <summary>
        /// Получить или установить текущее время.
        /// </summary>
        /// <value>Текущее время.</value>
        public static DateTime Now
        {
            get { return now(); }
            set { now = () => value; }
        }

        /// <summary>
        /// Сбросить занчения по умолчанию.
        /// </summary>
        public static void Reset()
        {
            now = () => DateTime.Now;
        }

        static TimeService()
        {
            Reset();
        }
    }
}