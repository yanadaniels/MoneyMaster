using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MoneyMaster.Common
{
    /// <summary>
    ///  Привилегии доступные для назначения пользователю, если нужно добавить - добавляем сюда
    ///  В базу данных пишем в поле Role соответствующую роль в текстовом формате
    /// </summary>
    [Flags]
    public enum Privileges
    {
        System = 0,
        Administrator = 1,
        User,
        Manager,
    }

    /// <summary>
    /// Атрибут позволяющий разграничить доступ по нескольким ролям, агрегатор атрибута Authorize
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequirePrivilegeAttribute : AuthorizeAttribute
    {
        public RequirePrivilegeAttribute(params Privileges[] priviliges)
        {
            Roles = String.Join(",", priviliges);

            if (priviliges.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            this.Roles = string.Join(",", priviliges.Select(r => Enum.GetName(r.GetType(), r)));
        }

    }
}
