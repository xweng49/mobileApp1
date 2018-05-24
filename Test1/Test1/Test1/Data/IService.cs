using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Data
{
    public interface IService
    {
        Task<String> InvokeGet(string url);
        Task<String> InvokePost(string url, string jsonObject);
        Task<String> InvokeHead(string url, string request);
    }
}
