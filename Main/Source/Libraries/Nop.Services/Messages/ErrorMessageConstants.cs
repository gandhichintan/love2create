using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Messages
{
    public class ErrorMessageConstants
    {
        #region "Product Error Messages"
        public const string ProductDoesNotExitsError = "Product doesn't exits.";
        public const string NoProductFoundError = "No product found";
        #endregion 

        #region "Comment"
        public const string ArgumentNullEntityComment = "comment";
        #endregion
    }
}
