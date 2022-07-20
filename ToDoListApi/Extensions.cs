using System.Security.Claims;
using System.Security.Principal;

namespace ToDoListApi
{
    public static class Extensions
    {
        /// <summary>
        /// достает id пользователя из токина
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static int GetId(this IIdentity identity) 
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var clam = claimsIdentity?.Claims.FirstOrDefault(x=>x.Type=="id");
            return int.Parse(clam?.Value);
        }
    }
}
