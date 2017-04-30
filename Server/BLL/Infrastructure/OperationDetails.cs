using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BLL.Infrastructure
{
    class OperationDetails
    {
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

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
