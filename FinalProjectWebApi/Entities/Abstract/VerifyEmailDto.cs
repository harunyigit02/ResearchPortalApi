﻿namespace FinalProjectWebApi.Entities.Abstract
{
    public class VerifyEmailDto
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
