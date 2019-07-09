using SOCIALNETWORK.API.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SOCIALNETWORK.API.Controllers
{
    [RoutePrefix("")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("servicios")]
        public IHttpActionResult Index()
        {
            var list = new List<ServicesTest>
            {
                new ServicesTest
                {
                    Id = 1,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown" +
                    " printer took a galley of type and scrambled it to make a type specimen book. It has survived not" +
                    " only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                    " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Name = "Dermatologia",
                    Price = "20.00",
                    UrlImage = "http://perros.com.ve/wp-content/uploads/2013/02/Enfermedades-m%C3%A1s-comunes-en-los-perros.jpg"
                },
                new ServicesTest
                {
                    Id = 2,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown" +
                    " printer took a galley of type and scrambled it to make a type specimen book. It has survived not" +
                    " only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                    " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Name = "Guardería Canina y Felina",
                    Price = "3  0.00",
                    UrlImage = "http://perros.com.ve/wp-content/uploads/2013/02/Enfermedades-m%C3%A1s-comunes-en-los-perros.jpg"
                },
                new ServicesTest
                {
                    Id = 3,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown" +
                    " printer took a galley of type and scrambled it to make a type specimen book. It has survived not" +
                    " only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                    " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Name = "Dermatologia",
                    Price = "20.00",
                    UrlImage = "http://perros.com.ve/wp-content/uploads/2013/02/Enfermedades-m%C3%A1s-comunes-en-los-perros.jpg"
                },
                new ServicesTest
                {
                    Id = 4,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown" +
                    " printer took a galley of type and scrambled it to make a type specimen book. It has survived not" +
                    " only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                    " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Name = "Dermatologia",
                    Price = "20.00",
                    UrlImage = "http://perros.com.ve/wp-content/uploads/2013/02/Enfermedades-m%C3%A1s-comunes-en-los-perros.jpg"
                },
            };

            return Ok(list);
        }
    }
}