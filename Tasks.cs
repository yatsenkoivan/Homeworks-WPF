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
        public Task(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
        }
        public string Name
        {
            get { return name; }
        }
        public DateTime Date
        {
            get { return date; }
        }
    }
}
