using System;
using System.Threading.Tasks;

namespace XAF.RestClient
{
    public interface IRestClient
    {
        bool IsLoggedIn { get; }

        void Login(Uri uri, object credentials);

        T Get<T>(Uri uri);

        T Post<T>(Uri uri, object content);

        T Patch<T>(Uri uri, object content);

        bool Delete(Uri uri);

        bool Logout(Uri uri);
    }
}