using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.Composite.Model
{
    public class UserSource
    {
        public string Name { get; set; }
        public ICollection<UserSource> Child { get; set; } = new List<UserSource>();
    }
}
