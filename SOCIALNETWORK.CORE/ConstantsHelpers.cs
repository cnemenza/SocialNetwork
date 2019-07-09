using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.CORE
{
    public class ConstantsHelpers
    {
        //public const string ConnectionString = "Server=localhost;Database=SOCIALNETWORK.DB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const string ConnectionString = "Server=tcp:joinus2k19.database.windows.net,1433;Initial Catalog=joinus2k19;Persist Security Info=False;User ID=werty51;Password=ProyectoTecnologico1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public class User
        {
            public class Type
            {
                public const byte ADMIN = 0;
                public const byte NORMAL = 1;  
            }

            public class Status
            {
                public const byte INACTIVE = 0;
                public const byte ACTIVE = 1;
            }
        }

        public class Event
        {
            public class Type
            {
                public const string ISOWNER = "OWNER";
                public const string ISUSERFREE = "FREE";
                public const string ISUSERREGISTER = "REGISTER";

            }
        }

        public class Api
        {
            //public const string BASEURI = "http://localhost:62722";
            public const string BASEURI = "https://joinus2k19api.azurewebsites.net";


            public class Login
            {
                public const string REGISTER = "/api/registro";
                public const string LOGIN = "/api/ingreso";
            }

            public class StudyCenter
            {
                public const string STUDYCENTERLIST = "/api/listar-centro-estudios";
                public const string DEGREELIST = "/api/listar-carreras?studyCenterId={0}";
            }

            public class User
            {
                public const string GETPROFILE = "/api/usuario/get-perfil?userId={0}";
                public const string UPDATEPROFILE = "/api/usuario/actualizar-perfil";
                public const string UPDATEPHOTO = "/api/usuario/actualizar-foto";
            }
            
            public class Event
            {
                public const string NEWEVENT = "/api/evento/nuevo";
                public const string GETMYEVENTS = "/api/evento/get-mis-eventos?userId={0}";
                public const string GETEVENTS = "/api/evento/get-eventos";
                public const string GETDETAILS = "/api/evento/{0}/detalle/{1}";
                public const string JOIN = "/api/evento/{0}/unirse/{1}";
                public const string DELETE = "/api/evento/{0}/eliminar/{1}";
                public const string GETMEMBERS = "/api/evento/{0}/participantes";
                public const string GOOUT = "/api/evento/{0}/salir/{1}";
            }

            public class Forum
            {
                public const string GETMESSAGES = "/api/foro/get-mensajes?eventId={0}";
                public const string NEWMESSAGE = "/api/foro/nuevo-mensaje";
                public const string HASAUTHORIZATION = "/api/foro/autorizacion?eventId={0}&userId={1}";
            }
        }
    }
}
