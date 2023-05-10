using Microsoft.AspNetCore.Identity;
using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Models
{
    public class User : IdentityUser
    {        
        //[Required]
        //public string Role { get; set; }
        public ICollection<Room> Teams { get; set; }
        //public virtual ICollection<UsersInTeams> Teams { get; set; }
        //public ICollection<UserRooms> Teams { get; set; }

    }

    public class UserIdentity : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string EmailId { get; set; }
    }
}
