using System;
using System.Threading.Tasks;

namespace XAF.RestClient
{
    public interface IRestClient
    {
        bool IsLoggedIn { get; }

        void Login(Uri uri, object credentials);

        object Get(Uri uri);

        object Post(Uri uri, object content);

        object Patch(Uri uri, object content);

        bool Delete(Uri uri);

        bool Logout(Uri uri);
    }
}