using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        
        public string TeamName { get; set; }
        //public int AdminId { get; set; }

        //[ForeignKey("UserId")]
        public virtual User Admin { get; set; }

        //public List<string> TeamMembers { get; set; } 
        public ICollection<User> TeamMembers { get; set; }
        //public virtual ICollection<UsersInTeams> TeamMembers { get; set; }
        public ICollection<Feature> Features { get; set; }
        
    }
}
