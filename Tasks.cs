using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworks_WPF
{
    [Serializable]
    internal class Task
    {
        private string name;
        private DateTime date;
        private uint priority;
        public Task(string name, DateTime date, uint priority)
        {
            this.name = name;
            this.date = date;
            this.priority = priority;
        }
        public string Name
        {
            get { return name; }
        }
        public DateTime Date
        {
            get { return date; }
        }
        public uint Priority
        {
            get { return priority; }
        }
    }
}
