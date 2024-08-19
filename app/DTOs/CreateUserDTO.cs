using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs;

public record CreateUserDTO(string Email, string UserName, string Language, string Password);