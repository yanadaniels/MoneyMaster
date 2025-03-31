using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Services.Contracts.User
{
    public class UserRefreshJwtTokenDto
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

        /// <summary>Токе доступа</summary>
        public required string AccsessToken { get; set; }

        /// <summary>Токе для обновления токеда доступа</summary>
        public required string RefreshToken { get; set; }

    }
}
