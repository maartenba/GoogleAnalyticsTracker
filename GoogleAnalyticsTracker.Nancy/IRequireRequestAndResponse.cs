using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace GoogleAnalyticsTracker.Nancy
{
    interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(Request requestMessage, Response responseMessage);
    }
}
