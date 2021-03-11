using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTokenTableAlumnos.Services
{
    public class ServiceTableAlumnos
    {
        private CloudTable tablaalumnos;

        public ServiceTableAlumnos(String keys)
        {
            CloudStorageAccount account =
                CloudStorageAccount.Parse(keys);
            CloudTableClient client =
                account.CreateCloudTableClient();
            this.tablaalumnos =
                client.GetTableReference("tablaalumnos");
        }

        //METODO PARA OFRECER TOKENS AL CONSUMIDOR
        //EN TABLES, PODEMOS FILTRAR LOS REGISTROS QUE 
        //DESEAMOS DEVOLVER O DAR ACCESO POR RANGOS
        //DE ROWKEY O PARTITIONKEY O COMBINACION
        public String GetKeySaS(String curso)
        {
            SharedAccessTablePolicy policy =
                new SharedAccessTablePolicy
                {
                     SharedAccessExpiryTime= DateTime.UtcNow.AddMinutes(30),
                      Permissions = SharedAccessTablePermissions.Query
                      | SharedAccessTablePermissions.Delete
                };
            String token = this.tablaalumnos.GetSharedAccessSignature(
                policy, null
                , curso, null, curso, null);
            return token;
        }
    }
}
