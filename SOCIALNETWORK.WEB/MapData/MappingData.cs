using SOCIALNETWORK.CORE;
using SOCIALNETWORK.ENTITIES.Models;
using SOCIALNETWORK.REPOSITORY.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.MapData
{
    public static class MappingData
    {
        public static void UploadData()
        {
            SaveStudyCenters().Wait();
            SaveUser().Wait();
            SaveSatff().Wait();
        }

        public static async Task SaveStudyCenters()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var studyCenter1 = new StudyCenter
                    {
                        Address = "Av. La Fontana 955",
                        Name = "ISIL",
                        Id = Guid.NewGuid()
                    };

                    var studyCenter2 = new StudyCenter
                    {
                        Address = "Av. Carlos Izaguirre 233",
                        Name = "Cibertec",
                        Id = Guid.NewGuid()
                    };

                    _context.StudyCenters.Add(studyCenter1);
                    _context.StudyCenters.Add(studyCenter2);


                    var degrees = new List<Degree>
                    {
                        new Degree
                        {
                            Id = Guid.NewGuid(),
                            Name = "Desarrollo de Software",
                            StudyCenterId = studyCenter1.Id
                        },
                        new Degree
                        {
                            Id = Guid.NewGuid(),
                            Name = "Sistemas de Informacion",
                            StudyCenterId = studyCenter1.Id
                        },
                        new Degree
                        {
                            Id = Guid.NewGuid(),
                            Name = "Desarrollo de Videojuegos",
                            StudyCenterId = studyCenter1.Id
                        },
                        new Degree
                        {
                            Id = Guid.NewGuid(),
                            Name = "Computacion e Informatica",
                            StudyCenterId = studyCenter2.Id
                        }
                    };

                    _context.Degrees.AddRange(degrees);

                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static async Task SaveUser()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var degree = await _context.Degrees.Where(x => x.StudyCenter.Name.Equals("ISIL"))
                        .Select(x => new { x.Id,x.StudyCenterId}).FirstOrDefaultAsync();

                    var user = new User
                    {
                        Email = "admin@gmail.com",
                        PasswordHash = "1".EncodeString(),
                        Type = ConstantsHelpers.User.Type.ADMIN,
                        UserDetail = new UserDetail
                        {
                            Name = "Administrador",
                            PatSurname = "1",
                            Dni = "12345678",
                            PhoneNumber = "999999999",
                            StudyCenterId = degree.StudyCenterId,
                            DegreeId = degree.Id,
                            NeedtoUpdate = false
                        }
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static async Task SaveSatff()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var staff = new List<Staff>
                    {
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Programador Web"
                        },
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Diseñador 3D"
                        },
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Maquetador Web"
                        },
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Diseñador Grafico"
                        },
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Programador de Videojuegos"
                        },
                        new Staff
                        {
                            Id = Guid.NewGuid(),
                            Name = "Tester"
                        }
                    };

                    _context.Staffs.AddRange(staff);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}