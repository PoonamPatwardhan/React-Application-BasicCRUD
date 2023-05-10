using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.InfrastructureLayer.TableRelations
{
    public class UserRooms
    {
        public int Id { get; set; }
        //public int UserId { get; set; }        
        public User User { get; set; }

        //public int RoomId { get; set; }
        public Room Room { get; set; }
    }

    public class UsersInTeams
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoomId { get; set; }
        public Room Team { get; set; }

    }
}
