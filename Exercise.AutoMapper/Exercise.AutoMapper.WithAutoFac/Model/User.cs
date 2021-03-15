
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.WithAutoFac.Model
{
    public class User
    {
        private readonly ILogger logger;

        public string Name { get; set; }
        public int Age { get; set; } 

        public User(ILogger logger)
        {
            this.logger = logger;
        }

        public void WriteName()
        {
            logger.Write(Name);
        }
    }
}
