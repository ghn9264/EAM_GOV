using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eam.PrintService
{ 
    [ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = "http://wwww.ky.com")]
    public interface IPrintService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Test", 
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultBase Test();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/QrPrint/{base64Path}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultBase QrPrint(string base64Path);

        /*[OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Print",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultBase Print(PrintItem item);*/
         
    }
}