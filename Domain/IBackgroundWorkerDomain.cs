using Domain.Model;
using System.Collections.Generic;

namespace Domain
{
    public interface IBackgroundWorkerDomain
    {
        void ReadFile(List<ClientModel> clientList, List<SalesmanModel> salesmanList, List<SaleModel> saleList);
    }
}
