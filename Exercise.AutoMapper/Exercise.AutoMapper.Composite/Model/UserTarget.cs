using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise.AutoMapper.Composite.Model
{
    public class UserTarget
    {
        public string Name { get; set; }
        public ICollection<UserTarget> Child { get; set; }

        public string Print(int param = 0)
        {
            StringBuilder builder = new StringBuilder();

            param++;

            builder.Append($"level - {param} ");
            builder.AppendLine(Name);

            if (Child.Any())
                foreach (var child in Child)
                {
                    builder.Append(child.Print(param));
                }

            return builder.ToString();
        }
    }
}
