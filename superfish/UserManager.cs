/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace superfish
{

    public partial class UserManager
    {

        static UserManager defaultInstance = new UserManager();
        MobileServiceClient client;
        

        private UserManager()
        {

            // todo: to base manager

#if DEBUG
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.client.AlternateLoginHost = new Uri(Constants.AlternateLoginHostUrl);
#else
   this.client = new MobileServiceClient(Constants.MobileAppUrlProd);  
#endif

        }

        public static UserManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        
        public async Task<JToken> InvokeApiAsync(string apiName, HttpMethod method, Dictionary<string, string> dictonary)
        {
            return await this.client.InvokeApiAsync(apiName, method, dictonary);
        }
        
        
    }
}
