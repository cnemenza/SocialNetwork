using PayliteMessaging.Client;
using SOCIALNETWORK.API.Models.Login;
using SOCIALNETWORK.CORE;
using SOCIALNETWORK.ENTITIES.Models;
using SOCIALNETWORK.REPOSITORY.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOCIALNETWORK.API.Controllers
{
    [RoutePrefix("api")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("registro")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    if (!model.Password.Equals(model.PasswordVerifier))
                        return BadRequest("Verificar contraseñas");

                    var emailExist = await _context.Users.AnyAsync(x => x.Email.Equals(model.Email));

                    if (emailExist)
                        return BadRequest("El correo electronico ya se encuentra registrado.");

                    var user = new User
                    {
                        Email = model.Email,
                        PasswordHash = model.Password.EncodeString(),
                        Type = ConstantsHelpers.User.Type.NORMAL,
                        UserDetail = new UserDetail
                        {
                            Name = model.Name,
                            PatSurname = model.PatSurname
                        },
                        ConfirmedEmail = false
                    };

                    var newUser = _context.Users.Add(user);

                    await _context.SaveChangesAsync();

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("christiannemenza@gmail.com", "vcepbmzzlucuqcaz");
                    MailMessage msObj = new MailMessage();
                    msObj.To.Add(model.Email);
                    msObj.From = new MailAddress("joinus2k19@gmail.com");
                    msObj.Subject = "Confirmaciòn JoinUs2k19";

                    msObj.Body = $"<h4>Verificar correo electronico.</h4>\n<h5>Hola {user.UserDetail.Name} Ingresar al siguiente link :</h5>\n" +
                        $"<a href='{ConstantsHelpers.Api.BASEURI}/api/validacion?id={user.Id}'>Validar Correo</a>";

                    msObj.IsBodyHtml = true;

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s
                       , X509Certificate certificate
                       , X509Chain chain
                       , SslPolicyErrors sslPolicyErrors)
                    { return true; };

                    client.Send(msObj);

                    return Ok("Usuario Registrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ingreso")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {

                    var user = await _context.Users.Where(x => x.Email.ToLower().Trim().Equals(model.Email.ToLower().Trim()))
                        .Include(x=>x.UserDetail).FirstOrDefaultAsync();

                    if(user is null)
                        return BadRequest("El correo electronico no se encuentra registrado.");

                    if (user.ConfirmedEmail == false)
                        return BadRequest("Es necesario confirmar la cuenta para poder ingresar.");

                    var passwordDecoded = user.PasswordHash.DecodeString();

                    if (passwordDecoded.Equals(model.Password))
                    {
                        return Ok(new
                        {
                            user.Id,
                            user.UserDetail.Name,
                            user.UserDetail.PatSurname,
                            user.Email
                        });
                    }
                     
                    return BadRequest("Usuario y/o contraseña incorrectos.");

                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error de conexión. Por favor vuelva a intentarlo nuevamente.");
            }
          
        }

        [HttpGet]
        [Route("validacion")]
        public async Task<IHttpActionResult> Valid(Guid id)
        {
            using(var _context = new DatabaseContext())
            {
                var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                user.ConfirmedEmail = true;
                await _context.SaveChangesAsync();

                string url = "https://joinus2k19.azurewebsites.net";
                System.Uri uri = new System.Uri(url);

                return Redirect(uri);
            }
        }
    }
}
