using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Group
    {
        //Entity framework need empty constructor for creating table 
        public Group()
        {

        }

        public Group(string name)
        {
            this.Name = name;

        }

        [Key]
        public string Name { get; set; } //Just use group name as primary key 
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}