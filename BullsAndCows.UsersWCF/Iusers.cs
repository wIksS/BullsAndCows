using BullsAndCows.Models;
using BullsAndCows.UsersWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BullsAndCows.UsersWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iusers" in both code and config file together.
    [ServiceContract]
    public interface Iusers
    {
        [OperationContract]
        [WebGet(UriTemplate = "?page={page}")]
        IEnumerable<UserModel> GetUsers(int page);

        [OperationContract]
        [WebGet(UriTemplate = "/{id}")]
        RankUserModel GetUser(string id);
    }
}
