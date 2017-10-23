using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetNodeDemo.Models
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public IList<AspNetNodeDemo.Models.User> Users
        {
            get
            {
                return new List<User>() {
                    new User { ID = 1, Active = true, BirthDate = new DateTime(1978, 12, 3), Email = "jklaric@gmail.com", FirstName = "Josip", LastName = "Klarić", Username = "jk" },
                    new User { ID = 2, Active = true, BirthDate = new DateTime(1967, 2, 9), Email = "rzeleznjak@gmail.com", FirstName = "Renato", LastName = "Železnjak", Username = "rz" },
                    new User { ID = 3, Active = true, BirthDate = new DateTime(1973, 4, 12), Email = "obajic@gmail.com", FirstName = "Ognjen", LastName = "Bajić", Username = "ob" },
                    new User { ID = 4, Active = true, BirthDate = new DateTime(1987, 6, 23), Email = "dabramec@gmail.com", FirstName = "Dobriša", LastName = "Adamec", Username = "dadamec" },
                    new User { ID = 5, Active = true, BirthDate = new DateTime(1989, 2, 13), Email = "marko.lohert@gmail.com", FirstName = "Marko", LastName = "Loher", Username = "ml" },
                    new User { ID = 6, Active = true, BirthDate = new DateTime(1992, 9, 8), Email = "mislav.startzik@gmail.com", FirstName = "Mislav", LastName = "Staržik", Username = "ms" },
                    new User { ID = 7, Active = true, BirthDate = new DateTime(1990, 10, 13), Email = "brunob@gmail.com", FirstName = "Bruno", LastName = "Brozović", Username = "bb" },
                    new User { ID = 8, Active = true, BirthDate = new DateTime(1986, 11, 23), Email = "dvuljanic@gmail.com", FirstName = "Dairio", LastName = "Vuljanić", Username = "dv" }
                };
            }
            //set;
        }
    }
}
