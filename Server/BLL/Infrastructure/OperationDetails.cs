using System.Linq;
using Microsoft.AspNet.Identity;

namespace BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
        public bool Succedeed { get; }
        public string Message { get; }
        public string Property { get; }

        public static OperationDetails IsAnyError(IdentityResult identityResult)
        {
            OperationDetails operationDetails;
            if (identityResult.Errors.Any())
            {
                operationDetails = new OperationDetails(false, identityResult.Errors.FirstOrDefault(), "");
                return operationDetails;
            }
            operationDetails = new OperationDetails(true, "", "");
            return operationDetails;
        }
    }
}
