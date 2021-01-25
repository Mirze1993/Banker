using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum Status
    {
        NotFound=404,
        SystemError=500,
        Succes=200,
        SuccessCreate=201,
        PasswordIncorrect=1

    }
}
